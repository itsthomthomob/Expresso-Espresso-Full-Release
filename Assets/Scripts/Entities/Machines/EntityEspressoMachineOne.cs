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
	private TimeSpan ForEachUnit = new TimeSpan(0, 0, 1);
	public bool isFillingMilk;
	public bool isFillingEspresso;
	public float StartTime;

	public Stopwatch EspressoWatch = new Stopwatch();
	public Stopwatch MilkWatch = new Stopwatch();

	EntityBarista myBarista;

    private void Awake()
    {
        gameObject.AddComponent<InspectorHelper>();
	}

	public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Espresso-Machine");
	}

    private void FixedUpdate()
    {
		if (isFillingMilk)
		{
			if (MilkUnits >= MilkUnitsLimit)
			{
				isFillingMilk = false;

				MilkWatch.Reset();
				MilkWatch.Stop();

			}
			else if (MilkWatch.Elapsed > ForEachUnit)
			{
				MilkUnits += 1;

				MilkWatch.Reset();
				MilkWatch.Start();
			}
		}

		if (isFillingEspresso)
		{

			if (EspressoUnits >= EspressoUnitsLimit)
			{
				isFillingEspresso = false;

				EspressoWatch.Reset();
				EspressoWatch.Stop();
			}
			else if (EspressoWatch.Elapsed > ForEachUnit)
			{
				EspressoUnits += 1;

				EspressoWatch.Reset();
				EspressoWatch.Start();
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
		EspressoWatch.Start();
		isFillingEspresso = true;
	}
	public void StartFillingMilk()
	{
		MilkWatch.Start();
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
		EspressoWatch.Stop();
		isFillingEspresso = false;
	}
	public void StopFillingMilk()
	{
		EspressoWatch.Stop();
		isFillingMilk = false;
	}
}
