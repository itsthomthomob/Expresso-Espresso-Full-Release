using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EspressoInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject EspressoUI;
    public Button CloseButton;

    [Header("Inventory")]
    public int EspressoUnits;
    public int SteamedMilkUnits;

    private void Start()
    {
        EspressoUI.SetActive(false);
        SetButtons();
    }

    private void SetButtons()
    {
        CloseButton.onClick.AddListener(CloseEspressoUI);
    }

    private void CloseEspressoUI()
    {
        EspressoUI.SetActive(false);
    }
}
