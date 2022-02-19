using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorTwo : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor1"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Checkered Floor");
	}
}
