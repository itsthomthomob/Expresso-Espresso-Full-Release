using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    float originalY;

    public float floatStrength = 10; 

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, originalY + ((float)Math.Sin(Time.time * 2) * floatStrength));
    }
}
