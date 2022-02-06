using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isOver;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        if (trigger != null)
        {
            trigger.triggers.Add(entry);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TileConstruction getCS = FindObjectOfType<TileConstruction>();
        isOver = true;
        getCS.isOverUI = true;
    }

    public void OnDragDelegate(PointerEventData data) 
    {
        TileConstruction getCS = FindObjectOfType<TileConstruction>();
        isOver = true;
        getCS.isOverUI = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TileConstruction getCS = FindObjectOfType<TileConstruction>();
        isOver = false;
        getCS.isOverUI = false;
    }
}