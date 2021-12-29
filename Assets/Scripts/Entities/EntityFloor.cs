using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloor : EntityBase
{
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor2");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Buildings;
	}
}