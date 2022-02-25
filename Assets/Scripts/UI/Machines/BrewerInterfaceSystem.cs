using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrewerInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject BrewerUI;
    public Button CloseButton;
    public EntityBase Brewer;

    [Header("Sliders")]
    public Slider BitternessSlider;
    public Slider TemperatureSlider;
    public Slider BrewedCoffeeSlider;

    [Header("Attributes / Text")]
    public int Temperature;
    public TMP_Text TemperatureText;
    public TMP_Text BrewedCoffeeText;

    private void Start()
    {
        BrewerUI.SetActive(false);
        SetButtons();
    }

    private void Update()
    {
        InspectorMaster GetInspector = FindObjectOfType<InspectorMaster>();
        Brewer = GetInspector.selectedEntity;
        if (Brewer != null)
        {
            BitternessSlider.value = TemperatureSlider.value;
            Temperature = (int)TemperatureSlider.value;
            TemperatureText.text = TemperatureSlider.value.ToString();
            CheckBrewer();
        }
    }

    private void CheckBrewer()
    {
        if (Brewer is EntityBrewingMachineOne)
        {
            EntityBrewingMachineOne brewer = Brewer as EntityBrewingMachineOne;
            UpdateVariables(brewer);
        }
    }

    private void UpdateVariables(EntityBrewingMachineOne entity) 
    {
        BrewedCoffeeText.text = entity.BrewedCoffeeUnits.ToString() + " / " + entity.BrewedCoffeeLimit + " Units";
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
