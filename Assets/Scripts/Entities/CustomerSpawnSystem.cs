using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawnSystem : MonoBehaviour
{
    public Button SpawnCustomer;
    public EntityGrid Grid;

    [Header("All Chairs")]
    EntityBarstool[] AllChairBarstools;
    EntityChairGrey[] AllChairGrey;
    EntityChairRed[] AllChairRed;
    EntityChairRough[] AllChairRough;
    EntityChairSmooth[] AllChairSmooth;

    private void Start()
    {
        Grid = FindObjectOfType<EntityGrid>();
        SpawnCustomer.onClick.AddListener(SpawnCustomerEntity);
    }

    private void Update()
    {
        FindChairs();
        FindCafe();
    }

    private void FindChairs() 
    { 
        AllChairBarstools = FindObjectsOfType<EntityBarstool>();
        AllChairGrey = FindObjectsOfType<EntityChairGrey>();
        AllChairRed = FindObjectsOfType<EntityChairRed>(); 
        AllChairRough = FindObjectsOfType<EntityChairRough>();  
        AllChairSmooth = FindObjectsOfType<EntityChairSmooth>();   
    }

    private void FindCafe() 
    { 
    
    }

    private void SpawnCustomers() 
    { 
    
    }

    private void SpawnCustomerEntity() 
    {
        Grid.Create<EntityCustomer>(new Vector2Int(-8, -8));
    }

}
