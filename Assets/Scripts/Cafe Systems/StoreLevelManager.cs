using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreLevelManager : MonoBehaviour
{ 
    [Header("UI Objects")]
    public Slider EXPSlider;
    public TMP_Text StoreLevelText;

    [Header("Store Attributes")]
    public int CustomersServed;
    public int StoreLevel;
    public int CurrentEXP;
    public int EXPPerLevel = 100;

    private void Update()
    {
        UpdateText();   
    }

    private void UpdateText() 
    {
        StoreLevelText.text = "Level: " + StoreLevel;
    }

}
