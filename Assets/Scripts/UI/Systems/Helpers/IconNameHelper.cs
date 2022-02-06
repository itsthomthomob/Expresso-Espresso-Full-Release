using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class IconNameHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject IconToolTip;
    public TMP_Text NameText;

    private void Start()
    {
        SetTextObj();
    }

    private void SetTextObj() 
    {
        IconToolTip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject != null)
        {
            switch (eventData.pointerEnter.name)
            {
                case "B_Construction":
                    IconToolTip.SetActive(true);

                    NameText.text = "Construction";
                    break;
                case "B_Shop":
                    IconToolTip.SetActive(true);

                    NameText.text = "Store";
                    break;
                case "B_Employees":
                    IconToolTip.SetActive(true);

                    NameText.text = "Management";
                    break;
                case "B_Coffee":
                    IconToolTip.SetActive(true);

                    NameText.text = "Coffee";
                    break;
                case "B_Stats":
                    IconToolTip.SetActive(true);

                    NameText.text = "Store Stats";
                    break;
                case "B_TechTree":
                    IconToolTip.SetActive(true);

                    NameText.text = "Technologies";
                    break;
                default:
                    IconToolTip.SetActive(false);
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            switch (eventData.pointerEnter.name)
            {
                default:
                    IconToolTip.SetActive(false);
                    break;
            }
        }
    }

}
