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
    public bool isOverUI;
    public Button B_ConstructionIcon;
    public GameObject ConstructionPanel;

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
    }

    private void HandleOnDrag()
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
        else if (Input.GetKeyDown(KeyCode.Mouse0))
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

    private void HandleSelectedEntities() 
    {
        if (curTile != CurrentTileState.None)
        {
            for (int i = 0; i < SelectedEntities.Count; i++)
            {
                SelectedEntities[i] = ChangeSelectedEntities(SelectedEntities[i].Position);
            }
        }
    }

    private EntityBase ChangeSelectedEntities(Vector2Int position) 
    {
        if (SelectedEntities.Count == 1)
        {
            switch (curTile)
            {
                case CurrentTileState.Roaster:
                    EntityRoasteryMachineOne roaster = Grid.Create<EntityRoasteryMachineOne>(position);
                    return roaster;
                case CurrentTileState.Brewer:
                    EntityBrewingMachineOne br = Grid.Create<EntityBrewingMachineOne>(position);
                    return br;
                case CurrentTileState.Register:
                    EntityRegister reg = Grid.Create<EntityRegister>(position);
                    return reg;
                case CurrentTileState.Espresso:
                    EntityEspressoMachineOne Espresso = Grid.Create<EntityEspressoMachineOne>(position);
                    return Espresso;
                case CurrentTileState.S_Table1:
                    EntityTableSmooth S_Table1 = Grid.Create<EntityTableSmooth>(position);
                    return S_Table1;
                case CurrentTileState.S_Table2:
                    EntityTableRough S_Table2 = Grid.Create<EntityTableRough>(position);
                    return S_Table2;
                case CurrentTileState.S_Table3:
                    EntityTableGrey S_Table3 = Grid.Create<EntityTableGrey>(position);
                    return S_Table3;
                case CurrentTileState.S_Table4:
                    EntityTableSquareGrey S_Table4 = Grid.Create<EntityTableSquareGrey>(position);
                    return S_Table4;
                case CurrentTileState.S_Chair1:
                    EntityChairSmooth S_Chair1 = Grid.Create<EntityChairSmooth>(position);
                    return S_Chair1;
                case CurrentTileState.S_Chair2:
                    EntityChairRough S_Chair2 = Grid.Create<EntityChairRough>(position);
                    return S_Chair2;
                case CurrentTileState.S_Chair3:
                    EntityChairGrey S_Chair3 = Grid.Create<EntityChairGrey>(position);
                    return S_Chair3;
                case CurrentTileState.S_Chair4:
                    EntityChairRed S_Chair4 = Grid.Create<EntityChairRed>(position);
                    return S_Chair4;
                case CurrentTileState.S_Barstool:
                    EntityBarstool S_Barstool = Grid.Create<EntityBarstool>(position);
                    return S_Barstool;
                default:
                    return null;
            }
        }
        else if (SelectedEntities.Count > 1)
        {
            // Entities are selected
            switch (curTile)
            {
                case CurrentTileState.S_Floor1:
                    EntityFloor S_Floor1 = Grid.Create<EntityFloor>(position);
                    return S_Floor1;
                case CurrentTileState.S_Floor2:
                    EntityFloorTwo S_Floor2 = Grid.Create<EntityFloorTwo>(position);
                    return S_Floor2;
                case CurrentTileState.S_Floor3:
                    EntityFloorThree S_Floor3 = Grid.Create<EntityFloorThree>(position);
                    return S_Floor3;
                case CurrentTileState.S_Floor4:
                    EntityFloorFour S_Floor4 = Grid.Create<EntityFloorFour>(position);
                    return S_Floor4;
                case CurrentTileState.S_Floor5:
                    EntityFloorFive S_Floor5 = Grid.Create<EntityFloorFive>(position);
                    return S_Floor5;
                case CurrentTileState.S_Floor7:
                    EntityFloorSeven S_Floor7 = Grid.Create<EntityFloorSeven>(position);
                    return S_Floor7;
                case CurrentTileState.S_Wall1:
                    EntityWallBrick S_Wall1 = Grid.Create<EntityWallBrick>(position);
                    return S_Wall1;
                case CurrentTileState.S_Wall2:
                    EntityWallGreyBrick S_Wall2 = Grid.Create<EntityWallGreyBrick>(position);
                    return S_Wall2;
                case CurrentTileState.S_Wall3:
                    EntityWallPale S_Wall3 = Grid.Create<EntityWallPale>(position);
                    return S_Wall3;
                case CurrentTileState.S_Wall4:
                    EntityWallPlaster S_Wall4 = Grid.Create<EntityWallPlaster>(position);
                    return S_Wall4;
                case CurrentTileState.S_Counter1:
                    EntityCounterGrey S_Counter1 = Grid.Create<EntityCounterGrey>(position);
                    return S_Counter1;
                case CurrentTileState.S_Counter2:
                    EntityCounterMarble S_Counter2 = Grid.Create<EntityCounterMarble>(position);
                    return S_Counter2;
                case CurrentTileState.S_Counter3:
                    EntityCounterRed S_Counter3 = Grid.Create<EntityCounterRed>(position);
                    return S_Counter3;
                default:
                    return null;
            }
        }
        else if (SelectedEntities.Count < 1)
        {
            return null;
        }
        else 
        {
            return null;
        }
    }

    private void CancelBuilding() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        curTile = CurrentTileState.S_Floor1;
    }
    private void FloorTwo() 
    {
        curTile = CurrentTileState.S_Floor2;
    }
    private void FloorThree()
    {
        curTile = CurrentTileState.S_Floor3;
    }
    private void FloorFour()
    {
        curTile = CurrentTileState.S_Floor4;
    }
    private void WallOne() 
    {
        curTile = CurrentTileState.S_Wall1;
    }
    private void WallTwo()
    {
        curTile = CurrentTileState.S_Wall2;
    }
    private void WallThree()
    {
        curTile = CurrentTileState.S_Wall3;
    }
    private void WallFour()
    {
        curTile = CurrentTileState.S_Wall4;
    }
    private void TableOne() 
    {
        curTile = CurrentTileState.S_Table1;
    }
    private void TableTwo()
    {
        curTile = CurrentTileState.S_Table2;
    }
    private void TableThree()
    {
        curTile = CurrentTileState.S_Table3;
    }
    private void TableFour()
    {
        curTile = CurrentTileState.S_Table4;
    }
    private void ChairOne() 
    {
        curTile = CurrentTileState.S_Chair1;
    }
    private void ChairTwo()
    {
        curTile = CurrentTileState.S_Chair2;
    }
    private void ChairThree()
    {
        curTile = CurrentTileState.S_Chair3;
    }
    private void ClickedBarstool() 
    {
        curTile = CurrentTileState.S_Barstool;
    }
    private void CounterOne() 
    {
        curTile = CurrentTileState.S_Counter1;
    }
    private void CounterTwo()
    {
        curTile = CurrentTileState.S_Counter2;
    }
    private void CounterThree()
    {
        curTile = CurrentTileState.S_Counter3;
    }
    private void ClickedEspresso() 
    {
        curTile = CurrentTileState.Espresso;
    }
    private void ClickedRoaster()
    {
        curTile = CurrentTileState.Roaster;
    }
    private void ClickedBrewer()
    {
        curTile = CurrentTileState.Brewer;
    }
    private void ClickedRegister()
    {
        curTile = CurrentTileState.Register;
    }
}