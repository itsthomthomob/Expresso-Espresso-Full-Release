using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableSmooth : EntityBase
{
    public override string GetEntityName()
    {
        return "Smooth Table";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table");
    }
}
