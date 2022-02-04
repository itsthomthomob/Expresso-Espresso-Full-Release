using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCounterGrey : EntityBase
{
    public override string GetEntityName()
    {
        return "Grey Counter";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter");
    }
}
