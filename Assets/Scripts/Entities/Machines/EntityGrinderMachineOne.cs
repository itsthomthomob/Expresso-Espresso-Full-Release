using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGrinderMachineOne : EntityBase
{
	private int Cost = 500;
	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Grinder-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Grinder");
	}
	private void Awake()
	{
		gameObject.AddComponent<InspectorHelper>();
	}
}
