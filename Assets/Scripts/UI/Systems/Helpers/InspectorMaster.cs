using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectorMaster : MonoBehaviour
{
    [Header("UI Attributes")]
    public GameObject InspectorUI;
    public Canvas parentCanvas;
    public TMP_Text MachineName;
    public TMP_Text MachineLevel;
    public Button InspectMachine_B;
    public Button SellMachine_B;
    public Canvas canvas;
    public float XAdjust;
    public float YAdjust;
    public bool isActive;
    public bool moveUI;
    public Vector3 newPos;

    [Header("Entity Objects")]
    public RectTransform root;
    public EntityGrid Grid;
    public EntityBase selectedEntity;

    [Header("Machine HUDs")]
    public GameObject RoasteryUI;
    public GameObject BrewerUI;
    public GameObject GrinderUI;
    public GameObject EspressoUI;

    private void Start()
    {
        Grid = FindObjectOfType<EntityGrid>();
        root = GameObject.Find("Grid").GetComponent<RectTransform>();

        SetButtons();
        GetInitialPos();
        moveUI = true;
        InspectorUI.SetActive(false);
    }

    void Update()
    {
        GetSelectedEntity();
        if (InspectorUI.activeSelf)
        {
            UpdatePos();
            moveUI = false;
        }
        else 
        {
            moveUI = true;
        }
        CloseInspector();
    }

    private void GetSelectedEntity() 
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                if (Grid.HasEntity<GhostEntity>(gridPoint))
                {
                    if (Grid.HasEntity<EntityRoasteryMachineOne>(gridPoint))
                    {
                        EntityRoasteryMachineOne curEntity = Grid.GetEntities<EntityRoasteryMachineOne>(gridPoint)[0];
                        selectedEntity = curEntity;
                        MachineName.text = "Roastery";
                        InspectorUI.SetActive(true);
                    }
                    if (Grid.HasEntity<EntityBrewingMachineOne>(gridPoint))
                    {
                        EntityBrewingMachineOne curEntity = Grid.GetEntities<EntityBrewingMachineOne>(gridPoint)[0];
                        selectedEntity = curEntity;
                        MachineName.text = "Brewer";
                        InspectorUI.SetActive(true);
                    }
                    if (Grid.HasEntity<EntityEspressoMachineOne>(gridPoint))
                    {
                        EntityEspressoMachineOne curEntity = Grid.GetEntities<EntityEspressoMachineOne>(gridPoint)[0];
                        selectedEntity = curEntity;
                        MachineName.text = "Espresso";
                        InspectorUI.SetActive(true);
                    }
                }
            }
        }
    }

    private void GetInitialPos()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform, Input.mousePosition,
                parentCanvas.worldCamera,
                out Vector2 pos);

        pos = new Vector2(pos.x + XAdjust, pos.y + YAdjust);
    }

    private void UpdatePos()
    {
        if (moveUI)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out Vector2 movePos);

            newPos = new Vector2(movePos.x + XAdjust, movePos.y + YAdjust);

            InspectorUI.transform.position = parentCanvas.transform.TransformPoint(newPos);
        }
    }

    private void CloseInspector() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            TileConstruction getCS = FindObjectOfType<TileConstruction>();

            if (!getCS.isOverUI)
            {
                InspectorUI.SetActive(false);
            }
        }
    }

    private void SetButtons() 
    {
        InspectMachine_B.onClick.AddListener(OpenInterface);
        SellMachine_B.onClick.AddListener(SellEntity);
    }

    private void SellEntity() 
    {
        Grid.Destroy(selectedEntity);
        RoasteryUI.SetActive(false);
    }

    private void OpenInterface()
    {
        if (selectedEntity is EntityRoasteryMachineOne)
        {
            RoasteryUI.SetActive(true);
            InspectorUI.SetActive(false);
        }
        if (selectedEntity is EntityBrewingMachineOne)
        {
            BrewerUI.SetActive(true);
            InspectorUI.SetActive(false);


        }
        if (selectedEntity is EntityEspressoMachineOne)
        {
            EspressoUI.SetActive(true);
            InspectorUI.SetActive(false);

        }
        if (selectedEntity is EntityGrinderMachineOne)
        {
            GrinderUI.SetActive(true);
            InspectorUI.SetActive(false);

        }
    }
}
