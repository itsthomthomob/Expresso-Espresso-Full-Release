using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsSystem : MonoBehaviour
{
    [Header("Master Objects")]
    public Button StatsButton;
    public Button CloseStats;
    public Button EcoTab;
    public Button InvTab;
    public GameObject StatsUI;
    public GameObject EcoUI;
    public GameObject InvUI;

    [Header("Economy Texts")]
    public TMP_Text TotalRatings;

    private void Start()
    {
        StatsUI.SetActive(true);
        EcoUI.SetActive(true);
        StatsUI.SetActive(false);

        LoadButtons();
    }

    private void LoadButtons()
    {
        StatsButton.onClick.AddListener(LoadStatisticsUI);
        EcoTab.onClick.AddListener(LoadEcoUI);
        InvTab.onClick.AddListener(LoadInvUI);
        CloseStats.onClick.AddListener(CloseStatsUI);
    }

    private void LoadStatisticsUI() { StatsUI.SetActive(true); }
    private void CloseStatsUI() {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.currentlyActiveUI = null;
        GetUI.isActive = false;
        StatsUI.SetActive(false); 
    }
    private void LoadEcoUI() { EcoUI.SetActive(true); InvUI.SetActive(false); }
    private void LoadInvUI() { InvUI.SetActive(true); EcoUI.SetActive(false); }
}
