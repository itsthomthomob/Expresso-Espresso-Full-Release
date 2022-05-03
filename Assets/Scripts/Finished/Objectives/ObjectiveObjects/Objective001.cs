using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective001 : ObjectiveObject
{

    TileConstruction getTiles;

    private void Awake()
    {
        SetObj("Build Floor Tiles");
        SetID(001);
        SetMinimum(0);
        SetMaximum(30);
        SetSpawned(false);
        SetStatus(Status.New);
    }

    private void Start()
    {
        getTiles = FindObjectOfType<TileConstruction>();
    }

    private void FixedUpdate()
    {
        RequirementsMet();
        SetMinimum(getTiles.AllFloors.Count + loadedMin);
    }
    public void RequirementsMet() 
    {
        if (getTiles.AllFloors.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (getTiles.AllFloors.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else 
        {
            SetStatus(Status.New);
        }
    }
}
