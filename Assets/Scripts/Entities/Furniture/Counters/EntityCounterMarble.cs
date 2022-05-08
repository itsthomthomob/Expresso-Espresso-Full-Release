using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterMarble : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter1"));
        SetEntityPriority(EntityPriority.Counters);
        SetEntityName("Marble Counter");
    }
}
