using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGrass : EntityBase
{
    public override string GetEntityName()
    {
        return "Grass";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Terrain;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Grass");
    }
}
