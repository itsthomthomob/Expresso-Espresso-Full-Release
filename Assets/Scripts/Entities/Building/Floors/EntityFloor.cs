using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloor : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor");
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
