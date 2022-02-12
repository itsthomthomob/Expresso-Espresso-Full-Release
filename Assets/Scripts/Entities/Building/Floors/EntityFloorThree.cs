using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorThree : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor2");
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