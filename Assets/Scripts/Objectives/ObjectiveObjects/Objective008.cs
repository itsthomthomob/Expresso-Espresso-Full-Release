using System.Collections;
using UnityEngine;

public class Objective008 : ObjectiveObject
{
    TileConstruction GetTiles;
    private void Awake()
    {
        SetObj("Buy Chairs");
        SetID(008);
        SetMinimum(0);
        SetMaximum(5);
        SetSpawned(false);
        SetStatus(Status.New);
        GetTiles = FindObjectOfType<TileConstruction>();
    }

    void Update()
    {
        CheckRequirements();
        SetMinimum(GetTiles.AllChairs.Count);
    }

    private void CheckRequirements()
    {
        if (GetMinimum() == GetMaximum())
        {
            SetStatus(Status.Finished);
        }
        else
        {
            SetStatus(Status.New);
        }
    }
}