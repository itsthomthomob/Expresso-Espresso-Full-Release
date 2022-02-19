using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChairSmooth : EntityBase
{

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left"));
        SetEntityPriority(EntityPriority.Furniture);
        SetEntityName("Smooth Chair");
    }
}
