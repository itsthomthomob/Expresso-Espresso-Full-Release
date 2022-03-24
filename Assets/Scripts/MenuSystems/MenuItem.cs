using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MenuItem : MonoBehaviour
{
    [Header("Attributes")]
    float price;
    float expense;
    int menuID;
    public string ItemName;
    public bool isItemActive;
    List<string> contents = new List<string>();
    public ItemType DrinkType;
    GameObject myObject;
    MenuSection mySection;

    public void LoadDrinkType(string newType) 
    {
        switch (newType)
        {
            case "Black":
                DrinkType = ItemType.Black;
                break;
            case "Lattes":
                DrinkType = ItemType.Lattes;
                break;
            case "Mochas":
                DrinkType = ItemType.Mochas;

                break;
            case "Americanos":
                DrinkType = ItemType.Americanos;

                break;
            case "Espresso":
                DrinkType = ItemType.Espresso;

                break;
            case "Cappoccino":
                DrinkType = ItemType.Cappoccino;

                break;
            case "Macchiato":
                DrinkType = ItemType.Macchiato;

                break;
            case "Mocha":
                DrinkType = ItemType.Mocha;

                break;
            case "FlatWhite":
                DrinkType = ItemType.FlatWhite;

                break;
            case "IrishCoffee":
                DrinkType = ItemType.IrishCoffee;

                break;
            default:
                break;
        }
    }

    public void SetName(string newName) 
    { 
        ItemName = newName;
    }

    public int GetMenuID() 
    { 
        return menuID;
    }

    public string GetItemName() 
    {
        return ItemName;
    }

    public bool GetIsActive() 
    {
        return isItemActive;
    }

    public void SetIsActive(bool newActive) 
    { 
        isItemActive = newActive;
    }

    public void SetExpense(float newExpense) 
    {
        expense = newExpense;
    }
    public float GetExpense()
    {
        return expense;
    }
    public void SetDrinkType(ItemType newType) 
    {
        DrinkType = newType;
    }

    public ItemType GetDrinkType() 
    {
        return DrinkType;
    }
    public void SetSection(MenuSection newSection) 
    {
        mySection = newSection;
    }
    public MenuSection GetSection() 
    {
        return mySection;
    }

    public GameObject GetMyObject()
    {
        return myObject;
    }
    public void SetMyObject(GameObject newObj) 
    {
        myObject = newObj;
    }
    public GameObject LoadMyObject(string newObj) 
    {
        MenuManagementSystem menu = FindObjectOfType<MenuManagementSystem>();
        menu.CoffeeUI.SetActive(true);
        GameObject findOBJ = GameObject.Find(newObj);
        menu.CoffeeUI.SetActive(false);
        return findOBJ;
    }
    public float GetPrice()
    {
        return price;
    }

    public void SetPrice(float newPrice)
    {
        price = newPrice;
    }

    public int GetID()
    {
        return menuID;
    }

    public void SetID(int newID)
    {
        menuID = newID;
    }

    public List<string> GetContents()
    {
        return contents;
    }

    public void SetContents(List<string> newContents) 
    {
        contents = new List<string>(newContents);
    }

    public void AddContent(string newContent) 
    {
        contents.Add(newContent);
    }

    public void RemoveContent(string content) 
    {
        contents.Remove(content);
    }
    
}
