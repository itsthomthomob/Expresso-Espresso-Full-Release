using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorThree : EntityBase
{

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor2"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Checkered Floor");
	}
}
