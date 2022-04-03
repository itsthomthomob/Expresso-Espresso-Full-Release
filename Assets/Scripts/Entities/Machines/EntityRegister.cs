using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RegisterVars
{
    public int MyFrontsID; // Employee ID
    public int MyCustomersID;
}

public class EntityRegister : EntityBase
{
    [SerializeField] private EntityFront MyFront;
    [SerializeField] private EntityCustomer MyCustomer;

    public override string OnSerialize()
    {
        RegisterVars vars = new RegisterVars();
        if (GetCustomer() != null)
        {
            vars.MyCustomersID = GetCustomer().MyCustomerID;
            vars.MyFrontsID = GetFront().GetEmployeeID();
        }
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        RegisterVars vars = JsonUtility.FromJson<RegisterVars>(OnSerialize());
        // Characters spawn before machines
        EntityFront[] AllFronts = FindObjectsOfType<EntityFront>();
        for (int i = 0; i < AllFronts.Length; i++)
        {
            if (AllFronts[i].GetEmployeeID() == vars.MyFrontsID)
            {
                SetFront(AllFronts[i]);
            }
        }
        EntityCustomer[] AllCustomers = FindObjectsOfType<EntityCustomer>();
        for (int i = 0; i < AllCustomers.Length; i++)
        {
            if (AllCustomers[i].MyCustomerID == vars.MyCustomersID)
            {
                SetCustomer(AllCustomers[i]);
            }
        }
    }


    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Register"));
        SetEntityPriority(EntityPriority.Appliances);
        SetEntityName("Register");
    }

    public void SetFront(EntityFront front) 
    {
        MyFront = front;
    }

    public EntityFront GetFront() 
    {
        return MyFront;
    }

    public void SetCustomer(EntityCustomer customer) 
    {
        MyCustomer = customer;
    }

    public EntityCustomer GetCustomer()
    {
        return MyCustomer;
    }

    public void SetCustomerToNone() 
    {
        MyCustomer = null;
    }

}
