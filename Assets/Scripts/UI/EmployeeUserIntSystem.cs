using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject HireablePrefab;

    public GameObject BaristasTab;
    public GameObject SupportTab;
    public GameObject FrontTab;

    public Button Baristas;
    public Button Support;
    public Button Front;

    public GameObject BaristasParent;
    public GameObject SupportParent;
    public GameObject FrontParent;

    public Slider WageOffer;

    [Header("Hire - Attributes")]
    public int HireableAmount;
    public float MinimumWage;

    public EmployeeTypeButtons currentEmployeeType;

    private void Start()
    {
        HireableAmount = 7;
        MinimumWage = 10.00f;
        LoadButtons();
        GenerateBaristas();
    }

    private void Update()
    {
        CheckHireTypes();
    }

    private void GenerateBaristas() 
    {
        for (int i = 0; i < HireableAmount; i++)
        {
            GameObject HireableEmployee = Instantiate(HireablePrefab);
            int InfoAmount = HireableEmployee.transform.childCount;
            for (int j = 0; j < InfoAmount; j++)
            {
                var child = HireableEmployee.transform.GetChild(j);
                if (child.name == "Name")
                {
                    child.GetComponent<TMP_Text>().text = "John Doe";
                }
                if (child.name == "Personality")
                {
                    float index = Random.Range(0, 2);
                    if (index > 1)
                    {
                        child.GetComponent<TMP_Text>().text = "Introvert";
                    }
                    if (index < 1)
                    {
                        child.GetComponent<TMP_Text>().text = "Extrovert";
                    }
                }
                if (child.name == "Pinup")
                {
                    var SR = child.GetComponent<Sprite>();
                }
                if (child.name == "Experience-Bar-Background")
                {
                    if (child.transform.GetChild(0).name == "EXP-BAR")
                    {
                        RectTransform EXPBAR = child.transform.GetChild(0).GetComponent<RectTransform>();
                        EXPBAR.sizeDelta = new Vector2(Random.Range(10, 200), 14);
                    }
                }
            }

            HireableEmployee.transform.SetParent(BaristasParent.transform);
        }
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