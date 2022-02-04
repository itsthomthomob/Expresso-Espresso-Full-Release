using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterRed : EntityBase
{
    public override string GetEntityName()
    {
        return "Red Counter";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter2");
    }
}
