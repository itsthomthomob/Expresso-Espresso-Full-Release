using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableSquareWhite : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table5"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Square White Table");
    }
}
