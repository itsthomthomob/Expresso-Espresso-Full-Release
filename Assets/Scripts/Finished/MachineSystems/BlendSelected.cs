using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BlendSelected : MonoBehaviour, IPointerClickHandler
{
    public Slider RoasterTemperature;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.selectedObject != null)
        {
            if (eventData.selectedObject.gameObject.name == "Blend_1" ||
                eventData.selectedObject.gameObject.name == "Blend_2" ||
                eventData.selectedObject.gameObject.name == "Blend_3" ||
                eventData.selectedObject.gameObject.name == "Blend_4" ||
                eventData.selectedObject.gameObject.name == "Blend_5" ||
                eventData.selectedObject.gameObject.name == "Blend_6")
            {
                GameObject blendInfo = eventData.selectedObject.gameObject.transform.GetChild(0).gameObject;
                RoasterTemperature.value = float.Parse(blendInfo.transform.GetChild(2).GetComponent<TMP_Text>().text);
                Debug.Log("Changed value.");
            }
            else
            {
                return;
            }
        }
    }
}
