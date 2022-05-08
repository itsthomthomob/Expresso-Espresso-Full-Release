using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawnSystem : MonoBehaviour
{
    [Header("Controllers")]
    public EntityGrid Grid;
    public int totalCustomers;
    public List<EntityCustomer> allCustomers = new List<EntityCustomer>();

    [Header("Spawn Modifiers")]
    public int StartingCustomers = 5;
    public int customerAmount;
    public WeatherManager getWeather;
    public CafeEconomySystem getEconomy;
    public TileConstruction getTiles;
    public EntityConcrete[] allConcrete;

    [Header("Spawn Objs")]
    TimeManager GetTime;
    EmployeeListManager employees;


    private void Start()
    {
        SetObjects();
        SpawnStartingCustomers();
    }

    private void SetObjects() 
    { 
        Grid = FindObjectOfType<EntityGrid>();
        GetTime = FindObjectOfType<TimeManager>();
        allConcrete = FindObjectsOfType<EntityConcrete>();
        employees = FindObjectOfType<EmployeeListManager>();
        getEconomy = FindObjectOfType<CafeEconomySystem>();
        getTiles = FindObjectOfType<TileConstruction>();
    }

    private void Update()
    {
        
    }

    private void SpawnStartingCustomers() 
    {
        for (int i = 0; i < StartingCustomers; i++)
        {
            customerAmount += 1;
            int index = UnityEngine.Random.Range(0, allConcrete.Length);
            EntityCustomer newCustomer = Grid.Create<EntityCustomer>(allConcrete[index].Position);
            allCustomers.Add(newCustomer);
        }
    }

    private void CustomerGenerator() 
    {
        if (getTiles.AllChairs.Count > 5 && allCustomers.Count > 5)
        {
            // All starting customers have spawned and player built more than 5 chairs
        }
    }

}
