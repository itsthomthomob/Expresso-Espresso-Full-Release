using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrewingMachineOne : EntityBase
{
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Brewer");
	}
}
