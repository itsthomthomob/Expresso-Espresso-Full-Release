using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective002 : ObjectiveObject
{
    List<EntityBase> AllWalls = new List<EntityBase>();

    private void Awake()
    {
        SetObj("Build Wall Tiles");
        SetID(002);
        SetMaximum(30);
        SetSpawned(false);
        SetStatus(Status.New);
    }

    private void Update()
    {
        CheckRequirements();
        RequirementsMet();
        SetMinimum(AllWalls.Count + loadedMin);
    }

    private void CheckRequirements() 
    { 
        EntityWallBrick[] AllWallBricks = FindObjectsOfType<EntityWallBrick>();
        EntityWallGreyBrick[] AllWallGreyBricks = FindObjectsOfType<EntityWallGreyBrick>();
        EntityWallPale[] AllWallPale = FindObjectsOfType<EntityWallPale>();
        EntityWallPlaster[] AllWallPlaster = FindObjectsOfType<EntityWallPlaster>();

        for (int i = 0; i < AllWallBricks.Length; i++)
        {
            if (!AllWalls.Contains(AllWallBricks[i]))
            {
                AllWalls.Add(AllWallBricks[i]);
            }
        }
        for (int i = 0; i < AllWallGreyBricks.Length; i++)
        {
            if (!AllWalls.Contains(AllWallGreyBricks[i]))
            {
                AllWalls.Add(AllWallGreyBricks[i]);
            }
        }
        for (int i = 0; i < AllWallPale.Length; i++)
        {
            if (!AllWalls.Contains(AllWallPale[i]))
            {
                AllWalls.Add(AllWallPale[i]);
            }
        }
        for (int i = 0; i < AllWallPlaster.Length; i++)
        {
            if (!AllWalls.Contains(AllWallPlaster[i]))
            {
                AllWalls.Add(AllWallPlaster[i]);
            }
        }
    }

    private void RequirementsMet() 
    {
        if (AllWalls.Count >= GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else if (AllWalls.Count > GetMinimum())
        {
            SetStatus(Status.InProgress);
        }
        else
        {
            SetStatus(Status.New);
        }
    }

}
