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
    public int StartingLoan;
    public float StartingLoanInterest;
    public int[] LoanEndDate = { 00, 00, 00 };

    [Header("Cafe Attributes")]
    public int CafeLevel;
    public float CafeExp;
    public List<string> CafeTechnologies;
    public string CafeCurrentTech;

    [Header("CafeModifiers")]
    public float CustomerRetentionModifier;
    public float CustomerMoodModifier;
    public float CustomerStayModifier;
    public float CostModifier;
    public float EmployeeMoodModifier;
    public float WageCostModifier;

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
