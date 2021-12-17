using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystemUI : MonoBehaviour
{
    [Header("Shop UI Object")]
    public GameObject shopUI;

    [Header("Shop UI")]
    public Button shopButton;
    public Button closeShopButton;

    [Header("Shop Variables")]
    public bool isOpen;

    private void Start()
    {
        shopButton.onClick.AddListener(CloseShop);
        closeShopButton.onClick.AddListener(CloseShop);
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen)
        {
            shopUI.SetActive(true);
        }
        else 
        {
            shopUI.SetActive(false);
        }
    }

    private void CloseShop()
    {
        isOpen = !isOpen;
    }
}
