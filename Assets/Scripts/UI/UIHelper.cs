using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver;

    public void OnPointerExit(PointerEventData eventData)
    {
        TileConstruction getCS = FindObjectOfType<TileConstruction>();
        isOver = false;
        getCS.isOverUI = false;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TileConstruction getCS = FindObjectOfType<TileConstruction>();
        isOver = true;
        getCS.isOverUI = true;
    }
}
