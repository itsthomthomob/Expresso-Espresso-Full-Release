using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRotationManager : MonoBehaviour
{
    [Header("Current Rotation Amount")]
    public int rotAmount;

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (rotAmount == 270)
            {
                rotAmount = 0;
            }
            else 
            { 
                rotAmount += 90;
            }
        }
    }
}
