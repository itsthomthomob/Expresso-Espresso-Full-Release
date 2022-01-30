using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGrinderMachineOne : EntityBase
{
	private int Cost = 500;
	private void Awake()
	{
		gameObject.AddComponent<InspectorHelper>();
	}
	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Machines/Grinder-machine-1-front");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Furniture;
		;
	}

	public override string GetEntityName()
	{
		return "Grinder";
	}
}
