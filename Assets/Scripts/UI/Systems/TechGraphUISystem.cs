using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TechGraphUISystem : MonoBehaviour
{
    [Header("Master Objects")]
    public GameObject TechUI;
    public Button TechButton;
    public Button CloseTech;

    [Header("UI Objects")]
    public GameObject TechnologiesContainer;

    private void Start()
    {
        LoadButtons();
    }

    private void LoadButtons()
    {
        TechButton.onClick.AddListener(OpenTechUI);
        CloseTech.onClick.AddListener(CloseTechUI);
    }

    private void CloseTechUI()
    {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.currentlyActiveUI = null;
        GetUI.isActive = false;
        TechUI.SetActive(false);

    }

    private void OpenTechUI()
    {
        TechUI.SetActive(true);
    }
}
