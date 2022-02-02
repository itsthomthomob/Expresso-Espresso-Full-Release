using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterOne : EntityBase
{
    public override string GetEntityName()
    {
        return "Counter-1";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter1");
    }
}
