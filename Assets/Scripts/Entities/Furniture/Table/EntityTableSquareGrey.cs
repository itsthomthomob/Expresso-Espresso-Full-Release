using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTableSquareGrey : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table3"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Square Grey Table");
    }
}
