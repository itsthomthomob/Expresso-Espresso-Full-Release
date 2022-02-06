using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystemUI : MonoBehaviour
{
    [Header("Shop UI Object")]
    public GameObject ShopUI;
    public MasterUIController GetUI;

    [Header("Shop UI")]
    public Button shopButton;
    public Button closeShopButton;

    private void Start()
    {
        GetUI = FindObjectOfType<MasterUIController>();
        LoadButtons();
    }

    private void LoadButtons() 
    {
        shopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
    }

    private void OpenShop() 
    {
        ShopUI.SetActive(true);
    }

    private void CloseShop()
    {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.currentlyActiveUI = null;
        GetUI.isActive = false;
        ShopUI.SetActive(false);
    }
}
