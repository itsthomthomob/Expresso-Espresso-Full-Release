using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableSmooth : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Smooth Table");
    }
}
