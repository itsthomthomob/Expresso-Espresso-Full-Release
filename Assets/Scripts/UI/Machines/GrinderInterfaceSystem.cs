using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrinderInterfaceSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject GrinderUI;
    public Button CloseButton;
    public Button NextFine;
    public Button PreviousFine;
    public TMP_Text SettingsText;
    public RectTransform Turner;
    public float fillAmount;

    [Header("Attributes")]
    public int FineIndex;
    public string[] FineSettings = { "Turkish", "Fine", "Regular", "Coarse", "Very Coarse" };

    private void Start()
    {
        //GrinderUI.SetActive(false);
        SetButtons();
    }

    private void Update()
    {
        ManageFineSettings();
    }

    private void ManageFineSettings() 
    {
        SettingsText.text = FineSettings[FineIndex];
    }

    private void SetButtons()
    {
        CloseButton.onClick.AddListener(CloseBrewerUI);
        NextFine.onClick.AddListener(NextFineSetting);
        PreviousFine.onClick.AddListener(PreviousFineSetting);
    }

    private void NextFineSetting() 
    {
        if (FineIndex == 4) { FineIndex = 0; Turner.Rotate(new Vector3(0, 0, 30)); }
        else { FineIndex += 1; Turner.Rotate(new Vector3(0, 0, 30)); }
    }

    private void PreviousFineSetting()
    {
        if (FineIndex == 0) { FineIndex = 4; Turner.Rotate(new Vector3(0, 0, -30)); }
        else { FineIndex -= 1; Turner.Rotate(new Vector3(0, 0, -30));}
    }

    private void CloseBrewerUI()
    {
        GrinderUI.SetActive(false);
    }
}
