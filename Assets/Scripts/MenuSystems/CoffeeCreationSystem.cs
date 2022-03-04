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
    public ItemDetails SelectedItem;
    public GameObject prefabItemMenu;

    private void Start()
    {
        AddItemButton.onClick.AddListener(CreateNewItem);
        BackToMenu.onClick.AddListener(GoBackToMenu);
        GetMenu = FindObjectOfType<MenuManagementSystem>();
    }
    private void Update()
    {
        FindSelectedItems();
        ManageItemCategories();
        UpdateItemImage();
        ItemCostText.text = "$" + ItemCostInput.value.ToString() +".00";
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
                        SelectedItems.Remove(SelectedItems[i]);
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
                int index = 0;
                //for (int j = 0; j < SelectedItems.Count; j++)
                //{
                //    if (SelectedItems[j].myCategory != ItemCategory.CoffeeType || SelectedItems[j].myCategory != ItemCategory.Dairy)
                //    {
                //        contents.text = SelectedItems[i].ItemName + contents.text;
                //    }
                //}

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

    private void AddToPhysicalMenu(GameObject item, MenuItem itemDetails) 
    {
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
    private void GoBackToMenu() 
    {
        CoffeeCreationUI.SetActive(false);
    }
}
