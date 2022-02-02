using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRegister : EntityBase
{
    public override string GetEntityName()
    {
        return "Register";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Appliances;
        //
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Machines/Register");
    }
}
