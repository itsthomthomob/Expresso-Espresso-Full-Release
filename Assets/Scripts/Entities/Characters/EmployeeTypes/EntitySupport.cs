using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySupport : EntityBase
{
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private float EfficiencyModifier;
    private string PersonalityType;

    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }
    public float GetWageAmount() { return WageAmount; }

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


    public enum State 
    {
        TravelToRoaster, FillRoaster, TravelToBrewer, FillBrewer, TravelToEspresso, FillEspresso
    }

    public State CurrentState = State.TravelToRoaster;

    public EntityRoasteryMachineOne Roaster;
    public EntityBrewingMachineOne Brewer;

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Characters/Character001"));
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Support");
    }

    private void FixedUpdate()
    {
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
            Grid.HasPriority(position, EntityPriority.Characters) ||
            Grid.HasPriority(position, EntityPriority.Foundations))
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
                    Move(next, 0.25f);
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
                CurrentState = State.TravelToBrewer;
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
                    Move(next, 0.25f);
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
        //
    }

    private void OnTravelToEspresso()
    {
        //
    }

    private void OnFillEspresso()
    {
        //
    }
}
