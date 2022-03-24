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
    public int CustomerEXP = 10;

    private void Update()
    {
        ManageLevels();
        UpdateText();
        UpdateSlider();
    }

    private void UpdateSlider() 
    {
        EXPSlider.value = CurrentEXP;
    }

    private void UpdateText() 
    {
        StoreLevelText.text = "Level: " + StoreLevel;
    }

    private void ManageLevels() 
    {
        if (CurrentEXP == EXPPerLevel)
        {
            StoreLevel += 1;
            CurrentEXP = 0;
        }
    }

    public void CustomerAddXP() 
    {
        CurrentEXP += CustomerEXP;
    }

}
