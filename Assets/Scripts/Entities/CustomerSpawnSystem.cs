using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public int spawnChance;
    public bool spawnedCustomers;
    WeatherManager getWeather;
    CafeEconomySystem getEconomy;
    TileConstruction getTiles;
    StoreLevelManager getLevel;
    public EntityConcrete[] allConcrete;

    [Header("Spawn Objs")]
    Stopwatch SpawnWatch = new Stopwatch();
    TimeManager GetTime;
    EmployeeListManager getEmployees;


    private void Start()
    {
        SetObjects();
        SpawnCustomers();
    }

    private void SetObjects() 
    {
        spawnedCustomers = false;
        Grid = FindObjectOfType<EntityGrid>();
        GetTime = FindObjectOfType<TimeManager>();
        allConcrete = FindObjectsOfType<EntityConcrete>();
        getEmployees = FindObjectOfType<EmployeeListManager>();
        getEconomy = FindObjectOfType<CafeEconomySystem>();
        getTiles = FindObjectOfType<TileConstruction>();
        getLevel = FindObjectOfType<StoreLevelManager>();
    }

    private void Update()
    {
        CustomerGenerator();
    }

    private void SpawnCustomers() 
    {
        for (int i = 0; i < StartingCustomers; i++)
        {
            customerAmount += 1;
            int index = UnityEngine.Random.Range(0, allConcrete.Length);
            EntityCustomer newCustomer = Grid.Create<EntityCustomer>(allConcrete[index].Position);
            allCustomers.Add(newCustomer);
        }
        spawnedCustomers = true;
    }

    private void CustomerGenerator() 
    {
        if (getTiles.AllChairs.Count > 5 && allCustomers.Count > 4 && getTiles.AllRegisters.Count > 0
            && getEmployees.hiredBaristas.Count > 0 && getEmployees.hiredFronts.Count > 0 && getEmployees.hiredSupports.Count > 0)
        {
            // All starting customers have spawned and player built more than 5 chairs
            if (!SpawnWatch.IsRunning)
            {
                SpawnWatch.Start();
            }

            // Spawn a new set of customers every few seconds

            if (getLevel.StoreLevel == 0)
            {
                if (SpawnWatch.Elapsed >= new TimeSpan(0, 0, 7))
                {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if (chance > spawnChance)
                    {
                        if (spawnedCustomers == true)
                        {
                            SpawnCustomers();
                            spawnedCustomers = false;
                        }
                    }
                    SpawnWatch.Restart();
                }
            }
            else if (getLevel.StoreLevel > 0 || getLevel.StoreLevel == 3) 
            {
                if (SpawnWatch.Elapsed >= new TimeSpan(0, 0, 5))
                {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if (chance > spawnChance)
                    {
                        if (spawnedCustomers == true)
                        {
                            SpawnCustomers();
                            spawnedCustomers = false;
                        }
                    }
                    SpawnWatch.Restart();
                }
            }
            else if (getLevel.StoreLevel == 4)
            {
                if (SpawnWatch.Elapsed >= new TimeSpan(0, 0, 3))
                {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if (chance > spawnChance)
                    {
                        if (spawnedCustomers == true)
                        {
                            SpawnCustomers();
                            spawnedCustomers = false;
                        }
                    }
                    SpawnWatch.Restart();
                }
            }
            else if (getLevel.StoreLevel == 5)
            {
                if (SpawnWatch.Elapsed >= new TimeSpan(0, 0, 2))
                {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if (chance > spawnChance)
                    {
                        if (spawnedCustomers == true)
                        {
                            SpawnCustomers();
                            spawnedCustomers = false;
                        }
                    }
                    SpawnWatch.Restart();
                }
            }
        }
    }

}
