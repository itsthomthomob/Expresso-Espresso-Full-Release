using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Objective005 : ObjectiveObject
{
    CustomerSpawnSystem getCustomers;
    EmployeeCommunicationSystem employeeCommunicationSystem;
    private void Awake()
    {
        SetObj("Your First Customer");
        SetID(005);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        employeeCommunicationSystem = FindObjectOfType<EmployeeCommunicationSystem>();
    }

    private void FixedUpdate()
    {
        RequirementsMet();
        if (employeeCommunicationSystem.NoDrinkCustomers.Count > 0)
        {
            SetMinimum(employeeCommunicationSystem.NoDrinkCustomers.Count);
        }
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
