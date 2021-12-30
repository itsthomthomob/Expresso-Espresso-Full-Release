using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeUserIntSystem : MonoBehaviour
{
    public enum EmployeeTypeButtons 
    { 
        onBaristas,
        onSupport,
        onFront
    }

    [Header("Master UI")]
    public GameObject EmployeeMenuUI;

    [Header("Tabs")]
    public GameObject HireUI;
    public GameObject EmployeesUI;
    public GameObject RelationsUI;
    public GameObject LogsUI;

    [Header("Tabs - Buttons")]
    public Button HireTab;
    public Button EmployeesTab;
    public Button RelationsTab;
    public Button LogTab;

    [Header("Hire - Elements")]
    public GameObject BaristasTab;
    public GameObject SupportTab;
    public GameObject FrontTab;

    public Button Baristas;
    public Button Support;
    public Button Front;

    public EmployeeTypeButtons currentEmployeeType;

    private void Start()
    {
        LoadButtons();
    }

    private void Update()
    {
        CheckHireTypes();
    }

    private void CheckHireTypes() 
    {
        switch (currentEmployeeType)
        {
            case EmployeeTypeButtons.onBaristas:
                BaristasTab.SetActive(true);
                SupportTab.SetActive(false);
                FrontTab.SetActive(false);
                break;
            case EmployeeTypeButtons.onSupport:
                BaristasTab.SetActive(false);
                SupportTab.SetActive(true);
                FrontTab.SetActive(false);
                break;
            case EmployeeTypeButtons.onFront:
                BaristasTab.SetActive(false);
                SupportTab.SetActive(false);
                FrontTab.SetActive(true);
                break;
        }
    }
    private void LoadButtons()
    {
        Baristas.onClick.AddListener(OnBaristas);
        Support.onClick.AddListener(OnSupport);
        Front.onClick.AddListener(OnFront);
    }
    private void OnBaristas() 
    {
        currentEmployeeType = EmployeeTypeButtons.onBaristas;
    }
    private void OnSupport()
    {
        currentEmployeeType = EmployeeTypeButtons.onSupport;
    }
    private void OnFront()
    {
        currentEmployeeType = EmployeeTypeButtons.onFront;
    }
}