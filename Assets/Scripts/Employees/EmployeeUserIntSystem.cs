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
    public EmployeeListManager getList;

    [Header("Tabs")]
    public EmployeesTabs selectedTab;
    public GameObject HireUI;
    public GameObject EmployeesUI;
    public GameObject ScheduleUI;
    public GameObject RelationsUI;
    public GameObject LogsUI;

    [Header("Tabs - Buttons")]
    public Color selectedColor = new Color(153, 153, 153);
    public Color originalColor = new Color(255, 255, 255);
    public Button HireTab;
    public Button EmployeesTab;
    public Button ScheduleTab;
    public Button RelationsTab;
    public Button LogTab;

    [Header("Hire - Elements")]
    public GameObject HireablePrefab;
    public List<GameObject> Hireables = new List<GameObject>();

    public GameObject BaristasTab;
    public GameObject SupportTab;
    public GameObject FrontTab;

    public Button Baristas;
    public Button Support;
    public Button Front;

    public GameObject BaristasParent;
    public GameObject SupportParent;
    public GameObject FrontParent;

    [Header("Hire - Attributes")]
    public int HireableAmount;
    public int CurrentNewEmployeeID;
    public string[] names;
    public string employeeName;
    public string currentTrait;
    public float skillAmount;
    public float CurrentWageOffer;
    public Slider WageSlider;
    public TMP_Text WageOffer;
    public float MinimumWage;
    public string currentCharImage;
    public Sprite CurrentCharacterImage;

    [Header("Character Info Card")]
    public GameObject CharacterCard;
    public Button hireButton;
    public GameObject FirstObj;

    public EmployeeTypeButtons currentEmployeeType;

    public Object[] LoadSprites;
    public List<Sprite> CharacterSprites;

    private void Awake()
    {
        LoadSprites = Resources.LoadAll("Sprites/Characters", typeof(Sprite));
        for (int i = 0; i < LoadSprites.Length; i++)
        {
            if (!CharacterSprites.Contains(LoadSprites[i] as Sprite))
            {
                CharacterSprites.Add(LoadSprites[i] as Sprite);
            }
        }
    }

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
        CharacterCard.SetActive(false);
        FirstObj.SetActive(false);
    }

    private void Update()
    {
        CheckHireTypes();
        GenerateInfoCard();
        ManageWage();
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
                ScheduleUI.SetActive(false);

                break;
            case EmployeesTabs.onEmployees:
                colors2.normalColor = selectedColor;
                colors.normalColor = originalColor;
                HireUI.SetActive(false);
                EmployeesUI.SetActive(true);
                ScheduleUI.SetActive(false);

                break;
            case EmployeesTabs.onRelations:
                break;
            case EmployeesTabs.onSchedule:
                colors2.normalColor = selectedColor;
                colors.normalColor = originalColor;
                HireUI.SetActive(false);
                EmployeesUI.SetActive(false);
                ScheduleUI.SetActive(true);
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
            Hireables.Add(HireableEmployee);

            int spriteIndex = Random.Range(0, CharacterSprites.Count);

            string SelectedSprite = CharacterSprites[spriteIndex].name;

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
                    Image GetImg = child.GetComponent<Image>();
                    GetImg.sprite = Resources.Load<Sprite>("Sprites/Characters/" + SelectedSprite);
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
            Hireables.Add(HireableEmployee);
            int spriteIndex = Random.Range(0, CharacterSprites.Count);

            string SelectedSprite = CharacterSprites[spriteIndex].name;

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
                    Image GetImg = child.GetComponent<Image>();
                    GetImg.sprite = Resources.Load<Sprite>("Sprites/Characters/" + SelectedSprite);
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
            Hireables.Add(HireableEmployee);
            int spriteIndex = Random.Range(0, CharacterSprites.Count);

            string SelectedSprite = CharacterSprites[spriteIndex].name;

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
                    Image GetImg = child.GetComponent<Image>();
                    GetImg.sprite = Resources.Load<Sprite>("Sprites/Characters/" + SelectedSprite);
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

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Character-Container-Small")
            {
                CharacterCard.SetActive(true);

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
                            CurrentCharacterImage = InfoCardChild.GetComponent<Image>().sprite;
                            if (CharacterCard.transform.GetChild(i).name == "CharacterSprite")
                            {
                                Transform child = CharacterCard.transform.GetChild(i);
                                child.GetComponent<Image>().sprite = CurrentCharacterImage;
                                currentCharImage = "Sprites/Characters/" + CurrentCharacterImage.name;
                                //Debug.Log("Character Image Name: " + currentCharImage);
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
                    }
                }
            }
        }
    }
    private void ManageWage() 
    {
        if (WageSlider.IsActive())
        {
            float wage = WageSlider.value;
            float newWage = Mathf.Round(wage * 10.0f) * 0.1f;
            WageOffer.text = newWage.ToString();
            CurrentWageOffer = newWage;
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
        ScheduleTab.onClick.AddListener(OnSchedule);
    }
    private void CreateEmployee() 
    {
        switch (currentEmployeeType)
        {
            case EmployeeTypeButtons.onBaristas:
                EntityBarista newBarista = grid.Create<EntityBarista>(new Vector2Int(0, 0));
                newBarista.SetEmployeeID(CurrentNewEmployeeID);
                CurrentNewEmployeeID += 1;
                newBarista.SetEmployeeName(employeeName);
                newBarista.SetSpriteName(currentCharImage);
                Debug.Log(newBarista.GetSpriteName());
                newBarista.SetWageAmount(CurrentWageOffer);
                newBarista.SetSkillModifier(skillAmount);
                newBarista.SetEfficiencyModifier(skillAmount / 2);
                switch (currentTrait)
                {
                    case "Extrovert":
                        newBarista.SetEmployeePersonality("Extrovert");
                        break;
                    case "Introvert":
                        newBarista.SetEmployeePersonality("Introvert");
                        break;
                }
                getList.hiredBaristas.Add(newBarista);
            break;
            case EmployeeTypeButtons.onSupport:
                EntitySupport newSupport = grid.Create<EntitySupport>(new Vector2Int(0, 0));
                newSupport.SetEmployeeID(CurrentNewEmployeeID);
                CurrentNewEmployeeID += 1;
                newSupport.SetEmployeeName(employeeName);
                newSupport.SetSpriteName(currentCharImage);
                newSupport.SetWageAmount(CurrentWageOffer);
                newSupport.SetSkillModifier(skillAmount);
                newSupport.SetEfficiencyModifier(skillAmount / 2);
                switch (currentTrait)
                {
                    case "Extrovert":
                        newSupport.SetEmployeePersonality("Extrovert");
                        break;
                    case "Introvert":
                        newSupport.SetEmployeePersonality("Introvert");
                        break;
                }
                getList.hiredSupports.Add(newSupport);
            break;
            case EmployeeTypeButtons.onFront:
                EntityFront newFront = grid.Create<EntityFront>(new Vector2Int(0, 0));
                newFront.SetEmployeeID(CurrentNewEmployeeID);
                CurrentNewEmployeeID += 1;
                newFront.SetEmployeeName(employeeName);
                newFront.SetSpriteName(currentCharImage);
                newFront.SetWageAmount(CurrentWageOffer);
                newFront.SetSkillModifier(skillAmount);
                newFront.SetEfficiencyModifier(skillAmount / 2);
                switch (currentTrait)
                {
                    case "Extrovert":
                        newFront.SetEmployeePersonality("Extrovert");
                        break;
                    case "Introvert":
                        newFront.SetEmployeePersonality("Introvert");
                        break;
                }
                getList.hiredFronts.Add(newFront);
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
    private void OnSchedule()
    {
        selectedTab = EmployeesTabs.onSchedule;
    }

    private void CloseEmployeesMenu() 
    {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.currentlyActiveUI = null;
        GetUI.isActive = false;
        EmployeeMenuUI.SetActive(false);
    }
    private void OpenEmployeesMenu()
    {
        EmployeeMenuUI.SetActive(true);
    }
}