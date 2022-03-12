using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EntityBarista : EntityBase
{
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private float EfficiencyModifier;
    private string PersonalityType = "N/A";
    public string GetEmployeeRole() { return "Barista"; }

    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }
    public float GetWageAmount(){ return WageAmount; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public void SetEmployeePersonality(string newPers) { PersonalityType = newPers; }
    public string GetEmployeePersonality() { return PersonalityType; }
    public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
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
    private void FixedUpdate()
    {
        Speed = GetTime.scale * 0.25f;
        switch (CurrentState)
        {
            case State.TravelToEspresso:
                OnTravelToEspresso();
                break;
            case State.WaitForDrink:
                OnWaitForDrink();
                break;
            case State.EspressoMakeDrink:
                OnEspressoMakeDrink();
                break;
            case State.TravelToCounter:
                OnTravelToCounter();
                break;
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
                EntityCounterGrey[] AllGreyCounters = FindObjectsOfType<EntityCounterGrey>();
                EntityCounterMarble[] AllMarbleCounters = FindObjectsOfType<EntityCounterMarble>();
                EntityCounterRed[] AllRedCounters = FindObjectsOfType<EntityCounterRed>();
                
                for (int i = 0; i < AllGreyCounters.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllGreyCounters[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        myCounter == null &&
                        AllGreyCounters[i] is EntityCounterGrey)
                    {
                        myCounter = AllGreyCounters[i];
                        break;
                    }
                }

                for (int i = 0; i < AllRedCounters.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllRedCounters[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        myCounter == null &&
                        AllRedCounters[i] is EntityCounterRed)
                    {
                        myCounter = AllRedCounters[i];
                        break;
                    }
                }

                for (int i = 0; i < AllMarbleCounters.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllMarbleCounters[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        myCounter == null &&
                        AllMarbleCounters[i] is EntityCounterMarble)
                    {
                        myCounter = AllMarbleCounters[i];
                        break;
                    }
                }
                UnityEngine.Debug.Log("Barista can not find counter.");
            }
            else if (((Position - myCounter.Position).magnitude < 1.5f)) 
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
                }
                else 
                {
                    UnityEngine.Debug.Log("Counter is not accessible.");
                }
            }
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
