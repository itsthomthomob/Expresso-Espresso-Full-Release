using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterRed : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter2"));
        SetEntityPriority(EntityPriority.Counters);
        SetEntityName("Red Counter");
    }
}