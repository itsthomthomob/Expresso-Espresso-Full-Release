using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairSmooth : EntityBase
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
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Smooth Chair");
    }
}
