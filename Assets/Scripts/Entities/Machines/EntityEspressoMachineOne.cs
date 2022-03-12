using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EntityEspressoMachineOne : EntityBase
{
	public int MilkUnits;
	public int MilkUnitsLimit = 10;
	public int EspressoUnits;
	public int EspressoUnitsLimit = 10;
	private float ForEachUnit = 3.0f;
	public bool isFillingMilk;
	public bool isFillingEspresso;
	public float StartTime;

	public TimeManager GetTime;
	public float StartTimeMilk;
	public float StartTimeEspresso;

	EntityBarista myBarista;

    private void Awake()
    {
        gameObject.AddComponent<InspectorHelper>();
		GetTime = FindObjectOfType<TimeManager>();

	}

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Espresso-Machine");
	}

    private void FixedUpdate()
    {
		float CurrentTimeMilk = Time.time * GetTime.scale;
		float CurrentTimeEspresso = Time.time * GetTime.scale;

		if (isFillingMilk)
		{
			if (MilkUnits >= MilkUnitsLimit)
			{
				isFillingMilk = false;
			}
			else if (CurrentTimeMilk - StartTimeMilk > ForEachUnit)
			{
				MilkUnits += 1;
				StartTimeMilk = CurrentTimeMilk;
			}
		}

		if (isFillingEspresso)
		{

			if (EspressoUnits >= EspressoUnitsLimit)
			{
				isFillingEspresso = false;
			}
			else if (CurrentTimeEspresso - StartTimeEspresso > ForEachUnit)
			{
				EspressoUnits += 1;
				StartTimeEspresso = CurrentTimeEspresso;
			}
		}
	}

	public void SetMyBarista(EntityBarista barista) 
	{
		myBarista = barista;
	}

	public EntityBarista GetMyBarista() 
	{
		return myBarista;
	}

	public void MakeDrink() 
	{
        if (EspressoUnits > 1 && MilkUnits > 2)
        {
			EspressoUnits -= 1;
			MilkUnits -= 2; // lattes take up a lot of milk
        }
	}

	public bool IsEspressoBelowFillThreshold()
	{
		if (EspressoUnits == 0)
		{
			return true;
		}
		else if (EspressoUnits >= EspressoUnitsLimit)
		{
			return false;
		}
		else if (EspressoUnits < 0)
		{
			return true;
		}
		else if (EspressoUnits < EspressoUnitsLimit)
		{
			return true;
		}
		else
		{
			return true;
		}
	}

	public bool IsMilkBelowFillThreshold()
	{
		if (MilkUnits == 0)
		{
			return true;
		}
		else if (MilkUnits >= MilkUnitsLimit)
		{
			return false;
		}
		else if (MilkUnits < 0)
		{
			return true;
		}
		else if (MilkUnits < MilkUnitsLimit)
		{
			return true;
		}
		else
		{
			return true;
		}
	}

	public void StartFillingEspresso()
	{
		StartTimeEspresso = Time.time * GetTime.scale;
		isFillingEspresso = true;
	}
	public void StartFillingMilk()
	{
		StartTimeMilk = Time.time * GetTime.scale;
		isFillingMilk = true;
	}

	public bool IsFillingEspresso()
	{
		return isFillingEspresso;
	}
	public bool IsFillingMilk()
	{
		return isFillingMilk;
	}

	public void StopFillingEspresso()
	{
		isFillingEspresso = false;
	}
	public void StopFillingMilk()
	{
		isFillingMilk = false;
	}
}
