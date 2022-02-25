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
    public EntityBase SelectedRoaster;

    [Header("Temperature Attributes")]
    public int Temperature;
    public int TemperatureMax = 350;
    public int TemperatureMin = 175;
    public Slider TemperatureSlider;
    public Slider BitternessSlider;
    public Slider CoffeeBagsSlider;

    [Header("Coffee Attributes")]
    public int AmountOfCoffeeBags;
    public int LevelOneThreshold = 20;
    public int CoffeeBagLimit = 50;
    public int BitternessLevel;

    [Header("Dynamic UI Objects")]
    public TMP_Text CoffeeBagsText;
    public TMP_Text TemperatureText;
    public Image BitternessFill;
    public Image CoffeeBagsFill;

    private void Start()
    {
        RoasterUI.SetActive(false);
        SetButtons();
    }

    private void Update()
    {
        InspectorMaster GetInspector = FindObjectOfType<InspectorMaster>();
        SelectedRoaster = GetInspector.selectedEntity;
        if (SelectedRoaster != null)
        {
            Temperature = (int)TemperatureSlider.value;
            TemperatureText.text = TemperatureSlider.value.ToString();
            BitternessSlider.value = TemperatureSlider.value;
            CheckRoaster();
        }
    }

    private void CheckRoaster() 
    {
        if (SelectedRoaster is EntityRoasteryMachineOne)
        {
            EntityRoasteryMachineOne roaster = SelectedRoaster as EntityRoasteryMachineOne;
            UpdateLevelOneVariables(roaster);
        }
    }

    private void UpdateLevelOneVariables(EntityRoasteryMachineOne currentRoaster) 
    {
        CoffeeBagsText.text = currentRoaster.AmountOfCoffeeBags.ToString() + " / " + currentRoaster.CoffeeBagLimit.ToString();
        currentRoaster.Temperature = Temperature;
        CoffeeBagsSlider.value = currentRoaster.AmountOfCoffeeBags;
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