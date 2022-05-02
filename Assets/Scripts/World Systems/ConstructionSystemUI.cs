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
        Floor7,
        Wall1,
        Wall2,
        Wall3,
        Wall4,
        Table1,
        Chair1,
        Counter1,
        Barstool1,
        Brewing1,
        Brewing2,
        Brewing3,
        Espresso1,
        Espresso2,
        Espresso3,
        Roastery,
        Register
    }

    [Header("Master UI Obj")]
    public GameObject ConstructionUI;
    public AudioSource GetAudio;
    public AudioClip multiTile;

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
    public Button Tile_Floor6;
    public Button Tile_Floor7;

    [Header("Tile - Wall - Buttons")]
    public Button Tile_Wall1;
    public Button Tile_Wall2;
    public Button Tile_Wall3;
    public Button Tile_Wall4;

    [Header("Tile - Furniture - Buttons")]
    public Button Table1;
    public Button Chair1;
    public Button Counter1;
    public Button Barstool;

    [Header("Tile - Machine - Buttons")]
    public Button Brewer1;
    public Button Brewer2;
    public Button Brewer3;
    public Button Espresso1;
    public Button Espresso2;
    public Button Espresso3;
    public Button Roastery;
    public Button Register;

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
        Tile_Floor6.onClick.AddListener(SelectedFloorSix);
        Tile_Floor7.onClick.AddListener(SelectedFloorSeven);

        Tile_Wall1.onClick.AddListener(SelectedWall1);
        Tile_Wall2.onClick.AddListener(SelectedWall2);
        Tile_Wall3.onClick.AddListener(SelectedWall3);
        Tile_Wall4.onClick.AddListener(SelectedWall4);

        Table1.onClick.AddListener(SelectedTable1);
        Chair1.onClick.AddListener(SelectedChair1);
        Counter1.onClick.AddListener(SelectedCounter1);
        Barstool.onClick.AddListener(SelectedBarstool1);

        Brewer1.onClick.AddListener(SelectedBrewerOne);
        Espresso1.onClick.AddListener(SelectedEspressoOne);
        Roastery.onClick.AddListener(SelectedRoastery);
        Register.onClick.AddListener(SelectedRegister1);
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
            ConstructionUI.SetActive(true);
            currentTileType = SelectedTileType.Building;
            scrollbar.SetActive(true);
            buildingTiles.SetActive(true);
            tileTypeSelector.SetActive(true);
            tileSelector.SetActive(true);
            ttButtonBackground.SetActive(true);
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
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length >= 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters) { }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloor>()) { }
                else { 
                    EntityBase newEntity = grid.Create<EntityFloor>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);
                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

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
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorTwo>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityFloorTwo>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);
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
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorThree>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityFloorThree>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

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
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorFour>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityFloorFour>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
            }
            GetAudio.PlayOneShot(multiTile);

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
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorFive>()) { }
                else 
                { 
                    EntityBase newEntity = grid.Create<EntityFloorFive>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorSix()
    {
        currentTile = SelectedTile.Floor6;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorSix>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityFloorSix>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedFloorSeven()
    {
        currentTile = SelectedTile.Floor7;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityFloorSeven>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityFloorSeven>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllFloors.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

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
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityWallBrick>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityWallBrick>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllWalls.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedWall2()
    {
        currentTile = SelectedTile.Wall2;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityWallPlaster>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityWallPlaster>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllWalls.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedWall3()
    {
        currentTile = SelectedTile.Wall3;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityWallPale>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityWallPale>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllWalls.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

            FindObjectOfType<TileConstruction>().selectedEntities = new EntityBase[0];
        }
    }
    private void SelectedWall4()
    {
        currentTile = SelectedTile.Wall4;
        if (FindObjectOfType<TileConstruction>().selectedEntities.Length > 0)
        {
            for (int i = 0; i < FindObjectOfType<TileConstruction>().selectedEntities.Length; i++)
            {
                if (FindObjectOfType<TileConstruction>().selectedEntities[i] == null)
                {
                    return;
                }
                if (FindObjectOfType<TileConstruction>().selectedEntities[i].Priority == EntityPriority.Characters)
                {

                }
                else if (FindObjectOfType<TileConstruction>().selectedEntities[i].GetComponent<EntityWallGreyBrick>()) { }
                else
                {
                    EntityBase newEntity = grid.Create<EntityWallGreyBrick>(FindObjectOfType<TileConstruction>().selectedEntities[i].Position);
                    FindObjectOfType<TileConstruction>().AllWalls.Add(newEntity);

                }
                GameObject currentEntity = FindObjectOfType<TileConstruction>().selectedEntities[i].gameObject;
                currentEntity.GetComponent<Image>().material = null;
            }
            GetAudio.PlayOneShot(multiTile);

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
    private void SelectedTable1()
    {
        currentTile = SelectedTile.Table1;
    }
    private void SelectedChair1()
    {
        currentTile = SelectedTile.Chair1;
    }
    private void SelectedCounter1()
    {
        currentTile = SelectedTile.Counter1;
    }
    private void SelectedBarstool1()
    {
        currentTile = SelectedTile.Barstool1;
    }
    private void SelectedRegister1() 
    { 
        currentTile = SelectedTile.Register;

    }
}