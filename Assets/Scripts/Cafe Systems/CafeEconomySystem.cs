using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CafeEconomySystem : MonoBehaviour
{
    [Header("Cafe Ledger")]
    TimeManager GetTime;
    public float CurrentRevenue;
    public float CurrentProfits;
    public float CurrentExpenses;
    public float EmployeeCosts;
    public int CurrentReviews;
    public int PositiveReviews;
    public int NegativeReviews;

    [Header("Text Objects")]
    public TMP_Text TXT_CurrentRevenue;
    public TMP_Text TXT_CurrentProfits;
    public TMP_Text TXT_CurrentExpenses;
    public TMP_Text TXT_CurrentReviews;
    public TMP_Text TXT_PositiveReviews;
    public TMP_Text TXT_NegativeReviews;

    private void Start()
    {
        GetTime = FindObjectOfType<TimeManager>();
    }
    private void Update()
    {
        UpdateTexts();
        CalculatePaychecks();
    }

    private void UpdateTexts() 
    {
        TXT_CurrentRevenue.text = "$" + CurrentRevenue.ToString();
        TXT_CurrentProfits.text = "$" + CurrentProfits.ToString();
        TXT_CurrentExpenses.text = "$" + CurrentExpenses.ToString();
        TXT_CurrentReviews.text = CurrentReviews.ToString();
        TXT_PositiveReviews.text = PositiveReviews.ToString();
        TXT_NegativeReviews.text = NegativeReviews.ToString();
    }

    private void CalculateLedger() 
    {
        CurrentRevenue = CurrentProfits - CurrentExpenses;
        CurrentExpenses = CurrentExpenses + EmployeeCosts;
    }

    private void CalculatePaychecks()
    {
        EmployeeListManager GetList = FindObjectOfType<EmployeeListManager>();

        // If an hour has passed and it's at 0 seconds
        if (GetTime.GameTime.Minutes % 60 == 1 && GetTime.GameTime.Seconds == 0)
        {
            for (int i = 0; i < GetList.Employees.Count; i++)
            {
                if (GetList.Employees[i].Name == "Barista")
                {
                    EntityBarista currentBarista = GetList.Employees[i] as EntityBarista;
                    EmployeeCosts = EmployeeCosts + currentBarista.GetWageAmount();
                    continue;
                }
                else if (GetList.Employees[i].Name == "Support") 
                {
                    EntitySupport currentSupport = GetList.Employees[i] as EntitySupport;
                    EmployeeCosts = EmployeeCosts + currentSupport.GetWageAmount();
                    continue;
                }
                else if (GetList.Employees[i].Name == "Front")
                {
                    EntityFront currentFront = GetList.Employees[i] as EntityFront;
                    EmployeeCosts = EmployeeCosts + currentFront.GetWageAmount();
                    continue;
                }
            }
        }
    }
}
