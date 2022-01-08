using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorTwo : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor3");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Buildings;
	}
}
