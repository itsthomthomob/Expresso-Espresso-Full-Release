using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFront : EntityBase
{
    public override string GetEntityName()
    {
        return "Front";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Characters;
    }

    protected override Sprite GetEntitySprite()
    {
        int i = 1;
        Sprite[] Sprites = { };


        return Sprites[i];
    }
}
