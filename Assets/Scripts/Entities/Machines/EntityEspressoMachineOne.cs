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


    public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Espresso-Machine");
	}

    private void FixedUpdate()
    {
		UnityEngine.Debug.Log("Calling fixed update, ");
		UnityEngine.Debug.Log("isFillingMilk: " + isFillingMilk);
		UnityEngine.Debug.Log("isFillingEspresso: " + isFillingEspresso);

		if (isFillingMilk)
		{
            UnityEngine.Debug.Log("Filling milk..");
			if (MilkUnits >= MilkUnitsLimit)
			{
				isFillingMilk = false;
				UnityEngine.Debug.Log("Stopped filling milk");

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
			UnityEngine.Debug.Log("Filling espresso..");

			if (EspressoUnits >= EspressoUnitsLimit)
			{
				isFillingEspresso = false;
				UnityEngine.Debug.Log("Stopped filling espresso");

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
