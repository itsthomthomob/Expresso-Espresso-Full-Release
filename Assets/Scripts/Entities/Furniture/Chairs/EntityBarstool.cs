using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarstool : EntityBase
{

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_back"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Red Barstool");
    }
}
