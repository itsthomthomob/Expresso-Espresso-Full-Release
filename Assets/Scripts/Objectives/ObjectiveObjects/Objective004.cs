using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective004 : ObjectiveObject
{

    private void Awake()
    {
        SetObj("Buy a Register");
        SetID(004);
        SetMinimum(0);
        SetMaximum(1);
        SetSpawned(false);
        SetStatus(Status.New);
    }

    private void Update()
    {
        RequirementsMet();

        EntityRegister GetRegister = FindObjectOfType<EntityRegister>();
        if (GetRegister != null) 
        {
            SetMinimum(1);
        }
    }

    private void RequirementsMet()
    {
        EntityRegister GetRegister = FindObjectOfType<EntityRegister>();

        if (GetRegister != null)
        {
            SetStatus(Status.Finished);
        }
        else
        {
            SetStatus(Status.New);
        }
    }
}
