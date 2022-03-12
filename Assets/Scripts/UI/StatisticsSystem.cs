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

    [Header("Reviews")]
    public TMP_Text TotalReviews;
    public TMP_Text NegativeReviews;
    public TMP_Text PositiveReviews;

    [Header("Budget")]
    public TMP_Text Revenue;
    public TMP_Text Profits;
    public TMP_Text Expenses;
    public TMP_Text Employees;
    public TMP_Text Imports;
    public TMP_Text Building;

    CafeEconomySystem GetECO;

    private void Awake()
    {
        GetECO = FindObjectOfType<CafeEconomySystem>();
    }
    private void Start()
    {
        SetActives();
        LoadButtons();
    }

    private void Update()
    {
        UpdateTexts();
    }

    private void UpdateTexts() 
    {
        // Reviews
        TotalReviews.text = GetECO.CurrentReviews.ToString();
        PositiveReviews.text = GetECO.PositiveReviews.ToString();
        NegativeReviews.text = GetECO.NegativeReviews.ToString();

        // Revenue
        Revenue.text = GetECO.CurrentRevenue.ToString();
        Profits.text = GetECO.CurrentProfits.ToString();
        Expenses.text = GetECO.CurrentExpenses.ToString();
        Employees.text = GetECO.EmployeeCosts.ToString();
    }

    private void SetActives() 
    {
        StatsUI.SetActive(true);
        EcoUI.SetActive(true);
        StatsUI.SetActive(false);
    }
    private void LoadButtons()
    {
        StatsButton.onClick.AddListener(LoadStatisticsUI);
        //EcoTab.onClick.AddListener(LoadEcoUI);
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
