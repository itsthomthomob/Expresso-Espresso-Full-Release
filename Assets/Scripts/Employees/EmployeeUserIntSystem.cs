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

    [Header("Master Objs")]
    public GameObject EmployeeMenuUI;
    public EntityGrid grid;
    public Button EmployeesOpenButton;
    public Button EmployeesCloseButton;

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
    public string currentCharImage;
    public float skillAmount;
    public string employeeName;
    public int HireableAmount;
    public float MinimumWage;
    public float CurrentWageOffer;
    public string[] names;
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
        LoadButtons();
        GenerateBaristas();
    }

    private void Update()
    {
        CheckHireTypes();
        GenerateInfoCard();
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
                        if (CharCardChild.name == "Pinup")
                        {
                            if (CharCardChild.TryGetComponent(out Image charImage))
                            {
                                //TO-DO:
                                // - Add random sprite picker
                                // - Add more sprites
                                // - Add dictionary of sprite names
                                currentCharImage = "Character001";
                                if (InfoCardChild.name == "CharacterSprite")
                                {
                                    charImage = InfoCardChild.GetComponent<Image>();
                                }
                            }
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
                        if (InfoCardChild.name == "WageAmount")
                        {
                            float wage = InfoCardChild.GetComponent<Slider>().value;
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
        newEmployee.SetDays(days);
        newEmployee.SetEmployeeName(employeeName);
        newEmployee.SetSpriteName(currentCharImage);
        newEmployee.SetWageAmount(CurrentWageOffer);
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
    private void CloseEmployeesMenu() 
    {
        EmployeeMenuUI.SetActive(false);
    }
    private void OpenEmployeesMenu()
    {
        EmployeeMenuUI.SetActive(true);
    }
}