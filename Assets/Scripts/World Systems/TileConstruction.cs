using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileConstruction : MonoBehaviour
{
    [Header("Game Controls")]
    EntityGrid Grid;
    public RectTransform root;
    public Vector2Int firstCorner;
    public Vector2Int lastCorner;
    public CafeEconomySystem GetEconomy;

    [Header("Handle Construction Panel")]
    public EventSystem curEvent;
    public Dropdown TileTypeSelector;
    public bool isConstructionOpen = false;
    public bool isDestroyOn = false;
    public bool isOverUI;
    public bool isMouseOverUI;
    public Button B_ConstructionIcon;
    public GameObject ConstructionPanel;
    public GameObject DestroyIcon;

    [Header("Construction Costs")]
    public float RefundRate;
    public int FurnitureCost;
    public int FloorCost;
    public int WallCost;
    public int RoasterCost;
    public int EspressoCost;
    public int RegisterCost;
    public int BrewerCost;

    public enum SelectedTileType
    {
        none,
        Building,
        Furniture,
        Machines
    }
    public enum CurrentTileState 
    { 
        Roaster, Brewer, Register, Espresso,
        S_Floor1, S_Floor2, S_Floor3, S_Floor4, S_Floor5, S_Floor6, S_Floor7,
        S_Wall1, S_Wall2, S_Wall3, S_Wall4,
        S_Table1, S_Table2, S_Table3, S_Table4,
        S_Chair1, S_Chair2, S_Chair3, S_Chair4,
        S_Counter1, S_Counter2, S_Counter3,
        S_Barstool,
        None
    }

    [Header("Tile Buttons")]
    public CurrentTileState curTile = CurrentTileState.None;
    public SelectedTileType currentTileType = SelectedTileType.Building;
    public EntityBase curSelectedTile;
    public GameObject machineTiles;
    public GameObject buildingTiles;
    public GameObject furnitureTiles;
    public Button B_Floor1;
    public Button B_Floor2;
    public Button B_Floor3;
    public Button B_Floor4;
    public Button B_Floor5;
    public Button B_Floor6;
    public Button B_Floor7;

    public Button B_Wall1;
    public Button B_Wall2;
    public Button B_Wall3;
    public Button B_Wall4;

    [Header("Furniture Buttons")]
    public Button B_Table1;
    public Button B_Table2;
    public Button B_Table3;
    public Button B_Table4;
    public Button B_Chair1;
    public Button B_Chair2;
    public Button B_Chair3;
    public Button B_Chair4;
    public Button B_Counter1;
    public Button B_Counter2;
    public Button B_Counter3;
    public Button B_Barstool;
    public Button B_Espresso;
    public Button B_Brewer;
    public Button B_Roaster;
    public Button B_Register;

    [Header("AI Dependent")]
    public List<EntityRegister> AllRegisters = new List<EntityRegister>();

    [Header("Objective Dependent")]
    public List<EntityBase> AllChairs = new List<EntityBase>();
    public List<EntityBase> AllFloors = new List<EntityBase>();
    public List<EntityBase> AllWalls = new List<EntityBase>();
    public List<EntityBase> AllCounters = new List<EntityBase>();

    [Header("Selected Entities")]
    public EntityBase[] SelectedEntities;
    EntityBase[] oldSelection = new EntityBase[0];
    GhostEntity ghostEntity;

    private void Start()
    {
        SetObjects();
        SetConstructionButtons();
        SetTileButtons();
    }

    private void Update()
    {
        // Tile Construction
        HandleOnDrag();
        CancelBuilding();
        HandleSelectedEntities();
        HandleDestroySwitch();

        // UI Management
        ManageSubmenus();
        CheckToggles();
    }

    private void HandleDestroySwitch() 
    {
        if (curTile != CurrentTileState.None)
        {
            isDestroyOn = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SelectedEntities = new EntityBase[0];
            curTile = CurrentTileState.None;
            isDestroyOn = !isDestroyOn;
        }

        if (isDestroyOn)
        {
            DestroyIcon.SetActive(true);
        }
        else 
        { 
            DestroyIcon.SetActive(false);
        }
    }
    private void HandleOnDrag()
    {
        // Building
        if (!isOverUI && !isDestroyOn) 
        {
            // Single-Tile Building
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                // If there arent multiple entities selected
                if (SelectedEntities.Length == 0)
                {
                    // Check if curTile is not none
                    if (curTile != CurrentTileState.None)
                    {
                        // Get mouse position
                        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                        {
                            // Gets first corner
                            Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));

                            // Build tile
                            SelectedEntities = new EntityBase[0];
                            BuildTileAt(gridPoint);
                        }
                    }
                }
            }
            // Gets min corner
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (curTile != CurrentTileState.None) 
                { 
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                    {
                        // Gets first corner
                        Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                        firstCorner = gridPoint;
                        curSelectedTile = Grid.GetLastEntity<EntityBase>(gridPoint);
                    }
                }
            }
            // Gets max corner, finds SelectedEntities
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                {
                    // Get Grid position relative to UI
                    Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                    lastCorner = gridPoint;
                    oldSelection = new EntityBase[0];
                    oldSelection = SelectedEntities;

                    // If both corners are different
                    if (firstCorner != lastCorner)
                    {
                        SelectedEntities = Grid.FindEntities(firstCorner, lastCorner);

                        // Once all entities are added
                        HandleSelectedEntities();
                    }

                    // Clear material
                    if (SelectedEntities != oldSelection)
                    {
                        for (int j = 0; j < oldSelection.Length; j++)
                        {
                            // This selected entity is not in the old selection
                            oldSelection[j].GetComponent<Image>().material = null;
                        }
                    }
                }
            }
            // Multi-Tile Building
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (!isOverUI)
                {
                    for (int i = 0; i < SelectedEntities.Length; i++)
                    {
                        if (SelectedEntities[i].Name != "GhostEntity")
                        {
                            SelectedEntities[i].GetComponent<Image>().material = null;
                            BuildTileAt(SelectedEntities[i].Position);
                        }
                    }

                    for (int i = 0; i < oldSelection.Length; i++)
                    {
                        if (oldSelection[i] != null)
                        {
                            if (oldSelection[i].Name != "GhostEntity")
                            {
                                oldSelection[i].GetComponent<Image>().material = null;
                            }
                        }
                    }
                }

                SelectedEntities = new EntityBase[0];
            }

            // Cancel Building
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                curTile = CurrentTileState.None;
                for (int i = 0; i < SelectedEntities.Length; i++)
                {
                    SelectedEntities[i].GetComponent<Image>().material = null;
                }
                for (int i = 0; i < oldSelection.Length; i++)
                {
                    oldSelection[i].GetComponent<Image>().material = null;
                }
                SelectedEntities = new EntityBase[0];
                oldSelection = new EntityBase[0];
                curSelectedTile = null;
            }
        }

        // Deleting
        if (isDestroyOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (root != null)
                {
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                    {
                        // Get Grid position relative to UI
                        Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                        firstCorner = gridPoint;

                        EntityBase[] selectedEntities = Grid.GetEntities<EntityBase>(gridPoint);

                        for (int i = 0; i < selectedEntities.Length; i++)
                        {
                            Debug.Log("Deleting: " + selectedEntities[i].Name);
                            if (selectedEntities[i] is EntityGrass || selectedEntities[i] is EntityConcrete ||
                                selectedEntities[i] is EntityCustomer || selectedEntities[i] is EntityBarista ||
                                selectedEntities[i] is EntitySupport || selectedEntities[i] is EntityFront ||
                                selectedEntities[i] is GhostEntity || selectedEntities[i] is EntityCoffee)
                            { 
                            }
                            else 
                            {
                                Grid.Destroy(selectedEntities[i]);
                                Debug.Log("Destroyed entity.");
                            }
                        }


                        SelectedEntities = new EntityBase[0];
                    }
                }
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                {
                    // Get Grid position relative to UI
                    Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                    lastCorner = gridPoint;

                    // If both corners are different
                    if (firstCorner != lastCorner)
                    {
                        SelectedEntities = Grid.FindEntities(firstCorner, lastCorner);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                for (int i = 0; i < SelectedEntities.Length; i++)
                {

                    SelectedEntities[i].GetComponent<Image>().material = null;

                    // If not grass, concrete, or character
                    if (SelectedEntities[i].Priority == EntityPriority.Terrain ||
                        SelectedEntities[i].Priority == EntityPriority.Characters ||
                        SelectedEntities[i] is GhostEntity || SelectedEntities[i] is EntityCoffee
                        )
                    { 
                    }
                    else 
                    { 
                        Grid.Create<EntityGrass>(SelectedEntities[i].Position);
                        CalculateRefund(SelectedEntities[i]);
                        Grid.Destroy(SelectedEntities[i]);
                        Debug.Log("Deleted entity.");
                    }

                }

                for (int i = 0; i < oldSelection.Length; i++)
                {
                    if (oldSelection[i] != null)
                    {
                        oldSelection[i].GetComponent<Image>().material = null;
                    }
                }

                SelectedEntities = new EntityBase[0];
            }
        }
    }
    private void HandleSelectedEntities() 
    {
        if (SelectedEntities.Length > 1)
        {
            if (curTile != CurrentTileState.None)
            {
                for (int i = 0; i < SelectedEntities.Length; i++)
                {
                    SelectedEntities[i].GetComponent<Image>().material = Resources.Load<Material>("Sprites/UI/Indicators/SelectedEntity");
                }
            }
            if (isDestroyOn)
            {
                for (int i = 0; i < SelectedEntities.Length; i++)
                {
                    if (SelectedEntities[i] != null)
                    {
                        SelectedEntities[i].GetComponent<Image>().material = Resources.Load<Material>("Sprites/UI/Indicators/SelectedEntity");
                    }
                }
            }
        }
    }

    private void CalculateRefund(EntityBase curEntity) 
    {
        switch (curEntity.Name)
        {
            case "Brewer":
                GetEconomy.CurrentExpenses -= BrewerCost * RefundRate;
                break;
            case "Espresso-Machine":
                GetEconomy.CurrentExpenses -= EspressoCost * RefundRate;
                break;
            case "RoasterLvl1":
                GetEconomy.CurrentExpenses -= RoasterCost * RefundRate;
                break;
            case "Register":
                GetEconomy.CurrentExpenses -= RegisterCost * RefundRate;
                break;
        }
        switch (curEntity.Priority)
        {
            case EntityPriority.Unknown:
                break;
            case EntityPriority.Terrain:
                break;
            case EntityPriority.Foundations:
                GetEconomy.CurrentExpenses -= FloorCost * RefundRate;
                break;
            case EntityPriority.Buildings:
                GetEconomy.CurrentExpenses -= WallCost * RefundRate;
                break;
            case EntityPriority.Furniture:
                GetEconomy.CurrentExpenses -= FurnitureCost * RefundRate;
                break;
            case EntityPriority.Characters:
                // do nothing
                break;
            case EntityPriority.Appliances:
                // do nothing
                break;
            case EntityPriority.Floating:
                // do nothing
                break;
        }
    }

    private void BuildTileAt(Vector2Int position) 
    {
        // Replace any entity that isn't concrete, grass, or character
        if (curTile != CurrentTileState.None)
        {
            EntityBase curEntity = Grid.GetLastEntity<EntityBase>(position);
            if (curEntity.Priority != EntityPriority.Terrain ||
                curEntity.Priority != EntityPriority.Characters ||
                curEntity.Priority != EntityPriority.Foundations)
            {
            }
            else 
            { 
                Grid.Destroy(curEntity);
            }
        }

        // If more than 1 is selected
        if (SelectedEntities.Length > 1)
        {
            // Spawn new entities
            switch (curTile)
            {
                case CurrentTileState.S_Floor1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloor)
                    {
                        return;
                    }
                    else
                    { 
                        EntityFloor S_Floor1 = Grid.Create<EntityFloor>(position);
                        AllFloors.Add(S_Floor1);

                        GetEconomy.CurrentExpenses += FloorCost;
                        return;
                    
                    }
                case CurrentTileState.S_Floor2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorTwo)
                    {
                        return;
                    }
                    else
                    {
                        EntityFloorTwo S_Floor2 = Grid.Create<EntityFloorTwo>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor2);

                        return;

                    }
                case CurrentTileState.S_Floor3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorThree)
                    {
                        return;
                    }
                    else
                    {

                    EntityFloorThree S_Floor3 = Grid.Create<EntityFloorThree>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor3);

                        return;
                    }
                case CurrentTileState.S_Floor4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorFour)
                    {
                        return;
                    }
                    else
                    {

                    EntityFloorFour S_Floor4 = Grid.Create<EntityFloorFour>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor4);

                        return;
                    }
                case CurrentTileState.S_Floor5:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorFive)
                    {
                        return;
                    }
                    else
                    {

                    EntityFloorFive S_Floor5 = Grid.Create<EntityFloorFive>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor5);

                        return;
                    }
                case CurrentTileState.S_Floor7:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorSeven)
                    {
                        return;
                    }
                    else
                    {

                    EntityFloorSeven S_Floor7 = Grid.Create<EntityFloorSeven>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor7);
                        return;
                    }
                    case CurrentTileState.S_Wall1:
                        if (Grid.GetLastEntity<EntityBase>(position) is EntityWallBrick)
                        {
                            return;
                        }
                        else
                        {

                        EntityWallBrick S_Wall1 = Grid.Create<EntityWallBrick>(position);
                            GetEconomy.CurrentExpenses += WallCost;
                            AllWalls.Add(S_Wall1);

                            return;
                        }
                case CurrentTileState.S_Wall2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallGreyBrick)
                    {
                        return;
                    }
                    else
                    {

                    EntityWallGreyBrick S_Wall2 = Grid.Create<EntityWallGreyBrick>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall2);

                        return;
                    }
                case CurrentTileState.S_Wall3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallPale)
                    {
                        return;
                    }
                    else
                    {

                    EntityWallPale S_Wall3 = Grid.Create<EntityWallPale>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall3);

                        return;
                    }
                case CurrentTileState.S_Wall4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallPlaster)
                    {
                        return;
                    }
                    else
                    {

                    EntityWallPlaster S_Wall4 = Grid.Create<EntityWallPlaster>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall4);
                        return;
                    }
                case CurrentTileState.S_Counter1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterGrey)
                    {
                        return;
                    }
                    else
                    {

                    EntityCounterGrey S_Counter1 = Grid.Create<EntityCounterGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllCounters.Add(S_Counter1);

                        return;
                    }
                case CurrentTileState.S_Counter2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterMarble)
                    {
                        return;
                    }
                    else
                    {

                    EntityCounterMarble S_Counter2 = Grid.Create<EntityCounterMarble>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllCounters.Add(S_Counter2);

                        return;
                    }
                case CurrentTileState.S_Counter3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterRed)
                    {
                        return;
                    }
                    else
                    {
                        EntityCounterRed S_Counter3 = Grid.Create<EntityCounterRed>(position);
                        AllCounters.Add(S_Counter3);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        return;

                    }
                default:
                    return;
            }
        }

        // If none are selected
        if (SelectedEntities.Length == 0)
        {
            switch (curTile)
            {
                case CurrentTileState.S_Floor1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloor)
                    {
                        break;
                    }
                    else
                    {
                        EntityFloor S_Floor1 = Grid.Create<EntityFloor>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor1);

                        break;

                    }
                case CurrentTileState.S_Floor2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorTwo)
                    {
                        break;
                    }
                    else
                    {
                        EntityFloorTwo S_Floor2 = Grid.Create<EntityFloorTwo>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor2);

                        break;

                    }
                case CurrentTileState.S_Floor3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorThree)
                    {
                        break;
                    }
                    else
                    {

                        EntityFloorThree S_Floor3 = Grid.Create<EntityFloorThree>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor3);

                        break;
                    }
                case CurrentTileState.S_Floor4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorFour)
                    {
                        break;
                    }
                    else
                    {

                        EntityFloorFour S_Floor4 = Grid.Create<EntityFloorFour>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor4);

                        break;
                    }
                case CurrentTileState.S_Floor5:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorFive)
                    {
                        break;
                    }
                    else
                    {

                        EntityFloorFive S_Floor5 = Grid.Create<EntityFloorFive>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor5);

                        break;
                    }
                case CurrentTileState.S_Floor7:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityFloorSeven)
                    {
                        break;
                    }
                    else
                    {

                        EntityFloorSeven S_Floor7 = Grid.Create<EntityFloorSeven>(position);
                        GetEconomy.CurrentExpenses += FloorCost;
                        AllFloors.Add(S_Floor7);

                        break;
                    }
                case CurrentTileState.S_Wall1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallBrick)
                    {
                        break;
                    }
                    else
                    {

                        EntityWallBrick S_Wall1 = Grid.Create<EntityWallBrick>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall1);

                        break;
                    }
                case CurrentTileState.S_Wall2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallGreyBrick)
                    {
                        break;
                    }
                    else
                    {

                        EntityWallGreyBrick S_Wall2 = Grid.Create<EntityWallGreyBrick>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall2);

                        break;
                    }
                case CurrentTileState.S_Wall3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallPale)
                    {
                        break;
                    }
                    else
                    {

                        EntityWallPale S_Wall3 = Grid.Create<EntityWallPale>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall3);

                        break;
                    }
                case CurrentTileState.S_Wall4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityWallPlaster)
                    {
                        break;
                    }
                    else
                    {

                        EntityWallPlaster S_Wall4 = Grid.Create<EntityWallPlaster>(position);
                        GetEconomy.CurrentExpenses += WallCost;
                        AllWalls.Add(S_Wall4);
                        break;
                    }
                case CurrentTileState.S_Counter1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterGrey)
                    {
                        break;
                    }
                    else
                    {

                        EntityCounterGrey S_Counter1 = Grid.Create<EntityCounterGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;
                    }
                case CurrentTileState.S_Counter2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterMarble)
                    {
                        break;
                    }
                    else
                    {

                        EntityCounterMarble S_Counter2 = Grid.Create<EntityCounterMarble>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;
                    }
                case CurrentTileState.S_Counter3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityCounterRed)
                    {
                        break;
                    }
                    else
                    {
                        EntityCounterRed S_Counter3 = Grid.Create<EntityCounterRed>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;

                    }
                case CurrentTileState.Roaster:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityRoasteryMachineOne)
                    {
                        break;
                    }
                    else
                    {
                        EntityRoasteryMachineOne roaster = Grid.Create<EntityRoasteryMachineOne>(position);
                        GetEconomy.CurrentExpenses += RoasterCost;

                        break;
                    }
                case CurrentTileState.Brewer:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityBrewingMachineOne)
                    {
                        break;
                    }
                    else
                    {
                        EntityBrewingMachineOne br = Grid.Create<EntityBrewingMachineOne>(position);
                        GetEconomy.CurrentExpenses += BrewerCost;

                        break;
                    }
                case CurrentTileState.Register:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityRegister)
                    {
                        break;
                    }
                    else
                    {

                        EntityRegister reg = Grid.Create<EntityRegister>(position);
                        GetEconomy.CurrentExpenses += RegisterCost;

                        break;
                    }
                case CurrentTileState.Espresso:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityEspressoMachineOne)
                    {
                        break;
                    }
                    else
                    {
                        EntityEspressoMachineOne Espresso = Grid.Create<EntityEspressoMachineOne>(position);
                        GetEconomy.CurrentExpenses += EspressoCost;

                        break;
                    }
                case CurrentTileState.S_Table1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableSmooth)
                    {
                        break;
                    }
                    else
                    {
                        EntityTableSmooth S_Table1 = Grid.Create<EntityTableSmooth>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;
                    }
                case CurrentTileState.S_Table2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableRough)
                    {
                        break;
                    }
                    else
                    {
                        EntityTableRough S_Table2 = Grid.Create<EntityTableRough>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;
                    }
                case CurrentTileState.S_Table3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableGrey)
                    {
                        break;
                    }
                    else
                    {
                        EntityTableGrey S_Table3 = Grid.Create<EntityTableGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;
                    }
                case CurrentTileState.S_Table4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableSquareGrey)
                    {
                        break;
                    }
                    else
                    {
                        EntityTableSquareGrey S_Table4 = Grid.Create<EntityTableSquareGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;

                        break;

                    }
                case CurrentTileState.S_Chair1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairSmooth)
                    {
                        break;
                    }
                    else
                    {
                        EntityChairSmooth S_Chair1 = Grid.Create<EntityChairSmooth>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllChairs.Add(S_Chair1);
                        break;

                    }
                case CurrentTileState.S_Chair2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairRough)
                    {
                        break;
                    }
                    else
                    {
                        EntityChairRough S_Chair2 = Grid.Create<EntityChairRough>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllChairs.Add(S_Chair2);

                        break;

                    }
                case CurrentTileState.S_Chair3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairSquareGrey)
                    {
                        break;
                    }
                    else
                    {
                        EntityChairSquareGrey S_Chair3 = Grid.Create<EntityChairSquareGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllChairs.Add(S_Chair3);

                        break;

                    }
                case CurrentTileState.S_Chair4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairRoundGrey)
                    {
                        break;
                    }
                    else
                    {
                        EntityChairRoundGrey S_Chair4 = Grid.Create<EntityChairRoundGrey>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllChairs.Add(S_Chair4);

                        break;

                    }
                case CurrentTileState.S_Barstool:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityBarstool)
                    {
                        break;
                    }
                    else
                    {
                        EntityBarstool S_Barstool = Grid.Create<EntityBarstool>(position);
                        GetEconomy.CurrentExpenses += FurnitureCost;
                        AllChairs.Add(S_Barstool);

                        break;

                    }
                default:
                    break;
            }
        }
    }
    private void CancelBuilding() 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            curTile = CurrentTileState.None;
            SelectedEntities = new EntityBase[0];
            curSelectedTile = null;
            ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent") as Sprite);
        }
    }
    private void ControlConstructionUI()
    {
        if (isConstructionOpen == false)
        {
            ConstructionPanel.SetActive(false);
            isConstructionOpen = true;
        }
        else if (isConstructionOpen == true)
        {
            ConstructionPanel.SetActive(true);
            isConstructionOpen = false;
        }
    }
    private void ManageSubmenus() 
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
    }
    private void CheckToggles()
    {
        if (TileTypeSelector.value == 0)
        {
            ClickedCon_Building();
        }
        else if (TileTypeSelector.value == 1)
        {
            ClickedCon_Furniture();
        }
        else if (TileTypeSelector.value == 2)
        {
            ClickedCon_Machines();
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
    private void SetObjects() 
    {
        Grid = FindObjectOfType<EntityGrid>();
        curEvent = EventSystem.current;
        GetEconomy = FindObjectOfType<CafeEconomySystem>();
        ghostEntity = FindObjectOfType<GhostEntity>();
    }
    private void SetConstructionButtons() 
    {
        B_ConstructionIcon.onClick.AddListener(ControlConstructionUI);
    }
    private void SetTileButtons() 
    {
        B_Floor1.onClick.AddListener(FloorOne);
        B_Floor2.onClick.AddListener(FloorTwo);
        B_Floor3.onClick.AddListener(FloorThree);
        B_Floor4.onClick.AddListener(FloorFour);

        B_Wall1.onClick.AddListener(WallOne);
        B_Wall2.onClick.AddListener(WallTwo);
        B_Wall3.onClick.AddListener(WallThree);
        B_Wall4.onClick.AddListener(WallFour);

        B_Table1.onClick.AddListener(TableOne);
        B_Table2.onClick.AddListener(TableTwo);
        B_Table3.onClick.AddListener(TableThree);
        B_Table4.onClick.AddListener(TableFour);

        B_Chair1.onClick.AddListener(ChairOne);
        B_Chair2.onClick.AddListener(ChairTwo);
        B_Chair3.onClick.AddListener(ChairThree);
        B_Chair4.onClick.AddListener(ChairFour);
        
        B_Counter1.onClick.AddListener(CounterOne);
        B_Counter2.onClick.AddListener(CounterTwo);
        B_Counter3.onClick.AddListener(CounterThree);

        B_Barstool.onClick.AddListener(ClickedBarstool);
        B_Espresso.onClick.AddListener(ClickedEspresso);
        B_Register.onClick.AddListener(ClickedRegister);
        B_Roaster.onClick.AddListener(ClickedRoaster);
        B_Brewer.onClick.AddListener(ClickedBrewer);
    }
    private void FloorOne() 
    {
        SelectedEntities = new EntityBase[0];
        curTile = CurrentTileState.S_Floor1;
    }
    private void FloorTwo() 
    {
        SelectedEntities = new EntityBase[0];
        curTile = CurrentTileState.S_Floor2;

    }
    private void FloorThree()
    {
        SelectedEntities = new EntityBase[0];
        curTile = CurrentTileState.S_Floor3;

    }
    private void FloorFour()
    {
SelectedEntities= new EntityBase[0];
        curTile = CurrentTileState.S_Floor4;

    }
    private void WallOne() 
    {
SelectedEntities= new EntityBase[0];
        curTile = CurrentTileState.S_Wall1;

    }
    private void WallTwo()
    {
SelectedEntities= new EntityBase[0];
        curTile = CurrentTileState.S_Wall2;

    }
    private void WallThree()
    {
SelectedEntities= new EntityBase[0];
        curTile = CurrentTileState.S_Wall3;

    }
    private void WallFour()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Wall4;
    }
    private void TableOne() 
    {
        curTile = CurrentTileState.S_Table1;
    }
    private void TableTwo()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Table2;
    }
    private void TableThree()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Table3;
    }
    private void TableFour()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Table4;
    }
    private void ChairOne() 
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Chair1;
    }
    private void ChairTwo()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Chair2;
    }
    private void ChairThree()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Chair3;
    }
    private void ChairFour()
    {
        SelectedEntities = new EntityBase[0];

        curTile = CurrentTileState.S_Chair4;
    }
    private void ClickedBarstool() 
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Barstool;
    }
    private void CounterOne() 
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Counter1;
    }
    private void CounterTwo()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Counter2;
    }
    private void CounterThree()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.S_Counter3;
    }
    private void ClickedEspresso() 
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.Espresso;
    }
    private void ClickedRoaster()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.Roaster;
    }
    private void ClickedBrewer()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.Brewer;
    }
    private void ClickedRegister()
    {
SelectedEntities= new EntityBase[0];

        curTile = CurrentTileState.Register;
    }
}