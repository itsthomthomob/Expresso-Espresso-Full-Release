using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoasterInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject RoasterUI;
    public Button CloseButton;

    private void Start()
    {
        RoasterUI.SetActive(false);
        SetButtons();
    }

    private void SetButtons() 
    {
        CloseButton.onClick.AddListener(CloseRoasterUI);
    }

    private void CloseRoasterUI() 
    { 
        RoasterUI.SetActive(false);
    }
}