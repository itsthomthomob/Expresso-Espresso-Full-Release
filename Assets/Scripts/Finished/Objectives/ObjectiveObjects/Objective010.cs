using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective010 : ObjectiveObject
{
    MenuManagementSystem menuManagementSystem;
    private void Awake()
    {
        SetObj("Create a Drink");
        SetID(010);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        menuManagementSystem = FindObjectOfType<MenuManagementSystem>();
    }

    void Update()
    {
        CheckRequirements();
        SetMinimum(menuManagementSystem.AllMenuItems.Length);
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
