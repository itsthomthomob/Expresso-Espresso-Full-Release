using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Rough Chair");
    }
}
