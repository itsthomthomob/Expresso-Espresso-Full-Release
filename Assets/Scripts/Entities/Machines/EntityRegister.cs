using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRegister : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Register"));
        SetEntityPriority(EntityPriority.Appliances);
        SetEntityName("Register");
    }
}
