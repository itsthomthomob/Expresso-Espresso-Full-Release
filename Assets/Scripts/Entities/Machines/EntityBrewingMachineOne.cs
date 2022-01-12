using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrewingMachineOne : EntityBase
{
	private int Cost = 2000;
	private int Level;
	private int EspressoUnits;
	private int EspressoMax = 100;
	private int EspressoMin = 0;

	private void Awake()
	{
		gameObject.AddComponent<InspectorHelper>();

		Level = 1;
		EspressoUnits = 0;
	}

	public int GetLevel()
	{
		return Level;
	}
	public void SetLevel(int newLevel)
	{
		Level = newLevel;
	}

	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front");
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Furniture;
		;
	}

    public override string GetEntityName()
    {
		return "Brewer";
	}
}
