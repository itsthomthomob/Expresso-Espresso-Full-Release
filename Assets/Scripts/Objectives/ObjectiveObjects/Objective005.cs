using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective005 : ObjectiveObject
{
    CustomerSpawnSystem getCustomers;
    private void Awake()
    {
        SetObj("Your First Customer");
        SetID(005);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        getCustomers = FindObjectOfType<CustomerSpawnSystem>();
    }

    private void FixedUpdate()
    {
        RequirementsMet();
        SetMinimum(0);
    }

    private void RequirementsMet()
    {
        if (GetMinimum() == 1)
        {
            SetStatus(Status.Finished);
        }
        else
        {
            SetStatus(Status.New);
        }
    }
}
