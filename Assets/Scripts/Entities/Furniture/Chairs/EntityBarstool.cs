using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarstoolVars 
{
    public int MyCustomersID;
}

public class EntityBarstool : EntityBase
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
        BarstoolVars vars = new BarstoolVars();
        if (GetMyCustomer() != null)
        {
        vars.MyCustomersID = GetMyCustomer().MyCustomerID;

        }
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        BarstoolVars vars = JsonUtility.FromJson<BarstoolVars>(OnSerialize());
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
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_back"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Red Barstool");
    }
}
