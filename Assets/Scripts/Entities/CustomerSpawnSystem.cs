using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawnSystem : MonoBehaviour
{
    //public Button SpawnCustomer;
    public EntityGrid Grid;
    public int totalCustomers;

    [Header("All Chairs")]
    List<EntityBase> AllChairs = new List<EntityBase>();
    EntityBarstool[] AllChairBarstools;
    EntityChairGrey[] AllChairGrey;
    EntityChairRed[] AllChairRed;
    EntityChairRough[] AllChairRough;
    EntityChairSmooth[] AllChairSmooth;

    [Header("Spawn Objs")]
    EntityBase GetFloor;
    EntityBase GetRandomTile;
    EntityRegister GetRegister;
    EntityEspressoMachineOne GetEspresso;
    EntityFront GetFront;
    EntityBarista GetBarista;
    TimeManager GetTime;
    TimeSpan CustomerDelay = new TimeSpan(0, 0, 0);
    bool DelayCustomerSpawning = false;

    private void Start()
    {
        Grid = FindObjectOfType<EntityGrid>();
        GetTime = FindObjectOfType<TimeManager>();
        //SpawnCustomer.onClick.AddListener(SpawnCustomerEntity);
    }

    private void Update()
    {
        GetRegister = FindObjectOfType<EntityRegister>();
        GetEspresso = FindObjectOfType<EntityEspressoMachineOne>();
        GetFront = FindObjectOfType<EntityFront>();
        GetBarista = FindObjectOfType<EntityBarista>();

        FindChairs();
        SpawnCustomers();
        FindCafe();
    }

    private void FindChairs() 
    { 
        AllChairBarstools = FindObjectsOfType<EntityBarstool>();
        AllChairGrey = FindObjectsOfType<EntityChairGrey>();
        AllChairRed = FindObjectsOfType<EntityChairRed>(); 
        AllChairRough = FindObjectsOfType<EntityChairRough>();  
        AllChairSmooth = FindObjectsOfType<EntityChairSmooth>();

        for (int i = 0; i < AllChairBarstools.Length; i++)
        {
            if (!AllChairs.Contains(AllChairBarstools[i]))
            {
                AllChairs.Add(AllChairBarstools[i]);
            }
        }
        for (int i = 0; i < AllChairGrey.Length; i++)
        {
            if (!AllChairs.Contains(AllChairGrey[i]))
            {
                AllChairs.Add(AllChairGrey[i]);
            }
        }
        for (int i = 0; i < AllChairRed.Length; i++)
        {
            if (!AllChairs.Contains(AllChairRed[i]))
            {
                AllChairs.Add(AllChairRed[i]);
            }
        }
        for (int i = 0; i < AllChairRough.Length; i++)
        {
            if (!AllChairs.Contains(AllChairRough[i]))
            {
                AllChairs.Add(AllChairRough[i]);
            }
        }
        for (int i = 0; i < AllChairSmooth.Length; i++)
        {
            if (!AllChairs.Contains(AllChairSmooth[i]))
            {
                AllChairs.Add(AllChairSmooth[i]);
            }
        }

    }

    // TO-DO:
    // - Flood fill for cafe
    //   - Find floors in at least a 8 x 8 shape

    private void FindCafe() 
    { 
        // FloodFill for cafe
        
        // Find any floor tile
        EntityFloor[] AllFloorOne = FindObjectsOfType<EntityFloor>();
        EntityFloorTwo[] AllFloorTwo = FindObjectsOfType<EntityFloorTwo>();
        EntityFloorThree[] AllFloorThree = FindObjectsOfType<EntityFloorThree>();
        EntityFloorFour[] AllFloorFour = FindObjectsOfType<EntityFloorFour>();
        EntityFloorFive[] AllFloorFive = FindObjectsOfType<EntityFloorFive>();
        EntityFloorSix[] AllFloorSix = FindObjectsOfType<EntityFloorSix>();
        EntityFloorSeven[] AllFloorSeven = FindObjectsOfType<EntityFloorSeven>();

        if (GetFloor == null)
        {
            for (int i = 0; i < AllFloorOne.Length; i++)
            {
                if (AllFloorOne[i] != null)
                {
                    GetFloor = AllFloorOne[i];
                }
            }
            for (int i = 0; i < AllFloorTwo.Length; i++)
            {
                if (AllFloorTwo[i] != null)
                {
                    GetFloor = AllFloorTwo[i];
                }
            }
            for (int i = 0; i < AllFloorThree.Length; i++)
            {
                if (AllFloorThree[i] != null)
                {
                    GetFloor = AllFloorThree[i];
                }
            }
            for (int i = 0; i < AllFloorFour.Length; i++)
            {
                if (AllFloorFour[i] != null)
                {
                    GetFloor = AllFloorFour[i];
                }
            }
            for (int i = 0; i < AllFloorFive.Length; i++)
            {
                if (AllFloorFive[i] != null)
                {
                    GetFloor = AllFloorFive[i];
                }
            }
            for (int i = 0; i < AllFloorSix.Length; i++)
            {
                if (AllFloorSix[i] != null)
                {
                    GetFloor = AllFloorSix[i];
                }
            }
            for (int i = 0; i < AllFloorSeven.Length; i++)
            {
                if (AllFloorSeven[i] != null)
                {
                    GetFloor = AllFloorSeven[i];
                }
            }
        }
    }

    public EntityGrass NewSpawnPosition(EntityBase source) 
    {
        int RandomX = UnityEngine.Random.Range(source.Position.x, 5);
        int RandomY = UnityEngine.Random.Range(source.Position.y, 5);

        Vector2Int RandomPosition = new Vector2Int(RandomX, RandomY);

        EntityGrass NearestOutsideTile= Grid.FindNearestEntity<EntityGrass>(RandomPosition);
        return NearestOutsideTile;
    }

    private void SpawnCustomers() 
    {
        MenuItem[] AllMenuItems = FindObjectsOfType<MenuItem>();
        EntityCustomer[] Customers = FindObjectsOfType<EntityCustomer>();
        if (GetFloor != null)
        {
            GetRandomTile = NewSpawnPosition(GetFloor);
            if (GetRandomTile != null)
            {
                if (AllChairs.Count > 0 && Customers.Length < AllChairs.Count)
                {
                    if (GetRegister != null && GetFront != null)
                    {
                        if (GetEspresso != null && GetBarista != null)
                        {
                            if (AllMenuItems.Length > 0)
                            {
                                DelaySpawning();
                                if (DelayCustomerSpawning == false)
                                {
                                    EntityCustomer Customer = Grid.Create<EntityCustomer>(GetRandomTile.Position);
                                    totalCustomers += 1;
                                    Customer.MyCustomerID = totalCustomers + 100;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void DelaySpawning() 
    {
        if (DelayCustomerSpawning == false)
        {
            CustomerDelay = GetTime.GameTime;
            DelayCustomerSpawning = true;
        }
        else 
        {
            if (GetTime.GameTime.Seconds - CustomerDelay.Seconds >= 5.0f)
            {
                DelayCustomerSpawning = false;
            }
        }
    }

    private void SpawnCustomerEntity() 
    {
        Grid.Create<EntityCustomer>(new Vector2Int(-8, -8));
    }

}
