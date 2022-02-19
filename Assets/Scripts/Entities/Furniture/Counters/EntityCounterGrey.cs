using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterGrey : EntityBase
{

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Grey Counter");
    }
}
