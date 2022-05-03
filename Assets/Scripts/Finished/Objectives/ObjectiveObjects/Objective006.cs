using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective006 : ObjectiveObject
{
    EmployeeListManager getEmployees;
    private void Awake()
    {
        SetObj("Hire a Barista");
        SetID(006);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        getEmployees = FindObjectOfType<EmployeeListManager>();
    }

    private void Update()
    {
        CheckRequirements();
        SetMinimum(getEmployees.hiredBaristas.Count);
    }

    private void CheckRequirements() 
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
