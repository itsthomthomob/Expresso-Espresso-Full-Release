using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver;
    TileConstruction getCS;

    private void Start()
    {
        getCS = FindObjectOfType<TileConstruction>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (name == "Weather")
        {
            isOver = false;
            getCS.isOverUI = false;
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (name == "Weather")
        {
            isOver = true;
            getCS.isOverUI = true;
        }
    }
}