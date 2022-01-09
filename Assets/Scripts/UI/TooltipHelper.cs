using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Objects")]
    public GameObject TooltipUI;
    public TMP_Text TileName;
    public TMP_Text TileDetails;

    private void Start()
    {
        TooltipUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.selectedObject != null)
        {
            switch (eventData.pointerEnter.name)
            {
                case "Floor_1":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Checkered Floor";
                    TileDetails.text = "A simple checker pattern.";
                    break;
                case "Floor_2":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Diagonal Wooden White";
                    TileDetails.text = "A simple diagonal wooden floor.";
                    break;
                case "Floor_3":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Suttle Wooden Brown";
                    TileDetails.text = "A rough wooden floor.";
                    break;
                case "Floor_4":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Smooth Wooden Brown";
                    TileDetails.text = "A smooth wooden floor.";
                    break;
                case "Floor_5":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Birch Wooden Floor";
                    TileDetails.text = "A birch wooden floor.";
                    break;
                case "Wall_1":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Red Brick Wall";
                    TileDetails.text = "A simple red brick wall.";
                    break;
                case "Espresso-Lvl_1":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Espresso Machine Level 1";
                    TileDetails.text = "A low-end espresso machine.";
                    break;
                case "Brewer-Lvl_1":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Brewer Level 1";
                    TileDetails.text = "First level of a Brewing machine.";
                    break;
                case "Roastery":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Roaster";
                    TileDetails.text = "A roasting machine to make your coffee beans nice and warm.";
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
                case "Floor_1":
                    TooltipUI.SetActive(false);
                    break;
                case "Floor_2":
                    TooltipUI.SetActive(false);
                    break;
                case "Floor_3":
                    TooltipUI.SetActive(false);
                    break;
                case "Floor_4":
                    TooltipUI.SetActive(false);
                    break;
                case "Floor_5":
                    TooltipUI.SetActive(false);
                    break;
                case "Wall_1":
                    TooltipUI.SetActive(false);
                    break;
            }
        }
    }
}
