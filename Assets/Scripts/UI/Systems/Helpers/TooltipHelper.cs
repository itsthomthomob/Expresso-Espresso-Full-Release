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
    public GameObject ghostTile;

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
                    ghostTile.SetActive(false);

                    TileName.text = "   Rough, Pale Floor";
                    TileDetails.text = "A rough pale wooden floor.";
                    break;
                case "Floor_2":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Detailed Pale Floor";
                    TileDetails.text = "A pale wooden floor with more planks.";
                    break;
                case "Floor_3":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Checkered Floor";
                    TileDetails.text = "A checkered floor tile.";
                    break;
                case "Floor_4":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Diagnol White Floor";
                    TileDetails.text = "A diagnol, white floor tile.";
                    break;
                case "Floor_5":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Smooth Wooden Brown";
                    TileDetails.text = "A smooth, brown wooden floor.";
                    break;
                case "Floor_6":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Rough Wooden Brown";
                    TileDetails.text = "A rough, brown wooden floor.";
                    break;
                case "Floor_7":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Pale Wooden Floor";
                    TileDetails.text = "A pale wooden floor.";
                    break;
                case "Wall_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Red Brick Wall";
                    TileDetails.text = "A simple red brick wall.";
                    break;
                case "Wall_2":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   White Plaster Wall";
                    TileDetails.text = "A plastered wall with green on bottom.";
                    break;
                case "Wall_3":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Pale Plaster Wall";
                    TileDetails.text = "A plastered white wall.";
                    break;
                case "Wall_4":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Grey Brick Wall";
                    TileDetails.text = "A simple grey brick wall.";
                    break;
                case "Espresso-Lvl_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Espresso Machine Level 1";
                    TileDetails.text = "A low-end espresso machine.";
                    break;
                case "Brewer-Lvl_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Brewer Level 1";
                    TileDetails.text = "First level of a Brewing machine.";
                    break;
                case "Roastery":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Roaster";
                    TileDetails.text = "A roasting machine to make your coffee beans nice and warm.";
                    break;
                case "Table_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Smooth Wooden Table";
                    TileDetails.text = "A smooth wooden table.";
                    break;
                case "Chair_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Smooth Wooden Chair";
                    TileDetails.text = "A smooth wooden chair.";
                    break;
                case "Table_2":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Rough Wooden Table";
                    TileDetails.text = "A rough wooden table.";
                    break;
                case "Chair_2":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Rough Wooden Chair";
                    TileDetails.text = "A rough wooden chair.";
                    break;
                case "Table_3":
                    TooltipUI.SetActive(true);

                    TileName.text = "   Briar Wooden Table";
                    TileDetails.text = "A wooden table made from Briar wood.";
                    break;
                case "Chair_3":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Briar Wooden Chair";
                    TileDetails.text = "A wooden chair made from Briar wood.";
                    break;
                case "Counter_1":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Briar Wooden Counter";
                    TileDetails.text = "A wooden counter for your Baristas to set drinks on." + '\n' + "Also increases customer retention.";
                    break;
                case "Counter_2":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Marble Counter";
                    TileDetails.text = "A marble counter.";
                    break;
                case "Counter_3":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Marble Red Counter";
                    TileDetails.text = "A marble counter with a red, triangle pattern.";
                    break;
                case "Barstool":
                    TooltipUI.SetActive(true);
                    ghostTile.SetActive(false);

                    TileName.text = "   Barstool";
                    TileDetails.text = "A barstool for your customers to sit at.";
                    break;
                default:
                    TooltipUI.SetActive(false);
                    ghostTile.SetActive(true);

                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.SetActive(false);
    }
}
