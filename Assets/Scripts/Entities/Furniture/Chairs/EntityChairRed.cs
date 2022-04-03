using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RedChairVars 
{
    public int MyCustomersID;
}

public class EntityChairRed : EntityBase
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
        RedChairVars vars = new RedChairVars();
        if (GetMyCustomer() != null)
        {
        vars.MyCustomersID = GetMyCustomer().MyCustomerID;

        }
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        RedChairVars vars = JsonUtility.FromJson<RedChairVars>(OnSerialize());
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
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sred chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Red Chair");
    }
}
