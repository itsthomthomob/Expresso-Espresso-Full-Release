using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoffeeCreationSystem : MonoBehaviour
{
    [Header("Master UI Objects")]
    public MenuManagementSystem GetMenu;
    public GameObject CoffeeCreationUI;
    public Button BackToMenu;
    public Button AddItemButton;
    public Slider ItemCostInput;
    public TMP_Text ItemCostText;
    public TMP_InputField ItemNameInput;

    [Header("Items")]
    public ItemDetails[] AllItems;
    public List<ItemDetails> SelectedItems = new List<ItemDetails>();
    public List<ItemDetails> CurrentSyrups = new List<ItemDetails>();
    public ItemDetails SelectedItem;
    public GameObject prefabItemMenu;

    [Header("Coffee Building Images")]
    public GameObject CoffeeCup;
    public Sprite EmptyCup;
    public Sprite HasMilk;
    public Sprite HasSyrup;
    public Sprite HasSyrups;

    [Header("Calculate Coffee Expenses")]
    public TMP_Text DairyCost;
    public TMP_Text SyrupCosts;
    public TMP_Text TimeCosts;
    public TMP_Text TotalCosts;
    public float CurrentExpense;
    public float SyrupExpense;
    public float CoffeeExpense;
    public float DairyExpense;
    public bool hasCoffee;
    public bool hasDairy;
    
    private void Start()
    {
        AddItemButton.onClick.AddListener(CreateNewItem);
        BackToMenu.onClick.AddListener(GoBackToMenu);
        GetMenu = FindObjectOfType<MenuManagementSystem>();
    }
    private void Update()
    {
        TrackCurrentItems();
        FindSelectedItems();
        ManageItemCategories();
        UpdateItemImage();
        BuildCoffeeSprites();
        UpdateCostTexts();
    }

    private void TrackCurrentItems() 
    {
        if (SelectedItem != null && SelectedItem.checkedItems == false)
        {
            if (!SelectedItems.Contains(SelectedItem))
            {
                Debug.Log("Checking Items");
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    if (SelectedItems[i].myCategory == ItemCategory.CoffeeType &&
                        SelectedItem.myCategory == ItemCategory.CoffeeType)
                    {
                        // There's already a coffee type and the selected item is a coffee type
                        CoffeeExpense -= 0.20f;
                        CurrentExpense -= 0.20f;
                        SelectedItem.checkedItems = true;
                        break;
                    }
                    if (SelectedItems[i].myCategory == ItemCategory.Dairy &&
                        SelectedItem.myCategory == ItemCategory.Dairy)
                    {
                        DairyExpense -= 0.20f;
                        CurrentExpense -= 0.20f;
                        SelectedItem.checkedItems = true;
                        break;
                    }
                }
            }
        }
    }

    private void UpdateCostTexts() 
    {
        if (SelectedItems.Count == 0)
        {
            DairyExpense = 0.0f;
            SyrupExpense = 0.0f;
            CoffeeExpense = 0.0f;
            CurrentExpense = 0.0f;
        }
        if (SelectedItems.Count == 1)
        {
            if (SelectedItems[0].myCategory == ItemCategory.CoffeeType)
            {
                DairyExpense = 0.0f;
                SyrupExpense = 0.0f;
                CoffeeExpense = 0.20f;
                CurrentExpense = 0.20f;
            }
            else if (SelectedItems[0].myCategory == ItemCategory.Dairy) 
            {
                DairyExpense = 0.20f;
                SyrupExpense = 0.0f;
                CoffeeExpense = 0.00f;
                CurrentExpense = 0.20f;
            }
            else if (SelectedItems[0].myCategory == ItemCategory.Syrups)
            {
                DairyExpense = 0.00f;
                SyrupExpense = 0.0f;
                CoffeeExpense = 0.80f;
                CurrentExpense = 0.80f;
            }
        }
        
        SyrupExpense = 0.80f * CurrentSyrups.Count;
        CurrentExpense = SyrupExpense + CoffeeExpense + DairyExpense;

        DairyCost.text = "$" + DairyExpense.ToString();
        SyrupCosts.text = "$" + SyrupExpense.ToString();
        TimeCosts.text = "$" + CoffeeExpense.ToString();
        TotalCosts.text = "$" + CurrentExpense.ToString();
    }
    private void UpdateItemImage() 
    {
        for (int i = 0; i < AllItems.Length; i++)
        {
            if (SelectedItems.Contains(AllItems[i]))
            {
                Image GetSprite = AllItems[i].GetComponent<Image>();
                GetSprite.color = Color.grey;
            }
            else 
            {
                Image GetSprite = AllItems[i].GetComponent<Image>();
                GetSprite.color = new Color(166, 166, 166,100);
            }
        }
    }
    private void ManageItemCategories()
    {
        if (SelectedItem != null)
        {
            if (SelectedItem.myCategory == ItemCategory.Dairy)
            {
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    if (SelectedItems[i].myCategory == ItemCategory.Dairy)
                    {
                        // Already selected a dairy type
                        SelectedItems[i].isSelected = false;
                        if (SelectedItems.Contains(SelectedItems[i]))
                        {
                            SelectedItems.Remove(SelectedItems[i]);
                        }
                        if (!SelectedItems.Contains(SelectedItem))
                        {
                            SelectedItems.Add(SelectedItem);
                        }
                        break;
                    }
                }
            }
            if (SelectedItem.myCategory == ItemCategory.CoffeeType)
            {
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    if (SelectedItems[i].myCategory == ItemCategory.CoffeeType)
                    {
                        // Already selected a coffee type
                        SelectedItems[i].isSelected = false;
                        SelectedItems.Remove(SelectedItems[i]);
                        if (!SelectedItems.Contains(SelectedItem))
                        {
                            SelectedItems.Add(SelectedItem);
                        }
                        break;
                    }
                }
            }
        }
    }
    private void FindSelectedItems() 
    {
        AllItems = FindObjectsOfType<ItemDetails>();

        for (int i = 0; i < AllItems.Length; i++)
        {
            if (AllItems[i].isSelected == true)
            {
                if (!SelectedItems.Contains(AllItems[i]))
                {
                    SelectedItems.Add(AllItems[i]);
                }
            }
        }
    }
    private void CreateNewItem() 
    {
        GameObject newItem = Instantiate(prefabItemMenu);
        MenuItem menuItem = newItem.AddComponent<MenuItem>();
        menuItem.SetMyObject(newItem);
        menuItem.SetPrice(ItemCostInput.value);
        menuItem.SetExpense(CurrentExpense);
        Transform getTF = newItem.transform;

        for (int i = 0; i < getTF.childCount; i++)
        {
            Transform getChild = getTF.GetChild(i);
            if (getChild.name == "ItemName")
            {
                TMP_Text ItemName = getChild.GetComponent<TMP_Text>();
                ItemName.text = ItemNameInput.text;
                menuItem.ItemName = ItemNameInput.text;
            }
            else if (getChild.name == "ItemContents")
            {
                TMP_Text contents = getChild.GetComponent<TMP_Text>();
                contents.text = SelectedItems[0].ItemName;
                int ContentAmount = SelectedItems.Count;
            }
            else if (getChild.name == "ItemPrice")
            {
                TMP_Text ItemPrice = getChild.GetComponent<TMP_Text>();
                ItemPrice.text = ItemCostInput.value.ToString();
            }
        }

        for (int i = 0; i < SelectedItems.Count; i++)
        {
            if (SelectedItems[i].ItemName == "Latte")
            {
                menuItem.SetDrinkType(ItemType.Lattes);
            }
            if (SelectedItems[i].ItemName == "Mochas")
            {
                menuItem.SetDrinkType(ItemType.Mochas);
            }
            if (SelectedItems[i].ItemName == "Americano")
            {
                menuItem.SetDrinkType(ItemType.Americanos);
            }
            if (SelectedItems[i].ItemName == "Cappoccino")
            {
                menuItem.SetDrinkType(ItemType.Cappoccino);
            }
            if (SelectedItems[i].ItemName == "Macchiato")
            {
                menuItem.SetDrinkType(ItemType.Macchiato);
            }
            if (SelectedItems[i].ItemName == "Mocha")
            {
                menuItem.SetDrinkType(ItemType.Mocha);
            }
            if (SelectedItems[i].ItemName == "FlatWhite")
            {
                menuItem.SetDrinkType(ItemType.FlatWhite);
            }
            if (SelectedItems[i].ItemName == "IrishCoffee")
            {
                menuItem.SetDrinkType(ItemType.IrishCoffee);
            }
        }

        newItem.name = menuItem.ItemName;
        AddToPhysicalMenu(newItem, menuItem);
    }
    public void AddToPhysicalMenu(GameObject item, MenuItem itemDetails) 
    {
        Debug.Log("Added: " + itemDetails.GetDrinkType().ToString());
        switch (itemDetails.GetDrinkType())
        {
            case ItemType.Lattes:
                GetMenu.LatteItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.Mochas:
                GetMenu.MochasItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.Americanos:
                GetMenu.AmericanosItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.Cappoccino:
                GetMenu.CappoccinoItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.Macchiato:
                GetMenu.MacchiatoItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.Mocha:
                GetMenu.MochaItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.FlatWhite:
                GetMenu.FlatWhiteItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
            case ItemType.IrishCoffee:
                GetMenu.IrishCoffeeItems.Add(item);
                CoffeeCreationUI.SetActive(false);

                break;
        }
        CoffeeCreationUI.SetActive(false);
    }
    private void BuildCoffeeSprites() 
    {
        Image GetImage = CoffeeCup.GetComponent<Image>();

        if (SelectedItems.Count == 0)
        {
            GetImage.sprite = EmptyCup;
        }
        else if (SelectedItems.Count > 0)
        { 
            for (int i = 0; i < SelectedItems.Count; i++)
            {
                if (SelectedItems[i].myCategory == ItemCategory.CoffeeType ||
                    SelectedItems[i].myCategory == ItemCategory.Dairy)
                {
                    GetImage.sprite = HasMilk;
                }
                if (SelectedItems[i].myCategory == ItemCategory.Syrups)
                {
                    if (!CurrentSyrups.Contains(SelectedItems[i]))
                    {
                        CurrentSyrups.Add(SelectedItems[i]);
                    }
                }
            }
            if (CurrentSyrups.Count == 1)
            {
                GetImage.sprite = HasSyrup;
            }
            if (CurrentSyrups.Count > 1)
            {
                GetImage.sprite = HasSyrups;
            }
        }
    }
    private void GoBackToMenu() 
    {
        CoffeeCreationUI.SetActive(false);
    }
}
