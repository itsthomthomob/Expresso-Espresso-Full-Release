using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoffeeUISystem : MonoBehaviour
{
    [Header("Master Controllers")]
    public GameObject CoffeeUI;
    public Button CoffeeButton;
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
        CoffeeButton.onClick.AddListener(OpenTechUI);
        CloseCoffee.onClick.AddListener(CloseCoffeeUI);
    }

    private void CloseCoffeeUI()
    {
        CoffeeUI.SetActive(false);

    }

    private void OpenTechUI()
    {
        CoffeeUI.SetActive(true);
    }
}
