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
    public bool isSelected = false;
    public ItemCategory myCategory;
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
        isSelected = !isSelected;

        CoffeeCreationSystem GetCSS = FindObjectOfType<CoffeeCreationSystem>();
        GetCSS.SelectedItem = this;
    }
}
