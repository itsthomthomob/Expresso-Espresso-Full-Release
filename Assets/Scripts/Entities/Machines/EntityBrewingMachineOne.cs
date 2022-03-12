using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrewingMachineOne : EntityBase
{
	[Header("Inventory")]
	public int BrewedCoffeeUnits;
	public int BrewedCoffeeLimit = 64;
    public int LevelOneThreshold = 20;

    [Header("Information")]
    public int HalfBatch = 32;
    public int FullBatch = 64;
    public float ForHalfBatch = 20.0f;
    public float ForFullBatch = 40.0f;
    public float ForEachUnit = 0.25f;
    public bool isFilling;
    public float StartTime;
    public TimeManager GetTime;

    private void Awake()
    {
        gameObject.AddComponent<InspectorHelper>();
        GetTime = FindObjectOfType<TimeManager>();
    }

    private void FixedUpdate()
    {
        float CurrentTime = Time.time * GetTime.scale;
        if (isFilling)
        {
            if (BrewedCoffeeUnits >= BrewedCoffeeLimit)
            {
                isFilling = false;
            }
            Debug.Log("Roaster: Filling... \n" + CurrentTime + " - " + StartTime);
            if (CurrentTime - StartTime > ForEachUnit)
            {
                Debug.Log("Adding bags...");
                if (BrewedCoffeeUnits < BrewedCoffeeLimit)
                {
                    BrewedCoffeeUnits += 1;
                }
                StartTime = CurrentTime;
            }
        }
    }

    public void StartFilling()
    {
        StartTime = Time.time * GetTime.scale;
        isFilling = true;
    }

    public bool IsFilling()
    {
        return isFilling;
    }

    public void StopFilling()
    {
        isFilling = false;
    }

    public bool IsBelowFillThreshold()
    {
        if (BrewedCoffeeUnits == 0)
        {
            return true;
        }
        else if (BrewedCoffeeUnits > LevelOneThreshold)
        {
            return false;
        }
        else if (BrewedCoffeeUnits < 0)
        {
            return true;
        }
        else if (BrewedCoffeeUnits < LevelOneThreshold)
        {
            return true;
        }
        else
        {
            return true;
        }
    }

    public override void OnEntityAwake()
	{
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("Brewer");
	}
}
