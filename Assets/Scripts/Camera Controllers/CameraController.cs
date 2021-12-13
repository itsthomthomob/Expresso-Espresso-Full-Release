using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridGenerator getGrid = GameObject.Find("GridManager").GetComponent<GridGenerator>();
        //gameObject.transform.position = getGrid.tileGrid.GetGridCenter();
    }
}
