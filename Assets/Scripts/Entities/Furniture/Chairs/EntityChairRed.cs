using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairRed : EntityBase
{
    public override string GetEntityName()
    {
        return "Red Chair";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/sred chair_left");
    }
}
