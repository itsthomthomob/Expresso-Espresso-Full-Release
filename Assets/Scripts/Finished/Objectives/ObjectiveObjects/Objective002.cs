using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective002 : ObjectiveObject
{
    TileConstruction getTiles;
    private void Awake()
    {
        SetObj("Build Wall Tiles");
        SetID(002);
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
        SetMinimum(getTiles.AllWalls.Count + loadedMin);
    }

    private void RequirementsMet() 
    {
        if (getTiles.AllWalls.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (getTiles.AllWalls.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else
        {
            SetStatus(Status.New);
        }
    }

}
