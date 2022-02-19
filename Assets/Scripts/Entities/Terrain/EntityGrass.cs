using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGrass : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Grass"));
        SetEntityPriority(EntityPriority.Terrain);
        SetEntityName("Grass");
    }
}
