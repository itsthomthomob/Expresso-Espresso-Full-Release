using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawnSystem : MonoBehaviour
{
    public Button SpawnCustomer;
    public EntityGrid Grid;

    private void Start()
    {
        Grid = FindObjectOfType<EntityGrid>();
        SpawnCustomer.onClick.AddListener(SpawnCustomerEntity);
    }
    // spawn customer
    private void SpawnCustomerEntity() 
    {
        Grid.Create<EntityCustomer>(new Vector2Int(-8, -8));
    }

}
