using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRegister : EntityBase
{
    [SerializeField] private EntityFront MyFront;
    [SerializeField] private EntityCustomer MyCustomer;

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
