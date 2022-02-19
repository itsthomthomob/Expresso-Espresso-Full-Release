using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloor : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Checkered Floor");
	}
}
