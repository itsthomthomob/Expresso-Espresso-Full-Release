using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableWhite : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table4"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("White Table");
    }
}
