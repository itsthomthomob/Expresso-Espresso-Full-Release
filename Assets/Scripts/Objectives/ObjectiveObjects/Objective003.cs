using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective003 : ObjectiveObject
{
    List<EntityBase> AllCounters = new List<EntityBase>();

    private void Awake()
    {
        SetObj("Build Counter Tiles");
        SetID(003);
        SetMinimum(0);
        SetMaximum(5);
        SetSpawned(false);
        SetStatus(Status.New);
    }

    private void Update()
    {
        CheckRequirements();
        RequirementsMet();
        SetMinimum(AllCounters.Count + loadedMin);
    }

    private void CheckRequirements() 
    {
        EntityCounterGrey[] AllCounterGreys = FindObjectsOfType<EntityCounterGrey>();
        EntityCounterMarble[] AllCounterMarbles = FindObjectsOfType<EntityCounterMarble>();
        EntityCounterRed[] AllCounterReds = FindObjectsOfType<EntityCounterRed>();

        for (int i = 0; i < AllCounterGreys.Length; i++)
        {
            if (!AllCounters.Contains(AllCounterGreys[i]))
            {
                AllCounters.Add(AllCounterGreys[i]);
            }
        }
        for (int i = 0; i < AllCounterMarbles.Length; i++)
        {
            if (!AllCounters.Contains(AllCounterMarbles[i]))
            {
                AllCounters.Add(AllCounterMarbles[i]);
            }
        }
        for (int i = 0; i < AllCounterReds.Length; i++)
        {
            if (!AllCounters.Contains(AllCounterReds[i]))
            {
                AllCounters.Add(AllCounterReds[i]);
            }
        }
    }

    private void RequirementsMet() 
    {
        if (AllCounters.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (AllCounters.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else
        {
            SetStatus(Status.New);
        }
    }
}
