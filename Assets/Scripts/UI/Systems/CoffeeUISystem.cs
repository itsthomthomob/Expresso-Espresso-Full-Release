using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoffeeUISystem : MonoBehaviour
{
    [Header("Master Controllers")]
    public GameObject CoffeeUI;
    public Button CloseCoffee;
    public Button MenuTab;
    public Button CoffeeCreationTab;

    [Header("Master UI")]
    public GameObject MenuUI;
    public GameObject CoffeeCreationUI;

    private void Start()
    {
        LoadButtons();
    }

    private void LoadButtons()
    {
        CloseCoffee.onClick.AddListener(CloseCoffeeUI);
    }

    private void CloseCoffeeUI()
    {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.currentlyActiveUI = null;
        GetUI.isActive = false;
        CoffeeUI.SetActive(false);
    }
}
