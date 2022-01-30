using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewerInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject BrewerUI;
    public Button CloseButton;

    [Header("Inventory")]
    public int BrewedCoffeeUnits;
    public string CurrentCustomer;
    public string TimeLeft;

    private void Start()
    {
        BrewerUI.SetActive(false);
        SetButtons();
    }

    private void SetButtons()
    {
        CloseButton.onClick.AddListener(CloseBrewerUI);
    }

    private void CloseBrewerUI()
    {
        BrewerUI.SetActive(false);
    }
}
