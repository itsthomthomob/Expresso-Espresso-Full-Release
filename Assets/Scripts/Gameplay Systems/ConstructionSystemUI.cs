using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConstructionSystemUI : MonoBehaviour
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
        Floor6,
        Wall1,
        Brewing1,
        Brewing2,
        Brewing3,
        Espresso1,
        Espresso2,
        Espresso3,
        Roastery
    }

    [Header("Selected Tiles")]
    public SelectedTileType currentTileType;
    public SelectedTile currentTile;
    public bool onConstruction;
    public EntityGrid grid;

    [Header("UI Objects")]
    public Dropdown tileTypeDD;
    public GameObject tileTypeSelector;
    public GameObject tileSelector;
    public GameObject ttButtonBackground;
    public GameObject scrollbar;

    [Header("Container Objects")]
    public GameObject buildingTiles;
    public GameObject furnitureTiles;
    public GameObject machineTiles;
    public Button constructionButton;

    [Header("Tile - Floor - Buttons")]
    public Button Tile_Floor1;
    public Button Tile_Floor2;
    public Button Tile_Floor3;
    public Button Tile_Floor4;
    public Button Tile_Floor5;

    [Header("Tile - Wall - Buttons")]
    public Button Tile_Wall1;

    [Header("Tile - Machine - Buttons")]
    public Button Brewer1;
    public Button Brewer2;
    public Button Brewer3;
    public Button Espresso1;
    public Button Espresso2;
    public Button Espresso3;
    public Button Roastery;

    private void Start()
    {
        grid = FindObjectOfType<EntityGrid>();
        SetConstructionUI();
        SetButtons();
    }

    private void Update()
    {
        ManageConstructionUI();
        CheckToggles();
        CheckSelections();
    }

    public void SetButtons() 
    {
        Tile_Floor1.onClick.AddListener(SelectedFloorOne);
        Tile_Floor2.onClick.AddListener(SelectedFloorTwo);
        Tile_Floor3.onClick.AddListener(SelectedFloorThree);
        Tile_Floor4.onClick.AddListener(SelectedFloorFour);
        Tile_Floor5.onClick.AddListener(SelectedFloorFive);
        Tile_Wall1.onClick.AddListener(SelectedWall1);
        Brewer1.onClick.AddListener(SelectedBrewerOne);
        Espresso1.onClick.AddListener(SelectedEspressoOne);
        Roastery.onClick.AddListener(SelectedRoastery);
    }

    private void SetConstructionUI()
    {
        // Set initial state
        currentTileType = SelectedTileType.none;

        // Construction UI elements
        scrollbar.SetActive(false);
        buildingTiles.SetActive(false);
        tileTypeSelector.SetActive(false);
        tileSelector.SetActive(false);
        ttButtonBackground.SetActive(false);

        // UI Icon
        constructionButton.onClick.AddListener(ClickedConstruction);

        onConstruction = false;
    }
    private void ManageConstructionUI() 
    {
        if (onConstruction)
        {
            currentTileType = SelectedTileType.Building;

            scrollbar.SetActive(true);
            tileTypeSelector.SetActive(true);
            tileSelector.SetActive(true);
            ttButtonBackground.SetActive(true);

            buildingTiles.SetActive(true);
        }
        else
        {
            currentTileType = SelectedTileType.none;
            currentTile = SelectedTile.none;

            scrollbar.SetActive(false);
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

    private void SelectedFloorOne() 
    {
        currentTile = SelectedTile.Floor1;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters) { }
                else { grid.Create<EntityFloor>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position); }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorTwo()
    {
        currentTile = SelectedTile.Floor2;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else
                {
                    grid.Create<EntityFloorTwo>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorThree()
    {
        currentTile = SelectedTile.Floor3;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else
                {
                    grid.Create<EntityFloorThree>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorFour()
    {
        currentTile = SelectedTile.Floor4;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else
                {
                    grid.Create<EntityFloorFour>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                }
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorFive()
    {
        currentTile = SelectedTile.Floor5;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else 
                { 
                    grid.Create<EntityFloorFive>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedWall1()
    {
        currentTile = SelectedTile.Wall1;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else
                {
                    grid.Create<EntityWall>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedBrewerOne()
    {
        currentTile = SelectedTile.Brewing1;
    }
    private void SelectedEspressoOne()
    {
        currentTile = SelectedTile.Espresso1;
    }
    private void SelectedRoastery()
    {
        currentTile = SelectedTile.Roastery;
    }
}