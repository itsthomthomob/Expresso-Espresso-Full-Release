using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective001 : ObjectiveObject
{
    [Header("Attributes")]
    EntityFloor[] AllFloorOne;
    EntityFloorTwo[] AllFloorTwo;
    EntityFloorThree[] AllFloorThree;
    EntityFloorFour[] AllFloorFour;
    EntityFloorFive[] AllFloorFive;
    EntityFloorSix[] AllFloorSix;
    EntityFloorSeven[] AllFloorSeven;

    List<EntityBase> AllFloors = new List<EntityBase>();

    private void Awake()
    {
        SetObj("Build Floor Tiles");
        SetID(001);
        SetMinimum(0);
        SetMaximum(30);
        SetSpawned(false);
        SetStatus(Status.New);
    }

    private void Update()
    {
        CheckRequirements();
        RequirementsMet();
        SetMinimum(AllFloors.Count + loadedMin);
    }

    void CheckRequirements() 
    {
        AllFloorOne = FindObjectsOfType<EntityFloor>();
        AllFloorTwo = FindObjectsOfType<EntityFloorTwo>();
        AllFloorThree = FindObjectsOfType<EntityFloorThree>();
        AllFloorFour = FindObjectsOfType<EntityFloorFour>();
        AllFloorFive = FindObjectsOfType<EntityFloorFive>();
        AllFloorSix = FindObjectsOfType<EntityFloorSix>();
        AllFloorSeven = FindObjectsOfType<EntityFloorSeven>();

        for (int i = 0; i < AllFloorOne.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorOne[i]))
            {
                AllFloors.Add(AllFloorOne[i]);
            }
        }
        for (int i = 0; i < AllFloorTwo.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorTwo[i]))
            {
                AllFloors.Add(AllFloorTwo[i]);
            }
        }
        for (int i = 0; i < AllFloorThree.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorThree[i]))
            {
                AllFloors.Add(AllFloorThree[i]);
            }
        }
        for (int i = 0; i < AllFloorFour.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorFour[i]))
            {
                AllFloors.Add(AllFloorFour[i]);
            }
        }
        for (int i = 0; i < AllFloorFive.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorFive[i]))
            {
                AllFloors.Add(AllFloorFive[i]);
            }
        }
        for (int i = 0; i < AllFloorSix.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorSix[i]))
            {
                AllFloors.Add(AllFloorSix[i]);
            }
        }
        for (int i = 0; i < AllFloorSeven.Length; i++)
        {
            if (!AllFloors.Contains(AllFloorSeven[i]))
            {
                AllFloors.Add(AllFloorSeven[i]);
            }
        }

        // Check for destroyed
        for (int i = 0; i < AllFloors.Count; i++)
        {
            if (AllFloors[i] == null)
            {
                AllFloors.Remove(AllFloors[i]);
            }
        }

    }

    public void RequirementsMet() 
    {
        if (AllFloors.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (AllFloors.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else 
        {
            SetStatus(Status.New);
        }
    }
}
