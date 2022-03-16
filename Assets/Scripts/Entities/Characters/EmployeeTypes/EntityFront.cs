using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class EntityFront : EntityBase
{
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private string PersonalityType = "N/A";

    private float EfficiencyModifier;
    public string GetEmployeeRole() { return "Front"; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
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
    [SerializeField] private float Range = 3.0f;
    [SerializeField] private float Speed = 0.25f;

    EntityRegister myRegister;

    List<Sprite> FrontTexts = new List<Sprite>();
    GameObject MyTextBubble;

    Stopwatch TextWatch = new Stopwatch();
    private void Awake()
    {
        GetTime = FindObjectOfType<TimeManager>();
        string[] FileNames = new string[] {"Front-Text1", "Front-Text2", "Front-Text3" };
        UnityEngine.Debug.Log(FileNames[0]);
        for (int i = 0; i < FileNames.Length; i++)
        {
            Sprite LoadSprite = Resources.Load("Sprites/UI/TextBubbles/Front/" + FileNames[i]) as Sprite;
            if (!FrontTexts.Contains(LoadSprite))
            {
                FrontTexts.Add(LoadSprite);
            }
        }
    }
    private void FixedUpdate()
    {
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

    private void OnTravelToRegister()
    {
        if (!IsMoving)
        {
            myRegister = Grid.FindNearestEntity<EntityRegister>(Position);

            if (myRegister == null)
            {
                // Do nothing, wait
            }
            else if ((Position - myRegister.Position).magnitude < Range)
            {
                CurrentState = State.WaitForCustomer;
                UnityEngine.Debug.LogWarning("At register");
            }
            else
            {
                bool found = Grid.Pathfind(Position, myRegister.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Finished State");

                    CurrentState = State.WaitForCustomer;
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

                var TextImage = textBubble.transform.GetChild(0).GetComponent<Image>();
                TextImage.sprite = FrontTexts[index];
                UnityEngine.Debug.Log("Front: " + FrontTexts[index].name);
            }
            else
            {
                if (TextWatch.Elapsed >= new TimeSpan(0, 0, 2))
                {
                    // Wait for next customer
                    Destroy(MyTextBubble);
                    CurrentState = State.WaitForCustomer;
                    TextWatch.Reset();
                }
            }
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
}
