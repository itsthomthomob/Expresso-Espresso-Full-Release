using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SupportData 
{
    public int MyEmployeeID;
    public float MyWage;
    public string MyName;
    public string MyState;
    public float MySkill;
    public float MyFast;
    public string MyImageName;
}

public class EntitySupport : EntityBase
{
    public float maxRotation = 5f;
    public float rotationSpeed = 6.5f;
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private float EfficiencyModifier;
    private string PersonalityType = "N/A";

    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }
    public float GetWageAmount() { return WageAmount; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public string GetEmployeeRole() { return "Support"; }
    public void SetEmployeePersonality(string newPers) { PersonalityType = newPers; }
    public string GetEmployeePersonality() { return PersonalityType; }
    public void SetSpriteName(string newSprite) { SpriteName = newSprite; UnityEngine.Debug.Log("Set Sprite Name to: " + newSprite); }
    public string GetSpriteName() { return SpriteName; }
    public void SetEmployeeName(string newName) { EmployeeName = newName; }
    public string GetEmployeeName() { return EmployeeName; }
    public void SetSkillModifier(float newSkill) { SkillModifier = newSkill; }
    public float GetSkillModifier() { return SkillModifier; }
    public void SetEfficiencyModifier(float newEff) { EfficiencyModifier = newEff; }
    public float GetEfficiencyModifier() { return EfficiencyModifier; }


    public enum State 
    {
        TravelToRoaster, FillRoaster, TravelToBrewer, FillBrewer, TravelToEspresso, FillEspresso
    }

    [SerializeField] private State CurrentState = State.TravelToRoaster;

    public EntityRoasteryMachineOne Roaster;
    public EntityBrewingMachineOne Brewer;
    public EntityEspressoMachineOne Espresso;
    public TimeManager GetTime;
    public float Speed = 0.25f;

    private void Awake()
    {
        GetTime = FindObjectOfType<TimeManager>();
    }

    public override void OnEntityAwake()
    {
        EmployeeUserIntSystem GetEmployeeSystem = FindObjectOfType<EmployeeUserIntSystem>();
        SetEntitySprite(GetEmployeeSystem.CurrentCharacterImage);
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Support");
    }

    private void Start()
    {
        if (GetSpriteName() != "")
        {
            SetEntitySprite(Resources.Load<Sprite>(GetSpriteName()));
        }
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed));
        }

        Speed = 0.25f / GetTime.scale;
        
        switch (CurrentState)
        {
            case State.TravelToRoaster:
                OnTravelToRoaster();
                break;
            case State.FillRoaster:
                OnFillRoaster();
                break;
            case State.TravelToBrewer:
                OnTravelToBrewer();
                break;
            case State.FillBrewer:
                OnFillBrewer();
                break;
            case State.TravelToEspresso:
                OnTravelToEspresso();
                break;
            case State.FillEspresso:
                OnFillEspresso();
                break;
            default:
                break;
        }
    }

    private bool IsPassable(Vector2Int position)
    {
        if (Grid.HasPriority(position, EntityPriority.Furniture) ||
            Grid.HasPriority(position, EntityPriority.Characters))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTravelToRoaster()
    {
        if (!IsMoving)
        {
            Roaster = Grid.FindNearestEntity<EntityRoasteryMachineOne>(Position);

            if (Roaster == null)
            {
                CurrentState = State.TravelToBrewer;
                Debug.LogWarning("No Roaster Found");
            }
            else if ((Position - Roaster.Position).magnitude < 1.5f)
            {
                CurrentState = State.FillRoaster;
                Debug.LogWarning("At roaster");
            }
            else
            {
                bool found = Grid.Pathfind(Position, Roaster.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
                else
                {
                    Debug.LogWarning("Finished State");

                    CurrentState = State.FillRoaster;
                }
            }
        }
    }

    private void OnFillRoaster()
    {
        Debug.Log("Fill roaster?");

        if (Roaster != null)
        {
            if (!Roaster.isFilling)
            {
                Debug.Log("Roaster is not filling");
                // not filling
                if (Roaster.IsBelowFillThreshold())
                {
                    // not filling and below threshold
                    Debug.Log("Filling roaster");
                    Roaster.StartFilling();
                }
                else
                {
                    // not filling and above threshold
                    CurrentState = State.TravelToBrewer;
                }
            }
        }
        else 
        {
            CurrentState = State.TravelToBrewer;
        }
    }

    private void OnTravelToBrewer()
    {
        if (!IsMoving)
        {
            Brewer = Grid.FindNearestEntity<EntityBrewingMachineOne>(Position);

            if (Brewer == null)
            {
                CurrentState = State.TravelToEspresso;
                Debug.LogWarning("No Brewer Found");
            }
            else if ((Position - Brewer.Position).magnitude < 1.5f)
            {
                CurrentState = State.FillBrewer;
                Debug.LogWarning("At Brewer");
            }
            else
            {
                bool found = Grid.Pathfind(Position, Brewer.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
                else
                {
                    Debug.LogWarning("Finished State");

                    CurrentState = State.FillBrewer;
                }
            }
        }
    }

    private void OnFillBrewer()
    {
        Debug.Log("Fill brewer?");

        if (Brewer != null)
        {
            if (!Brewer.isFilling)
            {
                Debug.Log("Brewer is not filling");
                // not filling
                if (Brewer.IsBelowFillThreshold())
                {
                    // not filling and below threshold
                    Debug.Log("Brewer roaster");
                    Brewer.StartFilling();
                }
                else
                {
                    // not filling and above threshold
                    CurrentState = State.TravelToEspresso;
                }
            }
        }
        else
        {
            CurrentState = State.TravelToEspresso;
        }
    }

    private void OnTravelToEspresso()
    {
        if (!IsMoving)
        {
            Espresso = Grid.FindNearestEntity<EntityEspressoMachineOne>(Position);

            if (Espresso == null)
            {
                CurrentState = State.TravelToRoaster;
                Debug.LogWarning("No Espresso Found");
            }
            else if ((Position - Espresso.Position).magnitude < 1.5f)
            {
                CurrentState = State.FillEspresso;
                Debug.LogWarning("At Espresso");
            }
            else
            {
                bool found = Grid.Pathfind(Position, Espresso.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
                else
                {
                    Debug.LogWarning("Finished State");

                    CurrentState = State.FillEspresso;
                }
            }
        }
    }

    private void OnFillEspresso()
    {
        if (Espresso == null)
        {
            // No espresso found
            CurrentState = State.TravelToRoaster;
        }
        else
        {
            bool milkBelowThreshold = Espresso.IsMilkBelowFillThreshold();
            bool espressoBelowThreshold = Espresso.IsEspressoBelowFillThreshold();
            if (Espresso.isFillingMilk || Espresso.isFillingEspresso)
            {
                // Either milk or espresso is filling, wait
            }
            else if (milkBelowThreshold || espressoBelowThreshold)
            {
                // Start filling milk or espresso or both
                if (milkBelowThreshold) Espresso.StartFillingMilk();
                if (espressoBelowThreshold) Espresso.StartFillingEspresso();
            }
            else
            {
                // Neither milk nor espresso is filling or below threshold
                CurrentState = State.TravelToRoaster;
            }
        }
    }

    public override string OnSerialize()
    {
        SupportData vars = new SupportData();
        vars.MyState = CurrentState.ToString();
        vars.MyFast = GetEfficiencyModifier();
        vars.MySkill = GetSkillModifier();
        vars.MyWage = GetWageAmount();
        vars.MyEmployeeID = GetEmployeeID();
        vars.MyName = GetEmployeeName();
        vars.MyImageName = GetSpriteName();
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        SupportData vars = JsonUtility.FromJson<SupportData>(json);
        switch (vars.MyState)
        {
            case "TravelToRoaster":
                CurrentState = State.TravelToRoaster;
                break;
            case "FillRoaster":
                CurrentState= State.FillRoaster;
                break;
            case "TravelToBrewer":
                CurrentState = State.TravelToBrewer;
                break;
            case "FillBrewer":
                CurrentState = State.FillBrewer;
                break;
            case "TravelToEspresso":
                CurrentState = State.TravelToEspresso;
                break;
            case "FillEspresso":
                CurrentState = State.FillEspresso;
                break;
        }
        SetSkillModifier(vars.MySkill);
        SetWageAmount(vars.MyWage);
        SetEfficiencyModifier(vars.MyFast);
        SetEmployeeID(vars.MyEmployeeID);
        SetEmployeeName(vars.MyName);
        SetSpriteName(vars.MyImageName);
        UnityEngine.Debug.Log("Loading: " + "/" + vars.MyImageName);
        SetSpriteName(vars.MyImageName);
        //Image myImage = GetComponent<Image>();
        //myImage.sprite = Resources.Load<Sprite>(GetSpriteName());
        SetEntitySprite(Resources.Load<Sprite>(GetSpriteName()));
    }
}
