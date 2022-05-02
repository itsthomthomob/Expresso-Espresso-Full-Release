using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

[Serializable]
public class BaristaData 
{
    public int MyEmployeeID;
    public string MyNameData;
    public float MyWage;
    public float MySkill;
    public float MyFast;
    public string MyState;
    public string MyImageName;
}

public class EntityBarista : EntityBase
{
    public float maxRotation = 5f;
    public float rotationSpeed = 6.5f;
    private int EmployeeID;
    private string EmployeeName;
    public string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private float EfficiencyModifier;
    private string PersonalityType = "N/A";
    Vector2Int oldPos = new Vector2Int();
    public string GetEmployeeRole() { return "Barista"; }

    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }
    public float GetWageAmount(){ return WageAmount; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

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

    public TimeManager GetTime;
    private float Speed;
    public enum State 
    { 
        TravelToEspresso, WaitForDrink, EspressoMakeDrink, TravelToCounter
    }

    [SerializeField] private State CurrentState = State.TravelToEspresso;
    EntityEspressoMachineOne myEspresso;
    [SerializeField] private EntityBase myCounter;
    Stopwatch DrinkTimer = new Stopwatch();

    private void Awake()
    {
        GetTime = FindObjectOfType<TimeManager>();
    }

    private void Start()
    {
        StartCoroutine(CheckIfStuck());
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        Speed = 0.25f / GetTime.scale;
        
        switch (CurrentState)
        {
            case State.TravelToEspresso:
                oldPos = Position;
                OnTravelToEspresso();
                break;
            case State.WaitForDrink:
                oldPos = Position;
                OnWaitForDrink();
                break;
            case State.EspressoMakeDrink:
                oldPos = Position;
                OnEspressoMakeDrink();
                break;
            case State.TravelToCounter:
                oldPos = Position;
                OnTravelToCounter();
                break;
        }
    }

    IEnumerator CheckIfStuck()
    {
        while (IsMoving)
        {
            // AI is moving
            yield return new WaitForSeconds(3);
            if (CurrentState == State.TravelToCounter ||
                CurrentState == State.TravelToEspresso)
            {
                // Reset new position
                Vector2Int newpos = new Vector2Int();
                newpos = Position;

                if (oldPos == newpos)
                {
                    // AI is still at position
                    EntityRegister register = Grid.FindNearestEntity<EntityRegister>(Position);

                    if (register != null)
                    {
                        // Has register
                        if (!Grid.HasPriority(new Vector2Int(register.Position.x - 1, register.Position.y + 1), EntityPriority.Buildings))
                        {
                            Move(new Vector2Int(register.Position.x + 1, register.Position.y + 1), 0f);
                            UnityEngine.Debug.Log("Employee unstuck");
                        }
                        else
                        {
                            if (!Grid.HasPriority(new Vector2Int(register.Position.x + 1, register.Position.y - 1), EntityPriority.Buildings))
                            {
                                Move(new Vector2Int(register.Position.x - 1, register.Position.y + 1), 0f);
                            }
                        }
                    }
                }
                else if (oldPos != newpos)
                {
                    UnityEngine.Debug.Log("Player moved");
                }

                oldPos = newpos;
            }
        }
    }

    private void OnTravelToEspresso()
    {
        if (!IsMoving)
        {
            if (myEspresso == null)
            {
                myEspresso = Grid.FindNearestEntity<EntityEspressoMachineOne>(Position);
            }
            else if ((Position - myEspresso.Position).magnitude < 1.5f)
            {
                CurrentState = State.WaitForDrink;
                UnityEngine.Debug.LogWarning("At espresso");
            }
            else
            {
                bool found = Grid.Pathfind(Position, myEspresso.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Finished State");

                    CurrentState = State.WaitForDrink;
                }
            }
        }
    }

    private void OnWaitForDrink()
    {
        if (!IsMoving)
        {
            if (myEspresso == null)
            {
                // Find espresso machine
                CurrentState = State.TravelToEspresso;
            }
            else 
            { 
                // Check drink queue
                EmployeeCommunicationSystem GetECS = FindObjectOfType<EmployeeCommunicationSystem>();
                if (GetECS.NoDrinkCustomers.Count > 0)
                {
                    CurrentState = State.EspressoMakeDrink;
                }
            }
        }
    }

    private void OnEspressoMakeDrink()
    {
        if (!IsMoving)
        {
            if (myEspresso == null)
            {
                // Find espresso machine
                CurrentState = State.TravelToEspresso;
            }
            else 
            {
                DrinkTimer.Start();
                if (DrinkTimer.Elapsed >= new TimeSpan(0, 0, 2))
                {
                    myEspresso.MakeDrink();
                    CurrentState = State.TravelToCounter;
                    DrinkTimer.Reset();
                }
            }
        }
    }

    private void OnTravelToCounter()
    {
        if (!IsMoving)
        {
            if (myCounter == null)
            {
                EntityRegister curRegister = Grid.FindNearestEntity<EntityRegister>(Position);
                myCounter = Grid.GetFirstEntity<EntityBase>(new Vector2Int(curRegister.Position.x, curRegister.Position.y - 1));
            }
            else if (((Position - myCounter.Position).magnitude < 2.0f)) 
            {
                // At counter
                EmployeeCommunicationSystem GetECS = FindObjectOfType<EmployeeCommunicationSystem>();
                GetECS.NoDrinkCustomers.Remove(GetECS.NoDrinkCustomers[0]);
                EntityCoffee newCoffee = Grid.Create<EntityCoffee>(myCounter.Position);
                CurrentState = State.TravelToEspresso;
            }
            else
            {
                // Found counter, move
                bool found = Grid.Pathfind(Position, myCounter.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                    UnityEngine.Debug.Log("Going To: " + next);
                }
                else 
                {
                    UnityEngine.Debug.Log("Counter is not accessible.");
                }
            }
        }
    }

    private EntityBase FindNewCounter(EntityBase curEntity) 
    {
        bool found = Grid.Pathfind(Position, curEntity.Position, IsPassable, out Vector2Int next);
        if (found)
        {
            return curEntity;
        }
        else 
        {
            EntityBase findNewG = Grid.FindNearestEntity<EntityCounterGrey>(Position);
            if (findNewG == null)
            {
                EntityBase findNewM = Grid.FindNearestEntity<EntityCounterMarble>(Position);
                if (findNewM == null)
                {
                    EntityBase findNewR = Grid.FindNearestEntity<EntityCounterRed>(Position);
                    return FindNewCounter(findNewR);
                }
                return FindNewCounter(findNewM);
            }
            return FindNewCounter(findNewG);
        }
    }

    public override void OnEntityAwake()
    {
        EmployeeUserIntSystem GetEmployeeSystem = FindObjectOfType<EmployeeUserIntSystem>();
        SetEntitySprite(GetEmployeeSystem.CurrentCharacterImage);
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Barista");
    }
    private bool IsPassable(Vector2Int position)
    {
        if (Grid.HasPriority(position, EntityPriority.Furniture) ||
            Grid.HasPriority(position, EntityPriority.Buildings))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override string OnSerialize()
    {
        BaristaData vars = new BaristaData();
        vars.MyState = CurrentState.ToString();
        vars.MyFast = GetEfficiencyModifier();
        vars.MySkill = GetSkillModifier();
        vars.MyWage = GetWageAmount();
        vars.MyEmployeeID = GetEmployeeID();
        vars.MyNameData = EmployeeName;
        vars.MyImageName = GetSpriteName();
        UnityEngine.Debug.Log("Saved Image: " + vars.MyImageName);
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        BaristaData vars = JsonUtility.FromJson<BaristaData>(json);
        UnityEngine.Debug.Log("Raw Json: \n" + json);
        switch (vars.MyState)
        {
            case "TravelToEspresso":
                CurrentState = State.TravelToEspresso;
                break;
            case "WaitForDrink":
                CurrentState = State.WaitForDrink;
                break;
            case "EspressoMakeDrink":
                CurrentState = State.EspressoMakeDrink;
                break;
            case "TravelToCounter":
                CurrentState = State.TravelToCounter;
                break;
        }
        SetSkillModifier(vars.MySkill);
        SetWageAmount(vars.MyWage);
        SetEfficiencyModifier(vars.MyFast);
        SetEmployeeID(vars.MyEmployeeID);
        SetEmployeeName(vars.MyNameData);
        UnityEngine.Debug.Log("Loading: " + vars.MyImageName);
        SetSpriteName(vars.MyImageName);
        //Image myImage = GetComponent<Image>();
        //myImage.sprite = Resources.Load<Sprite>(GetSpriteName());
        SetEntitySprite(Resources.Load<Sprite>(GetSpriteName()));
    }
}
