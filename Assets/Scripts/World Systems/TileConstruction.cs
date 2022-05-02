using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileConstruction : MonoBehaviour
{
    [Header("Grid")]
    EntityGrid Grid;
    RectTransform root;

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

    private void Start()
    {
        SetObjects();
        SetConstructionButtons();
        SetTileButtons();
    }

    private void Update()
    {
        OnMouseClick();
    }

    private void OnMouseClick()
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
        {
            Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
            Debug.Log(Grid.GetLastEntity<EntityBase>(gridPoint).Name);
            //
        }
    }

    private void SetObjects() 
    {
        Grid = GetComponent<EntityGrid>();
    }
    private void SetConstructionButtons() 
    {
        B_ConstructionIcon.onClick.AddListener(ControlConstructionUI);
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