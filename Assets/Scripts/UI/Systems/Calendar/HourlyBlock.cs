using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HourlyBlock : MonoBehaviour, IPointerDownHandler
{
    public int Day;
    public int Hour;

    public int GetDay() { return Day;  }
    public int GetHour() { return Hour; }

    public void OnPointerDown(PointerEventData eventData)
    {
        Image MyImage = gameObject.GetComponent<Image>();
        MyImage.color = Color.red;
        Debug.Log("Changed Color on: " + "Day " + Day + " Hour " + Hour);
    }

    public void SetDay(int newDay) { Day = newDay; }
    public void SetHour(int newHour) { Hour = newHour; }
}
