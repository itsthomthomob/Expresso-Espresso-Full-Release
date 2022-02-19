using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairRough : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Rough Chair");
    }
}
