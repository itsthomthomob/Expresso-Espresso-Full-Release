using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableRough : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table1"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Rough Table");
    }
}
