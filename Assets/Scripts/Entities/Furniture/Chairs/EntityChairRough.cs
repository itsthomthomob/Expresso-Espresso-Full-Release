using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RoughChairVars 
{
    public int MyCustomersID;
}

public class EntityChairRough : EntityBase
{
    EntityCustomer MyCustomer;

    public EntityCustomer GetMyCustomer() 
    {
        return MyCustomer;
    }

    public void SetMyCustomer(EntityCustomer Customer) 
    {
        MyCustomer = Customer;
    }
    public override string OnSerialize()
    {
        RoughChairVars vars = new RoughChairVars();
        if (GetMyCustomer() != null)
        {
        vars.MyCustomersID = GetMyCustomer().MyCustomerID;

        }
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        RoughChairVars vars = JsonUtility.FromJson<RoughChairVars>(OnSerialize());
        EntityCustomer[] AllCustomers = FindObjectsOfType<EntityCustomer>();
        for (int i = 0; i < AllCustomers.Length; i++)
        {
            if (AllCustomers[i].MyCustomerID == vars.MyCustomersID)
            {
                SetMyCustomer(AllCustomers[i]);
            }
        }
    }
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Rough Chair");
    }
}
