using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class MenuManagementSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject CoffeeUI;
    public GameObject CoffeeCreationUI;
    public Button CloseMenu;
    public Button OpenMenu;
    public GameObject prefabItemMenu;
    CoffeeCreationSystem GetCoffeeSystem;

    [Header("Menu Section Dropdowns")]
    public TMP_Dropdown DropDownOne;
    public TMP_Dropdown DropDownTwo;
    public TMP_Dropdown DropDownThree;

    [Header("Menu Objects")]
    public Button AddItemSectionOne;
    public Button AddItemSectionTwo;
    public Button AddItemSectionThree;

    [Header("Spawners")]
    public GameObject[] SectionOneObjects;
    public GameObject[] SectionTwoObjects;
    public GameObject[] SectionThreeObjects;

    [Header("Item Menu Types")]
    public List<GameObject> LatteItems = new List<GameObject>();
    public List<GameObject> MochasItems = new List<GameObject>();
    public List<GameObject> AmericanosItems = new List<GameObject>();
    public List<GameObject> CappoccinoItems = new List<GameObject>();
    public List<GameObject> MacchiatoItems = new List<GameObject>();
    public List<GameObject> MochaItems = new List<GameObject>();
    public List<GameObject> FlatWhiteItems = new List<GameObject>();
    public List<GameObject> IrishCoffeeItems = new List<GameObject>();

    [Header("All Menu Items")]
    public List<MenuItem> MenuItems = new List<MenuItem>();
    public MenuItem[] AllMenuItems;
    public List<MenuItem> NewLoadedMenuItems = new List<MenuItem>();

    private void Start()
    {
        LoadButtons();
        AddItemSectionTwo.onClick.AddListener(GoToCoffeeCreation);
        AddItemSectionOne.onClick.AddListener(GoToCoffeeCreation);
        AddItemSectionThree.onClick.AddListener(GoToCoffeeCreation);
        DropDownOne.onValueChanged.AddListener(ClearSectionObjects);
        GetCoffeeSystem = FindObjectOfType<CoffeeCreationSystem>();
    }

    private void Update()
    {
        UpdateDropDownChanges();
        GetMenuItems();
        UpdateMenuItems();
        SetLoadedItems();
    }

    private void SetLoadedItems() 
    {
        GameObject LoadedObjects = GameObject.Find("LoadedObjects");
        Component[] GetLoadedComponents = LoadedObjects.GetComponents(typeof(Component));

        for (int i = 0; i < GetLoadedComponents.Length; i++)
        {
            if (GetLoadedComponents[i] is MenuItem)
            {
                if (!NewLoadedMenuItems.Contains(GetLoadedComponents[i] as MenuItem))
                {
                    NewLoadedMenuItems.Add(GetLoadedComponents[i] as MenuItem);
                }
            }
        }

        if (NewLoadedMenuItems.Count > 0)
        {
            for (int k = 0; k < NewLoadedMenuItems.Count; k++)
            {
                // Get LoadedObject game object
                // Get components
                // Find MenuItem components
                // Register values
                if (NewLoadedMenuItems[k].GetMyObject() == null)
                {
                    GameObject newItem = Instantiate(prefabItemMenu);
                    NewLoadedMenuItems[k].SetMyObject(newItem);
                    Transform getTF = newItem.transform;

                    for (int j = 0; j < getTF.childCount; j++)
                    {
                        Transform getChild = getTF.GetChild(j);
                        if (getChild.name == "ItemName")
                        {
                            TMP_Text ItemName = getChild.GetComponent<TMP_Text>();
                            ItemName.text = NewLoadedMenuItems[k].GetItemName();
                        }
                        else if (getChild.name == "ItemContents")
                        {
                            TMP_Text contents = getChild.GetComponent<TMP_Text>();
                            contents.text = NewLoadedMenuItems[k].GetDrinkType().ToString();
                        }
                        else if (getChild.name == "ItemPrice")
                        {
                            TMP_Text ItemPrice = getChild.GetComponent<TMP_Text>();
                            ItemPrice.text = NewLoadedMenuItems[k].GetPrice().ToString();
                        }
                    }
                    Debug.Log("Adding Item: " + NewLoadedMenuItems[k].GetDrinkType().ToString());
                    GetCoffeeSystem.AddToPhysicalMenu(newItem, NewLoadedMenuItems[k]);
                }
            }
        }
    }

    private void UpdateMenuItems() 
    {
        AllMenuItems = new MenuItem[MenuItems.Count];
        for (int i = 0; i < MenuItems.Count; i++)
        {
            AllMenuItems[i] = MenuItems[i];

        }
    }

    public int GetTotalMenuItems() 
    {
        return AllMenuItems.Length;
    }

    public string[] GetSpawnObjectsData()
    {
        string[] AllMenuItemPriceData = new string[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetMyObject().name;
        }

        return AllMenuItemPriceData;
    }

    public float[] GetMenuItemsPriceData() 
    {
        float[] AllMenuItemPriceData = new float[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetPrice();
        }

        return AllMenuItemPriceData;
    }

    public float[] GetMenuItemsExpenseData()
    {
        float[] AllMenuItemPriceData = new float[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetExpense();
        }

        return AllMenuItemPriceData;
    }

    public int[] GetMenuItemIDsData()
    {
        int[] AllMenuItemPriceData = new int[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetMenuID();
        }

        return AllMenuItemPriceData;
    }

    public string[] GetMenuItemsNamesData()
    {
        string[] AllMenuItemPriceData = new string[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetItemName();
            Debug.Log("Setting Names: " + AllMenuItems[i].GetItemName());
        }

        return AllMenuItemPriceData;
    }

    public string[] GetMenuItemsTypeData()
    {
        string[] AllMenuItemPriceData = new string[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetDrinkType().ToString();
        }

        return AllMenuItemPriceData;
    }

    public bool[] GetMenuItemsActiveData()
    {
        bool[] AllMenuItemPriceData = new bool[AllMenuItems.Length];

        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            AllMenuItemPriceData[i] = AllMenuItems[i].GetIsActive();
        }

        return AllMenuItemPriceData;
    }

    private void GetMenuItems() 
    {
        MenuItem[] ActiveMenuItems = FindObjectsOfType<MenuItem>();
        for (int i = 0; i < ActiveMenuItems.Length; i++)
        {
            if (!MenuItems.Contains(ActiveMenuItems[i]))
            {
                MenuItems.Add(ActiveMenuItems[i]);
            }
        }
    }
    private void ClearSectionObjects(int value) 
    {
        try
        {
            switch (value)
            {
                case 0:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);
                    }
                    break;
                case 1:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);

                    }
                    break;
                case 2:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);
                    }
                    break;
                case 3:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);

                    }
                    break;
                case 4:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);
                    }
                    break;
                case 5:
                    for (int i = 0; i < SectionOneObjects.Length; i++)
                    {
                        Transform child = SectionOneObjects[i].transform.GetChild(0);
                        child.SetParent(null);

                    }
                    break;
            }
        }
        catch 
        { 
        
        }
    }

    private void UpdateDropDownChanges() 
    {
        try
        {
            switch (DropDownOne.value)
            {
                case 0:
                    if (LatteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                LatteItems[i].transform.position = SectionOneObjects[i].transform.position;
                                LatteItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 1:
                    if (MochaItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                MochaItems[i].transform.position = SectionOneObjects[i].transform.position;
                                MochaItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 2:
                    if (AmericanosItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                AmericanosItems[i].transform.position = SectionOneObjects[i].transform.position;
                                AmericanosItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 3:
                    if (CappoccinoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                CappoccinoItems[i].transform.position = SectionOneObjects[i].transform.position;
                                CappoccinoItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 4:
                    if (MacchiatoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                MacchiatoItems[i].transform.position = SectionOneObjects[i].transform.position;
                                MacchiatoItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 5:
                    if (FlatWhiteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                FlatWhiteItems[i].transform.position = SectionOneObjects[i].transform.position;
                                FlatWhiteItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 6:
                    if (IrishCoffeeItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionOneObjects[i].transform.childCount == 0)
                            {
                                IrishCoffeeItems[i].transform.position = SectionOneObjects[i].transform.position;
                                IrishCoffeeItems[i].transform.SetParent(SectionOneObjects[i].transform);
                            }
                        }
                    }
                    break;
            }

        }
        catch
        {

        }
        try
        {
            switch (DropDownTwo.value)
            {
                case 0:
                    if (LatteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                LatteItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                LatteItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 1:
                    if (MochaItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                MochaItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                MochaItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 2:
                    if (AmericanosItems.Count > 0)
                    {
                        for (int i = 0; i < SectionOneObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                AmericanosItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                AmericanosItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 3:
                    if (CappoccinoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                CappoccinoItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                CappoccinoItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 4:
                    if (MacchiatoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                MacchiatoItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                MacchiatoItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 5:
                    if (FlatWhiteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                FlatWhiteItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                FlatWhiteItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 6:
                    if (IrishCoffeeItems.Count > 0)
                    {
                        for (int i = 0; i < SectionTwoObjects.Length; i++)
                        {
                            if (SectionTwoObjects[i].transform.childCount == 0)
                            {
                                IrishCoffeeItems[i].transform.position = SectionTwoObjects[i].transform.position;
                                IrishCoffeeItems[i].transform.SetParent(SectionTwoObjects[i].transform);
                            }
                        }
                    }
                    break;
            }

        }
        catch
        {

        }
        try
        {
            switch (DropDownThree.value)
            {
                case 0:
                    if (LatteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                LatteItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                LatteItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 1:
                    if (MochaItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                MochaItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                MochaItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 2:
                    if (AmericanosItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                AmericanosItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                AmericanosItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 3:
                    if (CappoccinoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                CappoccinoItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                CappoccinoItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 4:
                    if (MacchiatoItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                MacchiatoItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                MacchiatoItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 5:
                    if (FlatWhiteItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                FlatWhiteItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                FlatWhiteItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
                case 6:
                    if (IrishCoffeeItems.Count > 0)
                    {
                        for (int i = 0; i < SectionThreeObjects.Length; i++)
                        {
                            if (SectionThreeObjects[i].transform.childCount == 0)
                            {
                                IrishCoffeeItems[i].transform.position = SectionThreeObjects[i].transform.position;
                                IrishCoffeeItems[i].transform.SetParent(SectionThreeObjects[i].transform);
                            }
                        }
                    }
                    break;
            }

        }
        catch
        {

        }
    }

    private void GoToCoffeeCreation() 
    {
        CoffeeCreationUI.SetActive(true);
    }
    private void LoadButtons() 
    {
        CloseMenu.onClick.AddListener(CloseMenuUI);
        OpenMenu.onClick.AddListener(OpenMenuUI);
    }
    private void CloseMenuUI() 
    {
        MasterUIController GetUI = FindObjectOfType<MasterUIController>();
        GetUI.isActive = false;
        CoffeeUI.SetActive(false);
    }
    private void OpenMenuUI()
    {
        CoffeeUI.SetActive(true);
    }

}
