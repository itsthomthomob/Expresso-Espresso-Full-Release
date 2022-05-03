using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCafeSystem : MonoBehaviour
{
    public enum CafeType 
    { 
        Corporate,
        Community
    }

    [Header("Cafe Set-up Attributes")]
    public string CafeName;
    public CafeType ThisCafeType;

    [Header("Cafe Attributes")]
    public int CafeLevel;
    public float CafeExp;
    public List<string> CafeTechnologies;
    public string CafeCurrentTech;

    [Header("Cafe Modifiers")]
    public float CustomerRetentionModifier;
    public float CustomerMoodModifier;
    public float CustomerStayModifier;
    public float CostModifier;
    public float EmployeeMoodModifier;
    public float WageCostModifier;

    [Header("Cafe Loan Details")]
    public bool tookLoan;
    public int StartingLoan;
    public float StartingLoanInterest;
    public TimeSpan LoanEndDate = new TimeSpan();

    [Header("Location Modifiers")]
    public string LocationName;
    public float WeatherCondition;
    public float PopulationRate;
    public float MinimumWage;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        switch (ThisCafeType)
        {
            case CafeType.Corporate:
                break;
            case CafeType.Community:
                break;
        }
    }
}
