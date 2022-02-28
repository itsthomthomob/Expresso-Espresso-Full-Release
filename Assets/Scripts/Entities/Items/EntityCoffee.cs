using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCoffee : EntityBase
{
    List<Sprite> CoffeeSprites = new List<Sprite>();
    [SerializeField] private EntityCustomer MyCustomer;
    public override void OnEntityAwake()
    {
        Object[] AllCoffeeSprites = Resources.LoadAll("Sprites/Items/CoffeeTypes", typeof(Sprite));
        for (int i = 0; i < AllCoffeeSprites.Length; i++)
        {
            CoffeeSprites.Add(AllCoffeeSprites[i] as Sprite);
        }
        int index = Random.Range(0, CoffeeSprites.Count);
        SetEntitySprite(CoffeeSprites[index]);
        SetEntityPriority(EntityPriority.Appliances);
        SetEntityName("Coffee Cup");
    }

    public void SetCustomer(EntityCustomer customer) 
    {
        MyCustomer = customer;
    }

    public EntityCustomer GetCustomer() 
    {
        return MyCustomer;
    }

}
