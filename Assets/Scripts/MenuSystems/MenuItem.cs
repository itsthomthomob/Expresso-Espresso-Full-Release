using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [Header("Attributes")]
    float price;
    int menuID;
    public string ItemName;
    List<string> contents = new List<string>();
    ItemType DrinkType;
    GameObject myObject;
    MenuSection mySection;

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
