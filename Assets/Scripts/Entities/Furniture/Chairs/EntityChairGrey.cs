using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairGrey : EntityBase
{
    public override string GetEntityName()
    {
        return "Grey Chair";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_left");
    }
}
