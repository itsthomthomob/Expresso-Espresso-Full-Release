using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmployeeUserIntSystem : MonoBehaviour
{
    public enum EmployeeTypeButtons 
    { 
        onBaristas,
        onSupport,
        onFront
    }
    public enum EmployeesTabs 
    { 
        onHire,
        onEmployees,
        onRelations,
        onSchedule,
        onLogs
    }

    [Header("Master Objs")]
    public GameObject EmployeeMenuUI;
    public EntityGrid grid;
    public Button EmployeesOpenButton;
    public Button EmployeesCloseButton;

    [Header("Tabs")]
    public EmployeesTabs selectedTab;
    public GameObject HireUI;
    public GameObject EmployeesUI;
    public GameObject RelationsUI;
    public GameObject LogsUI;

    [Header("Tabs - Buttons")]
    public Color selectedColor = new Color(153, 153, 153);
    public Color originalColor = new Color(255, 255, 255);
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
    public int CurrentNewEmployeeID;
    public string[] names;
    public string employeeName;
    public string currentTrait;
    public float skillAmount;
    public float CurrentWageOffer;
    public float MinimumWage;
    public string currentCharImage;
    public Image[] days;

    [Header("Character Info Card")]
    public GameObject CharacterCard;
    public Button hireButton;

    public EmployeeTypeButtons currentEmployeeType;

    private void Start()
    {
        HireableAmount = 7;
        MinimumWage = 10.00f;
        grid = FindObjectOfType<EntityGrid>();
        selectedTab = EmployeesTabs.onHire;

        LoadButtons();
        GenerateBaristas();
        GenerateSupport();
        GenerateFront();
    }

    private void Update()
    {
        CheckHireTypes();
        GenerateInfoCard();
        ManageTabs();
    }

    private void ManageTabs() 
    {
        var colors = HireTab.colors;
        var colors2 = EmployeesTab.colors;

        switch (selectedTab)
        {
            case EmployeesTabs.onHire:
                colors.normalColor = selectedColor;
                colors2.normalColor = originalColor;
                HireUI.SetActive(true);
                EmployeesUI.SetActive(false);
                break;
            case EmployeesTabs.onEmployees:
                colors2.normalColor = selectedColor;
                colors.normalColor = originalColor;
                HireUI.SetActive(false);
                EmployeesUI.SetActive(true);
                break;
            case EmployeesTabs.onRelations:
                break;
            case EmployeesTabs.onSchedule:
                break;
            case EmployeesTabs.onLogs:
                break;
            default:
                break;
        }
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
                    child.GetComponent<TMP_Text>().text = names[Random.Range(0, names.Length)];
                }
                if (child.name == "Personality")
                {
                    float index = Random.Range(0, 2);
                    Debug.Log(index);
                    if (index == 0)
                    {
                        child.GetComponent<TMP_Text>().text = "Introvert";
                    }
                    if (index == 1)
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
            HireableEmployee.name = "Character-Container-Small";
            HireableEmployee.transform.SetParent(BaristasParent.transform);
        }
    }
    private void GenerateSupport()
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
                    child.GetComponent<TMP_Text>().text = names[Random.Range(0, names.Length)];
                }
                if (child.name == "Personality")
                {
                    float index = Random.Range(0, 2);
                    Debug.Log(index);
                    if (index == 0)
                    {
                        child.GetComponent<TMP_Text>().text = "Introvert";
                    }
                    if (index == 1)
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
            HireableEmployee.name = "Character-Container-Small";
            HireableEmployee.transform.SetParent(SupportParent.transform);
        }
    }
    private void GenerateFront()
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
                    child.GetComponent<TMP_Text>().text = names[Random.Range(0, names.Length)];
                }
                if (child.name == "Personality")
                {
                    float index = Random.Range(0, 2);
                    Debug.Log(index);
                    if (index == 0)
                    {
                        child.GetComponent<TMP_Text>().text = "Introvert";
                    }
                    if (index == 1)
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
            HireableEmployee.name = "Character-Container-Small";
            HireableEmployee.transform.SetParent(FrontParent.transform);
        }
    }

    private void GenerateInfoCard() 
    {
        int childAmount = CharacterCard.transform.childCount;
        // if currently selected object is an employee container
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Character-Container-Small")
            {
                int cardChildAmount = EventSystem.current.currentSelectedGameObject.transform.childCount;
                for (int i = 0; i < childAmount; i++)
                {
                    for (int j = 0; j < cardChildAmount; j++)
                    {
                        GameObject CharCardChild = CharacterCard.transform.GetChild(i).gameObject;
                        GameObject InfoCardChild = EventSystem.current.currentSelectedGameObject.transform.GetChild(j).gameObject;
                        if (CharCardChild.name == "Character-Name")
                        {
                            if (CharCardChild.TryGetComponent(out TMP_Text myText))
                            {
                                employeeName = myText.text;

                                if (InfoCardChild.name == "Name")
                                {
                                    myText.text = InfoCardChild.GetComponent<TMP_Text>().text;
                                }
                            }
                        }
                        if (InfoCardChild.name == "Personality")
                        {
                            currentTrait = InfoCardChild.GetComponent<TMP_Text>().text;
                        }
                        if (InfoCardChild.name == "Pinup")
                        {
                            Debug.Log("Getting image...");
                            Sprite childSprite = InfoCardChild.GetComponent<Image>().sprite;
                            currentCharImage = childSprite.name;
                        }
                        if (CharCardChild.name == "Skill-EXP-BG")
                        {
                            GameObject getChild = CharCardChild.transform.GetChild(0).gameObject;
                            if (InfoCardChild.name == "Experience-Bar-Background")
                            {
                                GameObject getEXPBAR = InfoCardChild.transform.GetChild(0).gameObject;
                                if (getEXPBAR.TryGetComponent<RectTransform>(out RectTransform EXPBAR))
                                {
                                    RectTransform skillEXP = getChild.GetComponent<RectTransform>();
                                    skillEXP.sizeDelta = new Vector2(EXPBAR.sizeDelta.x, 20);
                                    skillAmount = skillEXP.sizeDelta.x;
                                }
                            }
                        }
                        if (CharCardChild.name == "WageAmount")
                        {
                            float wage = CharCardChild.GetComponent<Slider>().value;
                            CurrentWageOffer = wage;
                        }
                    }
                }
            }
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
        HireTab.onClick.AddListener(OnHire);
        EmployeesTab.onClick.AddListener(OnEmployees);

        Baristas.onClick.AddListener(OnBaristas);
        Support.onClick.AddListener(OnSupport);
        Front.onClick.AddListener(OnFront);
        hireButton.onClick.AddListener(CreateEmployee);
        EmployeesCloseButton.onClick.AddListener(CloseEmployeesMenu);
        EmployeesOpenButton.onClick.AddListener(OpenEmployeesMenu);
    }

    private void CreateEmployee() 
    {
        EntityEmployee newEmployee = grid.Create<EntityEmployee>(new Vector2Int(0, 0));
        
        newEmployee.SetEmployeeID(CurrentNewEmployeeID);
        CurrentNewEmployeeID += 1;
        newEmployee.SetDays(days);
        newEmployee.SetEmployeeName(employeeName);
        //newEmployee.SetSpriteName(currentCharImage);
        newEmployee.SetWageAmount(CurrentWageOffer);
        newEmployee.SetSkillModifier(skillAmount);
        newEmployee.SetEfficiencyModifier(skillAmount / 2);
        switch (currentTrait)
        {
            case "Extrovert":
                newEmployee.SetEmployeePersonality(EntityEmployee.PersonalityTypes.Extrovert);
                break;
            case "Introvert":
                newEmployee.SetEmployeePersonality(EntityEmployee.PersonalityTypes.Introvert);
                break;
        }

        switch (currentEmployeeType)
        {
            case EmployeeTypeButtons.onBaristas:
                newEmployee.SetEmployeeRole(EntityEmployee.EmployeeRoles.Barista);
                break;
            case EmployeeTypeButtons.onSupport:
                newEmployee.SetEmployeeRole(EntityEmployee.EmployeeRoles.Support);
                break;
            case EmployeeTypeButtons.onFront:
                newEmployee.SetEmployeeRole(EntityEmployee.EmployeeRoles.Front);
                break;
            default:
                break;
        }
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

    private void OnHire() 
    {
        selectedTab = EmployeesTabs.onHire;
    }
    private void OnEmployees()
    {
        selectedTab = EmployeesTabs.onEmployees;
    }

    private void CloseEmployeesMenu() 
    {
        EmployeeMenuUI.SetActive(false);
    }
    private void OpenEmployeesMenu()
    {
        EmployeeMenuUI.SetActive(true);
    }
}