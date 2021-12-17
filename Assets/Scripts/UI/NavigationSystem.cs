using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavigationSystem : MonoBehaviour
{
    public enum SelectedTileType
    {
        none,
        Building,
        Furniture,
        Machines
    }
    public enum SelectedTile
    {
        none,
        Floor1,
        Floor2,
        Floor3,
        Floor4,
        Floor5,
        Wall1
    }

    [Header("Selected Tiles")]
    public SelectedTileType currentTileType;
    public SelectedTile currentTile;
    public bool onConstruction;

    [Header("Buttons/GameObjects")]
    public Dropdown tileTypeDD;
    public GameObject tileTypeSelector;
    public GameObject tileSelector;
    public GameObject ttButtonBackground;

    public GameObject buildingTiles;
    public GameObject furnitureTiles;
    public GameObject machineTiles;

    public Button constructionButton;

    private void Start()
    {
        SetConstructionUI();
    }

    private void Update()
    {
        ManageConstructionUI();
        CheckToggles();
        CheckSelections();
    }
    private void SetConstructionUI()
    {
        currentTileType = SelectedTileType.none;

        buildingTiles.SetActive(false);
        tileTypeSelector.SetActive(false);
        tileSelector.SetActive(false);
        ttButtonBackground.SetActive(false);

        constructionButton.onClick.AddListener(ClickedConstruction);

        onConstruction = false;
    }
    private void ManageConstructionUI() 
    {
        if (onConstruction)
        {
            currentTileType = SelectedTileType.Building;

            tileTypeSelector.SetActive(true);
            tileSelector.SetActive(true);
            ttButtonBackground.SetActive(true);


            buildingTiles.SetActive(true);
        }
        else
        {
            currentTileType = SelectedTileType.none;

            tileTypeSelector.SetActive(false);
            tileSelector.SetActive(false);
            ttButtonBackground.SetActive(false);

            buildingTiles.SetActive(false);
            furnitureTiles.SetActive(false);
            machineTiles.SetActive(false);
        }
    }
    private void CheckToggles() 
    {
        if (tileTypeDD.value == 0)
        {
            if (onConstruction)
            {
                ClickedCon_Building();
            }
        }
        else if (tileTypeDD.value == 1)
        {
            ClickedCon_Furniture();
        }
        else if (tileTypeDD.value == 2)
        {
            ClickedCon_Machines();
        }
    }
    private void CheckSelections()
    {
        switch (currentTileType)
        {
            case SelectedTileType.Building:
                buildingTiles.SetActive(true);

                furnitureTiles.SetActive(false);
                machineTiles.SetActive(false);
                break;

            case SelectedTileType.Furniture:
                furnitureTiles.SetActive(true);

                machineTiles.SetActive(false);
                buildingTiles.SetActive(false);
                break;

            case SelectedTileType.Machines:
                machineTiles.SetActive(true);

                buildingTiles.SetActive(false);
                furnitureTiles.SetActive(false);
                break;
            case SelectedTileType.none:
                machineTiles.SetActive(false);

                buildingTiles.SetActive(false);
                furnitureTiles.SetActive(false);
                break;
        }

        switch (currentTile)
        {
            case SelectedTile.Floor1:
                break;
            case SelectedTile.Floor2:
                break;
            case SelectedTile.Floor3:
                break;
            case SelectedTile.Floor4:
                break;
            case SelectedTile.Floor5:
                break;
            case SelectedTile.Wall1:
                break;
            default:
                break;
        }
    }
    private void ClickedConstruction()
    {
        onConstruction = !onConstruction;
        if (onConstruction)
        {
            currentTileType = SelectedTileType.Building;
        }
        else
        {
            currentTileType = SelectedTileType.none;
        }
    }
    private void ClickedCon_Building()
    {
        currentTileType = SelectedTileType.Building;
    }
    private void ClickedCon_Furniture()
    {
        currentTileType = SelectedTileType.Furniture;

    }
    private void ClickedCon_Machines()
    {
        currentTileType = SelectedTileType.Machines;

    }
}