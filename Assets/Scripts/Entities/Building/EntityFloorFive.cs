using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorFive : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/floor6");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Buildings;
	}

    public override string GetEntityName()
    {
        throw new System.NotImplementedException();
    }
}
