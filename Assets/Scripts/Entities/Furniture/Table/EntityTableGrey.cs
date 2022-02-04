using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableGrey : EntityBase
{
    public override string GetEntityName()
    {
        return "Grey Table";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table2");
    }
}
