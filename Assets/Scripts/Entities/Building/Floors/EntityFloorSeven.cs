using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorSeven : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor6"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Pale Wooden Floor");
	}
}
