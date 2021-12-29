using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorSix : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor6");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Buildings;
	}
}
