using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MenuSection : MonoBehaviour
{
    ItemType MyType;
    public List<MenuItem> MyItems = new List<MenuItem>();
    public GameObject[] MySpawns;

    public string GetMyType() 
    { 
        return MyType.ToString();
    }

    public GameObject FindEmptySpawnItem() 
    {
        for (int i = 0; i < MySpawns.Length; i++)
        {
            if (MySpawns[i].transform.childCount == 0)
            {
                return MySpawns[i];
            }
        }
        return null;
    }
    public void SetSectionType(ItemType newType) 
    {
        MyType = newType;
    }

    public ItemType GetSectionType() 
    {
        return MyType;
    }
    public void ClearItems() 
    {
        MyItems = new List<MenuItem>();
    }
    public void AddItem(MenuItem newItem) 
    {
        if (MyItems.Count + 1 > 5)
        {
            // Do nothing
        }
        else 
        { 
            MyItems.Add(newItem);
        }
    }
    public void RemoveItem(MenuItem newItem)
    {
        MyItems.Remove(newItem);
    }
    public GameObject[] GetSpawnObjects() 
    {
        return MySpawns;
    }

    public List<MenuItem> GetItems() 
    {
        return MyItems;
    }

    public void SetSpawnObjects(GameObject[] objects) 
    {
        MySpawns = objects;
    }
}
