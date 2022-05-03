using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreLevelManager : MonoBehaviour
{ 
    [Header("UI Objects")]
    public Slider EXPSlider;
    public TMP_Text StoreLevelText;

    [Header("Store Attributes")]
    public int CustomersServed;
    public int StoreLevel;
    public int CurrentEXP;
    public int EXPPerLevel = 100;
    public int CustomerEXP = 10;

    [Header("Store Name UI")]
    public Button OpenNameChangeButton;
    public TMP_InputField getName;
    public Button ConfirmNameChange;
    public GameObject NameChangeModule;
    public TMP_Text storeName;
    MasterCafeSystem getCafe;

    private void Start()
    {
        getCafe = FindObjectOfType<MasterCafeSystem>();

        SetButtons();

        NameChangeModule.SetActive(false);
    }

    private void Update()
    {
        ManageLevels();
        UpdateText();
        UpdateSlider();
        UpdateStoreText();
    }

    private void SetButtons() 
    { 
        OpenNameChangeButton.onClick.AddListener(OnOpenNameChange);
        ConfirmNameChange.onClick.AddListener(OnConfirmNameChange);
    }

    private void OnOpenNameChange() 
    { 
        NameChangeModule.SetActive(true);
    }

    private void OnConfirmNameChange() 
    { 
        NameChangeModule?.SetActive(false);
        getCafe.CafeName = getName.text;
    }

    private void UpdateStoreText() 
    {
        storeName.text = getCafe.CafeName;
    }

    private void UpdateSlider() 
    {
        EXPSlider.value = CurrentEXP;
    }

    private void UpdateText() 
    {
        StoreLevelText.text = "Level: " + StoreLevel;
    }

    private void ManageLevels() 
    {
        if (CurrentEXP == EXPPerLevel)
        {
            StoreLevel += 1;
            CurrentEXP = 0;
        }
    }

    public void CustomerAddXP() 
    {
        CurrentEXP += CustomerEXP;
    }
}
