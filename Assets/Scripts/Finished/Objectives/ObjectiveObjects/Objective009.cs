using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective009 : ObjectiveObject
{
    EmployeeListManager employeeManagementSystem;
    private void Awake()
    {
        SetObj("Hire a Support");
        SetID(009);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        employeeManagementSystem = FindObjectOfType<EmployeeListManager>();
    }

    void Update()
    {
        CheckRequirements();
        SetMinimum(employeeManagementSystem.hiredSupports.Count);
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
