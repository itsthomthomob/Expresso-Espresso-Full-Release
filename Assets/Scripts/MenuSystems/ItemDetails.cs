using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ItemDetails : MonoBehaviour, IPointerClickHandler
{
    GameObject myParent;
    public Sprite ItemImage;
    public string ItemName;
    public int RequiredLevel;
    public bool isSelected;
    public ItemCategory myCategory;

    public bool checkedItems = false;
    CoffeeCreationSystem GetCSS;

    private void Awake()
    {
        GetCSS = FindObjectOfType<CoffeeCreationSystem>();
    }

    private void Start()
    {
        myParent = gameObject;
        Image parentImage = myParent.GetComponent<Image>();
        parentImage.sprite = ItemImage;
        TMP_Text parentName = myParent.GetComponentInChildren<TMP_Text>();
        parentName.text = ItemName;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;
        checkedItems = false;

        if (GetCSS.SelectedItems.Contains(this))
        {
            if (myCategory == ItemCategory.CoffeeType)
            {
                GetCSS.CoffeeExpense -= 0.20f;
                GetCSS.CurrentExpense = GetCSS.CurrentExpense - 0.20f;
            }
            else if (myCategory == ItemCategory.Dairy)
            {
                GetCSS.DairyExpense -= 0.20f;
                GetCSS.CurrentExpense = GetCSS.CurrentExpense - 0.20f;

            }
            else if (myCategory == ItemCategory.Syrups)
            {
                GetCSS.SyrupExpense -= 0.20f;
                GetCSS.CurrentExpense = GetCSS.CurrentExpense - 0.20f;
            }
            GetCSS.SelectedItems.Remove(this);
            isSelected = false;
        }

        if (myCategory == ItemCategory.Syrups)
        {
            if (GetCSS.CurrentSyrups.Contains(this))
            {
                GetCSS.CurrentSyrups.Remove(this);
            }
        }

        if (myCategory == ItemCategory.CoffeeType)
        {
            GetCSS.CoffeeExpense += 0.20f;
            GetCSS.CurrentExpense = GetCSS.CurrentExpense + 0.20f;
        }
        else if (myCategory == ItemCategory.Dairy)
        {
            GetCSS.DairyExpense += 0.20f;
            GetCSS.CurrentExpense = GetCSS.CurrentExpense + 0.20f;

        }
        else if (myCategory == ItemCategory.Syrups)
        {
            GetCSS.SyrupExpense += 0.20f;
            GetCSS.CurrentExpense = GetCSS.CurrentExpense + 0.20f;
        }

        GetCSS.SelectedItem = this;
    }
}
