using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableSquareWhite : EntityBase
{
    public override string GetEntityName()
    {
        return "Square White Table";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Furniture;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table5");
    }
}
