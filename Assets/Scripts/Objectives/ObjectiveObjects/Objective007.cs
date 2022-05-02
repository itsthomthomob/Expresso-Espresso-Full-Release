using System.Collections;
using UnityEngine;

public class Objective007 : ObjectiveObject
{

    EmployeeListManager getEmployees;
    private void Awake()
    {
        SetObj("Hire a Front");
        SetID(007);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
        getEmployees = FindObjectOfType<EmployeeListManager>();
    }

    void Update()
    {
        CheckRequirements();
        SetMinimum(getEmployees.hiredFronts.Count);
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