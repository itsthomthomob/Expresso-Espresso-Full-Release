using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorFive : EntityBase
{

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor4"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Warm Wooden Floor");
	}
}
