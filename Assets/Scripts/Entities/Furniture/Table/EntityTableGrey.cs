using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableGrey : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table2"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Grey Table");
    }
}
