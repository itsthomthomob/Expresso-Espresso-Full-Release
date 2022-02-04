using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairSmooth : EntityBase
{
    public override string GetEntityName()
    {
        return "Smooth Chair";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left");
    }
}
