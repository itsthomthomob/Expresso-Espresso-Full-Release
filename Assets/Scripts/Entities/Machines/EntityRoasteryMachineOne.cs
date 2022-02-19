using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityRoasteryMachineOne : EntityBase
{
	private int Cost = 5000;

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Roaster");
	}
	private void Awake()
	{
		gameObject.AddComponent<InspectorHelper>();
	}
}
