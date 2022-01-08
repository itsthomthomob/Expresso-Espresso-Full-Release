using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRoasteryMachineOne : EntityBase
{
	private int Cost = 5000;
	private int Level;
	private int MilkUnits;
	private int EspressoUnits;
	private int MilkMax = 100;
	private int MilkMin = 0;
	private int EspressoMax = 100;
	private int EspressoMin = 0;

	private void Awake()
	{
		Level = 1;
		EspressoUnits = 0;
		MilkUnits = 0;
	}

	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Furniture;
	}
}