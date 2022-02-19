using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorSix : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor5"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Checkered Floor");
	}
}
