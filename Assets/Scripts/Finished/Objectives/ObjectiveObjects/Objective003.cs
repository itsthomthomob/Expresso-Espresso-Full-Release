using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective003 : ObjectiveObject
{
    TileConstruction getTiles;

    private void Awake()
    {
        SetObj("Build Counter Tiles");
        SetID(003);
        SetMinimum(0);
        SetMaximum(5);
        SetSpawned(false);
        SetStatus(Status.New);
    }
    private void Start()
    {
        getTiles = FindObjectOfType<TileConstruction>();
    }
    private void Update()
    {
        RequirementsMet();
        SetMinimum(getTiles.AllCounters.Count + loadedMin);
    }

    private void RequirementsMet() 
    {
        if (getTiles.AllCounters.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (getTiles.AllCounters.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else
        {
            SetStatus(Status.New);
        }
    }
}
