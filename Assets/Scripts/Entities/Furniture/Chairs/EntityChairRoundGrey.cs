using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairRoundGrey : EntityBase
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
        GreyChairVars vars = new GreyChairVars();
        if (GetMyCustomer() != null)
        {
            vars.MyCustomersID = GetMyCustomer().MyCustomerID;

        }
        return JsonUtility.ToJson(vars);
    }
    public override void OnDeserialize(string json)
    {
        GreyChairVars vars = JsonUtility.FromJson<GreyChairVars>(OnSerialize());
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
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_front"));
                break;
            case 90:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_left"));
                break;
            case 180:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_back"));
                break;
            case 270:
                SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_right"));
                break;
        }
    }

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Round Grey Chair");
    }
}
