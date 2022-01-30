using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityRoasteryMachineOne : EntityBase
{
	private int Cost = 5000;
	private void Awake()
	{
		gameObject.AddComponent<InspectorHelper>();
	}

	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Furniture;
	}

    public override string GetEntityName()
    {
		return "Roaster";
    }
}
