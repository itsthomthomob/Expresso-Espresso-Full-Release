using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFloorFour : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor3"));
		SetEntityPriority(EntityPriority.Foundations);
		SetEntityName("Pale Diagnal Floor");
	}
}
