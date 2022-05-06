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

    private void Start()
    {
        ItemRotationManager getRotation = FindObjectOfType<ItemRotationManager>();
        switch (getRotation.rotAmount)
        {
            case 0:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_front"));
                break;
            case 90:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_left"));
                break;
            case 180:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_back"));
                break;
            case 270:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_right"));
                break;
        }
    }

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Rough Grey Chair");
    }
}
