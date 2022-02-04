using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairRough : EntityBase
{
    public override string GetEntityName()
    {
        return "Rough Chair";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left");
    }
}
