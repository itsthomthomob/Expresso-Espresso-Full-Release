using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileConstruction : MonoBehaviour
{
    [Header("Grid")]
    EntityGrid Grid;
    public RectTransform root;
    public Vector2Int firstCorner;
    public Vector2Int lastCorner;

    [Header("Handle Construction Panel")]
    public bool isConstructionOpen = false;
    public bool isDestroyOn = false;
    public bool isOverUI;
    public Button B_ConstructionIcon;
    public GameObject ConstructionPanel;
    public GameObject DestroyIcon;

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
    public EntityBase curSelectedTile;
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
    public List<EntityBase> SelectedEntities = new List<EntityBase>();

    private void Start()
    {
        SetObjects();
        SetConstructionButtons();
        SetTileButtons();
    }

    private void Update()
    {
        HandleOnDrag();
        CancelBuilding();
        HandleSelectedEntities();
        HandleDestroy();
    }

    private void HandleDestroy() 
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SelectedEntities.Clear();
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
        if (!isOverUI && !isDestroyOn) 
        { 
            if (curTile != CurrentTileState.None)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    // TO-DO
                    // - Return selected entity
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                    {
                        Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                        // Get Grid position relative to UI
                        curSelectedTile = Grid.GetLastEntity<EntityBase>(gridPoint);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (root != null) 
                { 
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                    {
                        // Get Grid position relative to UI
                        Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                        firstCorner = gridPoint;
                        // Add first selected entity to selected entities
                        if (!SelectedEntities.Contains(Grid.GetLastEntity<EntityBase>(gridPoint)))
                        {
                            if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority != EntityPriority.Characters)
                            {
                                SelectedEntities.Add(Grid.GetLastEntity<EntityBase>(gridPoint));
                            }
                        }
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
                        for (int i = 0; i < Grid.FindEntities(firstCorner, lastCorner).Length; i++)
                        {
                            if (!SelectedEntities.Contains(Grid.FindEntities(firstCorner, lastCorner)[i]))
                            {
                                SelectedEntities.Add(Grid.FindEntities(firstCorner, lastCorner)[i]);
                            }
                        }

                        // Once all entities are added
                        HandleSelectedEntities();
                    }
                }
            }
        }
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
                        // Add first selected entity to selected entities
                        if (!SelectedEntities.Contains(Grid.GetLastEntity<EntityBase>(gridPoint)))
                        {
                            if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority != EntityPriority.Characters)
                            {
                                SelectedEntities.Add(Grid.GetLastEntity<EntityBase>(gridPoint));
                            }
                        }
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
                        for (int i = 0; i < Grid.FindEntities(firstCorner, lastCorner).Length; i++)
                        {
                            if (!SelectedEntities.Contains(Grid.FindEntities(firstCorner, lastCorner)[i]))
                            {
                                SelectedEntities.Add(Grid.FindEntities(firstCorner, lastCorner)[i]);
                            }
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                for (int i = 0; i < SelectedEntities.Count; i++)
                {
                    if (SelectedEntities[i].Name == "Grass" ||
                        SelectedEntities[i].Name == "Customer" ||
                        SelectedEntities[i].Name == "Concrete"
                        )
                    {

                    }
                    else 
                    {
                        Grid.Destroy(SelectedEntities[i]);
                    }
                }
            }
        }
    }

    private void HandleSelectedEntities() 
    {
        if (curTile != CurrentTileState.None)
        {
            if (firstCorner != Vector2Int.zero && lastCorner != Vector2Int.zero)
            {
                for (int i = 0; i < SelectedEntities.Count; i++)
                {
                    ChangeSelectedEntities(SelectedEntities[i].Position);
                }
            }
        }
    }

    private void ChangeSelectedEntities(Vector2Int position) 
    {
        if (SelectedEntities.Count == 1)
        {
            switch (curTile)
            {
                case CurrentTileState.Roaster:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityRoasteryMachineOne)
                    {
                        return;
                    }
                    else 
                    { 
                        EntityRoasteryMachineOne roaster = Grid.Create<EntityRoasteryMachineOne>(position);
                        return;
                    }
                case CurrentTileState.Brewer:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityBrewingMachineOne)
                    {
                        return;
                    }
                    else
                    { 
                        EntityBrewingMachineOne br = Grid.Create<EntityBrewingMachineOne>(position);
                        return;
                    }
                case CurrentTileState.Register:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityRegister)
                    {
                        return;
                    }
                    else
                    {

                        EntityRegister reg = Grid.Create<EntityRegister>(position);
                        return;
                    }
                case CurrentTileState.Espresso:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityEspressoMachineOne)
                    {
                        return;
                    }
                    else
                    {
                        EntityEspressoMachineOne Espresso = Grid.Create<EntityEspressoMachineOne>(position);
                        return;
                    }
                case CurrentTileState.S_Table1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableSmooth)
                    {
                        return;
                    }
                    else
                    {
                        EntityTableSmooth S_Table1 = Grid.Create<EntityTableSmooth>(position);
                        return;
                    }
                case CurrentTileState.S_Table2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableRough)
                    {
                        return;
                    }
                    else
                    {
                        EntityTableRough S_Table2 = Grid.Create<EntityTableRough>(position);
                        return;
                    }
                case CurrentTileState.S_Table3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableGrey)
                    {
                        return;
                    }
                    else
                    {
                        EntityTableGrey S_Table3 = Grid.Create<EntityTableGrey>(position);
                        return;
                    }
                case CurrentTileState.S_Table4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityTableSquareGrey)
                    {
                        return;
                    }
                    else
                    {
                        EntityTableSquareGrey S_Table4 = Grid.Create<EntityTableSquareGrey>(position);
                        return;

                    }
                case CurrentTileState.S_Chair1:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairSmooth)
                    {
                        return;
                    }
                    else
                    {
                        EntityChairSmooth S_Chair1 = Grid.Create<EntityChairSmooth>(position);
                        return;

                    }
                case CurrentTileState.S_Chair2:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairRough)
                    {
                        return;
                    }
                    else
                    {
                        EntityChairRough S_Chair2 = Grid.Create<EntityChairRough>(position);
                        return;

                    }
                case CurrentTileState.S_Chair3:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairGrey)
                    {
                        return;
                    }
                    else
                    {
                        EntityChairGrey S_Chair3 = Grid.Create<EntityChairGrey>(position);
                        return;

                    }
                case CurrentTileState.S_Chair4:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityChairRed)
                    {
                        return;
                    }
                    else
                    {
                        EntityChairRed S_Chair4 = Grid.Create<EntityChairRed>(position);
                        return;

                    }
                case CurrentTileState.S_Barstool:
                    if (Grid.GetLastEntity<EntityBase>(position) is EntityBarstool)
                    {
                        return;
                    }
                    else
                    {
                        EntityBarstool S_Barstool = Grid.Create<EntityBarstool>(position);
                        return;

                    }
                default:
                    return;
            }
        }
        else if (SelectedEntities.Count > 1)
        {
            // Entities are selected
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
                        return;

                    }
                default:
                    return;
            }
        }
    }

    private void CancelBuilding() 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            curTile = CurrentTileState.None;
            curSelectedTile = null;
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

    private void SetObjects() 
    {
        Grid = FindObjectOfType<EntityGrid>();
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
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Floor1;
    }
    private void FloorTwo() 
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Floor2;

    }
    private void FloorThree()
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Floor3;

    }
    private void FloorFour()
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Floor4;

    }
    private void WallOne() 
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Wall1;

    }
    private void WallTwo()
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Wall2;

    }
    private void WallThree()
    {
        SelectedEntities.Clear();
        curTile = CurrentTileState.S_Wall3;

    }
    private void WallFour()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Wall4;
    }
    private void TableOne() 
    {
        curTile = CurrentTileState.S_Table1;
    }
    private void TableTwo()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Table2;
    }
    private void TableThree()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Table3;
    }
    private void TableFour()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Table4;
    }
    private void ChairOne() 
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Chair1;
    }
    private void ChairTwo()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Chair2;
    }
    private void ChairThree()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Chair3;
    }
    private void ClickedBarstool() 
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Barstool;
    }
    private void CounterOne() 
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Counter1;
    }
    private void CounterTwo()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Counter2;
    }
    private void CounterThree()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.S_Counter3;
    }
    private void ClickedEspresso() 
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.Espresso;
    }
    private void ClickedRoaster()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.Roaster;
    }
    private void ClickedBrewer()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.Brewer;
    }
    private void ClickedRegister()
    {
        SelectedEntities.Clear();

        curTile = CurrentTileState.Register;
    }
}