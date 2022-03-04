using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManagementSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public GameObject CoffeeUI;
    public GameObject CoffeeCreationUI;
    public Button CloseMenu;
    public Button OpenMenu;

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

    private void Start()
    {
        LoadButtons();
        AddItemSectionTwo.onClick.AddListener(GoToCoffeeCreation);
        AddItemSectionOne.onClick.AddListener(GoToCoffeeCreation);
        AddItemSectionThree.onClick.AddListener(GoToCoffeeCreation);
        DropDownOne.onValueChanged.AddListener(ClearSectionObjects);
    }

    private void Update()
    {
        UpdateDropDownChanges();
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
        /// TO-DO:
        /// - On drop down change
        ///   - Get dropdown value
        ///   - Change children of ItemSpawns <--
        ///     - Destroy all children of ItemSpawns
        ///     - Add all children from selected dropdown gameobject list
        ///     
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
        CoffeeUI.SetActive(false);
    }
    private void OpenMenuUI()
    {
        CoffeeUI.SetActive(true);
    }

}
