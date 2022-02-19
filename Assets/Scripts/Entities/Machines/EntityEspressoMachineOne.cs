using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEspressoMachineOne : EntityBase
{
	private int Cost = 5000;
	private int Level;
	private int MilkUnits;
	private int EspressoUnits;
	private int MilkMax = 100;
	private int MilkMin = 0;
	private int EspressoMax = 100;
	private int EspressoMin = 0;

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Espresso-Machine");
	}

	private void Awake()
    {
		gameObject.AddComponent<InspectorHelper>();

		Level = 1;
		EspressoUnits = 0;
		MilkUnits = 0;
	}

	public int GetLevel()
	{
		return Level;
	}
	public void SetLevel(int newLevel)
	{
		Level = newLevel;
	}
}
