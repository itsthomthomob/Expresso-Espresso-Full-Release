using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarstool : EntityBase
{
    public override string GetEntityName()
    {
        return "Barstool-1";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_back");
    }
}
