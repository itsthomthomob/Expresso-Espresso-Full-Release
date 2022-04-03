using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CoffeeVars 
{
    public int MyCustomersID;
}


public class EntityCoffee : EntityBase
{
    List<Sprite> CoffeeSprites = new List<Sprite>();
    [SerializeField] private EntityCustomer MyCustomer;
    public override void OnEntityAwake()
    {
        UnityEngine.Object[] AllCoffeeSprites = Resources.LoadAll("Sprites/Items/CoffeeTypes", typeof(Sprite));
        for (int i = 0; i < AllCoffeeSprites.Length; i++)
        {
            CoffeeSprites.Add(AllCoffeeSprites[i] as Sprite);
        }
        int index = UnityEngine.Random.Range(0, CoffeeSprites.Count);
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

    public override string OnSerialize()
    {
        CoffeeVars vars = new CoffeeVars();
        vars.MyCustomersID = GetCustomer().MyCustomerID;
        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        CoffeeVars vars = JsonUtility.FromJson<CoffeeVars>(OnSerialize());
        EntityCustomer[] AllCustomers = FindObjectsOfType<EntityCustomer>();
        for (int i = 0; i < AllCustomers.Length; i++) 
        {
            if (AllCustomers[i].MyCustomerID == vars.MyCustomersID)
            {
                SetCustomer(AllCustomers[i]);
            }
        }
    }

}
