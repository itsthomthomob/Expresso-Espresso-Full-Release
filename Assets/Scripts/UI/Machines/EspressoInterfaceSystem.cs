using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EspressoInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public EntityBase EspressoMachine;
    public GameObject EspressoUI;
    public Button CloseButton;

    [Header("Sliders")]
    public Slider TemperatureSlider;
    public Slider EspressoSlider;
    public Slider MilkSlider;
    public Slider BitternessSlider;

    [Header("Text")]
    public TMP_Text Temp;
    public TMP_Text Espresso;
    public TMP_Text Milk;

    private void Start()
    {
        EspressoUI.SetActive(false);
        SetButtons();
    }

    private void Update()
    {
        InspectorMaster GetInspector = FindObjectOfType<InspectorMaster>();

        if (GetInspector.selectedEntity != null)
        {
            EspressoMachine = GetInspector.selectedEntity;
        }

        UpdateText();
    }

    private void UpdateText() 
    {
        if (EspressoMachine != null)
        {
            Temp.text = TemperatureSlider.value.ToString();

            if (EspressoMachine is EntityEspressoMachineOne)
            {
                EntityEspressoMachineOne espresso = EspressoMachine as EntityEspressoMachineOne;
                EspressoSlider.value = espresso.EspressoUnits;
                MilkSlider.value = espresso.MilkUnits;
                BitternessSlider.value = Mathf.RoundToInt(TemperatureSlider.value);
                Espresso.text = EspressoSlider.value.ToString();
                Milk.text = MilkSlider.value.ToString();
            }

        }
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
