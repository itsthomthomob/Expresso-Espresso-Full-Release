using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawnSystem : MonoBehaviour
{
    //public Button SpawnCustomer;
    public EntityGrid Grid;
    public int totalCustomers;
    public List<EntityCustomer> spawnedCustomers = new List<EntityCustomer>();
    public List<EntityCustomer> allCustomers = new List<EntityCustomer>();

    [Header("Spawn Modifiers")]
    public int spawnLimit = 10;
    public int AllCustomers;
    public WeatherManager getWeather;
    public EntityConcrete[] allConcrete;

    [Header("Spawn Objs")]
    TimeManager GetTime;
    Stopwatch CustomerTimer = new Stopwatch();
    EmployeeListManager employees;

    [Header("All Chairs")]
    List<EntityBase> AllChairs = new List<EntityBase>();
    EntityBarstool[] AllChairBarstools;
    EntityChairGrey[] AllChairGrey;
    EntityChairRed[] AllChairRed;
    EntityChairRough[] AllChairRough;
    EntityChairSmooth[] AllChairSmooth;


    private void Start()
    {
        SetObjects();
    }

    private void Update()
    {
        //FindChairs();
        if (spawnedCustomers.Count > spawnLimit)
        {
            if (!CustomerTimer.IsRunning)
            {
                CustomerTimer.Start();
            }
            if (employees.hiredFronts.Count + employees.hiredSupports.Count + employees.hiredSupports.Count > 2)
            {
                SpawnCustomers();
            }
        }
        else
        {
            SpawnStartingCustomers();
        }
    }

    private void SetObjects() 
    { 
        Grid = FindObjectOfType<EntityGrid>();
        GetTime = FindObjectOfType<TimeManager>();
        allConcrete = FindObjectsOfType<EntityConcrete>();
        employees = FindObjectOfType<EmployeeListManager>();
    }
    private void SpawnStartingCustomers() 
    {
        AllCustomers = allCustomers.Count;

        // Find conrete
        // Spawn on concrete with no customer
        // Stop spawning after 10 customers (spawnLimit)
        // If chairs > spawn limit, continue spawning
        // Customers can only walk on concrete, foundation, or furniture tiles (AI)

        if (allConcrete.Length > 0)
        {
            // Concrete exists, choose random concrete
            int index = UnityEngine.Random.Range(0, allConcrete.Length - 1);
            EntityConcrete curCon = allConcrete[index];
            if (Grid.GetLastEntity<EntityCustomer>(curCon.Position) == null)
            {
                // No customer, can spawn here
                if (spawnedCustomers.Count < spawnLimit + 1)
                {
                    // current amount of spawne customers is less than limit, spawn
                    EntityCustomer newCustomer = Grid.Create<EntityCustomer>(curCon.Position);
                    spawnedCustomers.Add(newCustomer);
                    allCustomers.Add(newCustomer);
                }
            }
            else 
            {
                return;
            }
        }
    }

    private void SpawnCustomers() 
    {
        float spawnChance = UnityEngine.Random.Range(0, 1);
        int index = UnityEngine.Random.Range(0, allConcrete.Length - 1);
        EntityConcrete curCon = allConcrete[index];
        if (Math.Floor(CustomerTimer.Elapsed.TotalSeconds) % 5 == 0)
        {
            CustomerTimer.Restart();
            if (getWeather.curWeather != WeatherManager.WeatherTypes.None)
            {
                switch (getWeather.curWeather)
                {
                    case WeatherManager.WeatherTypes.None:
                        // do nothing
                        break;
                    case WeatherManager.WeatherTypes.Rain:
                        if (spawnChance > getWeather.RainChance)
                        {
                            EntityCustomer newCus1 = Grid.Create<EntityCustomer>(curCon.Position);
                            allCustomers.Add(newCus1);
                        }
                        break;
                    case WeatherManager.WeatherTypes.Storm:
                        if (spawnChance > getWeather.StormChance)
                        {
                            EntityCustomer newCus2 = Grid.Create<EntityCustomer>(curCon.Position);
                            allCustomers.Add(newCus2);
                        }
                        break;
                    case WeatherManager.WeatherTypes.Fog:
                        if (spawnChance > getWeather.StormChance)
                        {
                            EntityCustomer newCus3 = Grid.Create<EntityCustomer>(curCon.Position);
                            allCustomers.Add(newCus3);
                        }
                        break;
                }
            }
        }
    }
}
