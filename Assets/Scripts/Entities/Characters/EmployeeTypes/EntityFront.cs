using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FrontData 
{
    public int MyEmployeeID;
    public float MyWage;
    public string MyName;
    public string MyState;
    public float MySkill;
    public float MyFast;
    public string MySpriteName;
}

public class EntityFront : EntityBase
{
    public float maxRotation = 5f;
    public float rotationSpeed = 6.5f;
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private string PersonalityType = "N/A";
    Vector2Int oldPos = new Vector2Int();

    private float EfficiencyModifier;
    public string GetEmployeeRole() { return "Front"; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public void SetSpriteName(string newSprite) { SpriteName = newSprite; UnityEngine.Debug.Log("Set Sprite Name to: " + newSprite); }
    public string GetSpriteName() { return SpriteName; }
    public void SetEmployeeName(string newName) { EmployeeName = newName; }
    public string GetEmployeeName() { return EmployeeName; }
    public void SetSkillModifier(float newSkill) { SkillModifier = newSkill; }
    public float GetSkillModifier() { return SkillModifier; }
    public void SetEfficiencyModifier(float newEff) { EfficiencyModifier = newEff; }
    public float GetEfficiencyModifier() { return EfficiencyModifier; }
    public string GetEmployeePersonality() { return PersonalityType; }
    public void SetEmployeePersonality(string newPers) { PersonalityType = newPers; }

    public float GetWageAmount() { return WageAmount; }
    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }

    public TimeManager GetTime;
    public override void OnEntityAwake()
    {
        EmployeeUserIntSystem GetEmployeeSystem = FindObjectOfType<EmployeeUserIntSystem>();
        SetEntitySprite(GetEmployeeSystem.CurrentCharacterImage);
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Front");
    }

    public enum State 
    { 
        TravelToRegister, WaitForCustomer, TakeOrder, DisplayText
    }

    [SerializeField] private State CurrentState = State.TravelToRegister;
    [SerializeField] private float Range = 4.0f;
    [SerializeField] private float Speed = 0.25f;

    EntityRegister myRegister;

    List<Image> FrontTexts = new List<Image>();
    GameObject MyTextBubble;

    Stopwatch TextWatch = new Stopwatch();
    private void Awake()
    {
        GetTime = FindObjectOfType<TimeManager>();
        string[] FileNames = new string[] {"Front-Text1.png", "Front-Text2.png", "Front-Text3.png" };
        UnityEngine.Debug.Log(FileNames[0]);
        for (int i = 0; i < FileNames.Length; i++)
        {
            Image LoadSprite = Resources.Load("Sprites/UI/TextBubbles/Front/" + FileNames[i]) as Image;
            if (!FrontTexts.Contains(LoadSprite))
            {
                FrontTexts.Add(LoadSprite);
            }
        }
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
            case State.TravelToRegister:
                OnTravelToRegister();
                break;
            case State.WaitForCustomer:
                OnWaitForCustomer();
                break;
            case State.TakeOrder:
                OnTakeOrder();
                break;
            case State.DisplayText:
                OnDisplayText();
                break;
        }
    }

    IEnumerator CheckIfStuck()
    {
        while (IsMoving)
        {
            // AI is moving
            yield return new WaitForSeconds(3);
            if (CurrentState == State.TravelToRegister)
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
    private void OnTravelToRegister()
    {
        if (!IsMoving)
        {
            myRegister = Grid.FindNearestEntity<EntityRegister>(Position);

            if (myRegister == null)
            {
                // Do nothing, wait
                myRegister = Grid.FindNearestEntity<EntityRegister>(Position);
                UnityEngine.Debug.Log("Can't find register");
            }
            else if (Position == new Vector2Int(myRegister.Position.x, myRegister.Position.y + 1))
            {
                CurrentState = State.WaitForCustomer;
                UnityEngine.Debug.LogWarning("At register");
            }
            else
            {
                bool found = Grid.Pathfind(Position, new Vector2Int(myRegister.Position.x, myRegister.Position.y + 1), IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
            }
        }
    }

    private void OnWaitForCustomer()
    {
        if (myRegister != null)
        {
            if (myRegister.GetCustomer() == null)
            {
                // Wait for customer or until customer is at register
            }
            else if ((myRegister.GetCustomer().Position - myRegister.Position).magnitude < Range)
            {
                CurrentState = State.TakeOrder;
            }
        }
    }

    private void OnTakeOrder()
    {
        if (myRegister != null)
        {
            if (myRegister.GetCustomer() != null)
            {
                EmployeeCommunicationSystem GetCOMMS = FindObjectOfType<EmployeeCommunicationSystem>();

                if (!GetCOMMS.NoDrinkCustomers.Contains(myRegister.GetCustomer()))
                {
                    GetCOMMS.NoDrinkCustomers.Add(myRegister.GetCustomer());
                }

                if ((myRegister.GetCustomer().Position - myRegister.Position).magnitude < Range)
                {
                    CurrentState = State.DisplayText;
                }
            }
            else
            {
                // wait for customer
            }
        }
    }

    private void OnDisplayText()
    {
        if (myRegister != null)
        {
            if (!TextWatch.IsRunning)
            {
                TextWatch.Reset();
                TextWatch.Start();
            }

            // spawn text bubble
            if (MyTextBubble == null)
            {
                GameObject textBubble = Instantiate(Resources.Load<GameObject>("Sprites/UI/TextBubbles/TextBubble"));
                MyTextBubble = textBubble;
                textBubble.transform.position = new Vector3(transform.position.x, transform.position.y + 50, -10);
                textBubble.transform.SetParent(this.transform);

                // get random customer text
                int index = UnityEngine.Random.Range(0, (FrontTexts.Count));

                Image TextImage = textBubble.transform.GetChild(0).GetComponent<Image>();
                TextImage = FrontTexts[index];
            }
            else
            {
                if (TextWatch.Elapsed >= new TimeSpan(0, 0, 2))
                {
                    // Wait for next customer
                    Destroy(MyTextBubble);
                    CurrentState = State.WaitForCustomer;
                    TextWatch.Stop();
                }
            }
        }
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
        FrontData vars = new FrontData();
        vars.MyState = CurrentState.ToString();
        vars.MyFast = GetEfficiencyModifier();
        vars.MySkill = GetSkillModifier();
        vars.MyWage = GetWageAmount();
        vars.MyEmployeeID = GetEmployeeID();
        vars.MyName = GetEmployeeName();
        vars.MySpriteName = GetSpriteName();
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        FrontData vars = JsonUtility.FromJson<FrontData>(json);
        switch (vars.MyState)
        {
            case "TravelToRegister":
                CurrentState = State.TravelToRegister;
                break;
            case "WaitForCustomer":
                CurrentState = State.WaitForCustomer;
                break;
            case "TakeOrder":
                CurrentState = State.TakeOrder;
                break;
            case "DisplayText":
                CurrentState = State.DisplayText;
                break;
        }
        SetSkillModifier(vars.MySkill);
        SetWageAmount(vars.MyWage);
        SetEfficiencyModifier(vars.MyFast);
        SetEmployeeID(vars.MyEmployeeID);
        SetEmployeeName(vars.MyName);
        SetSpriteName(vars.MySpriteName);
        UnityEngine.Debug.Log("Loading: " + vars.MySpriteName);
        SetSpriteName(vars.MySpriteName);
        //Image myImage = GetComponent<Image>();
        //myImage.sprite = Resources.Load<Sprite>(GetSpriteName());
        SetEntitySprite(Resources.Load<Sprite>(GetSpriteName()));
    }
}
