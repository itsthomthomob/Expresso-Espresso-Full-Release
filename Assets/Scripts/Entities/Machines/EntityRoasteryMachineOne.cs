using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityRoasteryMachineOne : EntityBase
{
	private int Cost = 5000;

	// Temperature attributes
	public int Temperature;

	// Coffee attributes
	public int AmountOfCoffeeBags = 0;
	public int LevelOneThreshold = 20;
	public int CoffeeBagLimit = 50;

    // Time attributes
    public float StartTime;
    public float TimeForBag = 0.01f;
    public float TimeNeeded = 5.0f;
    public bool isFilling;

    private void Awake()
    {
        gameObject.AddComponent<InspectorHelper>();
    }

    private void FixedUpdate()
    {
        float CurrentTime = Time.time;
        if (isFilling)
        {
            if (AmountOfCoffeeBags == CoffeeBagLimit)
            {
                isFilling = false;
            }
            Debug.Log("Roaster: Filling... \n" + CurrentTime + " - " + StartTime);
            if (CurrentTime - StartTime > TimeForBag)
            {
                Debug.Log("Adding bags...");
                AmountOfCoffeeBags += 1;

                StartTime = CurrentTime;
            }
        }
    }

    public void StartFilling()
    {
        StartTime = Time.time;
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

    /// <summary>
    /// Used by Support, checks if the amount of coffee bags is below the threshold
    /// </summary>
    /// <returns></returns>
    public bool IsBelowFillThreshold()
    {
        if (AmountOfCoffeeBags == 0)
        {
            return true;
        }
        else if (AmountOfCoffeeBags > LevelOneThreshold)
        {
            return false;
        }
        else if (AmountOfCoffeeBags < 0)
        {
            return true;
        }
        else if (AmountOfCoffeeBags < LevelOneThreshold)
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
		SetEntitySprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery"));
		SetEntityPriority(EntityPriority.Appliances);
		SetEntityName("RoasterLvl1");
	}
}
