using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileConstruction : MonoBehaviour
{
    public enum EntityTypeSelected 
    { 
        All,
        Tiles,
        Characters,
        Items
    }

    [Header("UI Objects")]
    public Canvas globalCanvas;
    public RectTransform root;
    public Camera _main;
    public GameObject destroyIcon;

    [Header("Grid Objects")]
    public ConstructionSystemUI.SelectedTile selectedTile;
    public ConstructionSystemUI GetConstruction;
    private EntityGrid Grid;

    [Header("Modes")]
    public bool destroyOn;
    public bool isOverUI;

    [Header("Selection Mode")]
    public bool selectionMode;
    public Vector2Int min = new Vector2Int();
    public Vector2Int max = new Vector2Int();
    public Vector2Int minSelected = new Vector2Int();
    public Vector2Int maxSelected = new Vector2Int();
    public GameObject selectionRect;
    public RectTransform selectionRectImg;
    public EntityTypeSelected currentTypeSelected;
    public EntityBase[] selectedEntities;

    private void Start()
    {
		Grid = FindObjectOfType<EntityGrid>();
        GetConstruction = FindObjectOfType<ConstructionSystemUI>();

        selectionRectImg = selectionRect.GetComponent<RectTransform>();
        selectionRect.SetActive(false);

        currentTypeSelected = EntityTypeSelected.All;

        destroyOn = false;
        selectionMode = false;
    }

    private void Update()
    {
        selectedTile = GetConstruction.currentTile;

        ManageMode();
        if (!isOverUI)
        {
            ManageBuilding();
            TileSelection();
            ManageSelected();
        }
    }

    public void ManageMode() 
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            destroyOn = !destroyOn;
            Debug.Log("Destroy on: " + destroyOn);
        }

        if (destroyOn)
        {
            destroyIcon.SetActive(true);
        }
        else 
        { 
            destroyIcon.SetActive(false);
        }
    }
    public void ManageBuilding() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (selectedTile != ConstructionSystemUI.SelectedTile.none)
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                {
                    Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                    if (destroyOn)
                    {
                        Debug.Log("Destroying: " + Grid.GetLastEntity<EntityBase>(gridPoint));
                        if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Characters) { }
                        else if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Terrain) { }
                        else if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Buildings) 
                        {
                            Grid.Destroy(Grid.GetLastEntity<EntityBase>(gridPoint));
                        }
                    }
                    else
                    {
                        Debug.Log("Created on: " + Grid.GetLastEntity<EntityBase>(gridPoint));
                        BuildTile(gridPoint);
                    }
                }
            }
        }
    }
    public void BuildTile(Vector2Int atPos) 
    {
        switch (selectedTile)
        {
            case ConstructionSystemUI.SelectedTile.Floor1:
                Grid.Create<EntityFloor>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor2:
                Grid.Create<EntityFloorTwo>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor3:
                Grid.Create<EntityFloorThree>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor4:
                Grid.Create<EntityFloorFour>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor5:
                Grid.Create<EntityFloorFive>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor6:
                Grid.Create<EntityFloorSix>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Floor7:
                Grid.Create<EntityFloorSeven>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Wall1:
                Grid.Create<EntityWallBrick>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Register:
                Grid.Create<EntityRegister>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Barstool1:
                Grid.Create<EntityBarstool>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Counter1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityCounterMarble>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Table1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityTableSmooth>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Chair1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityChairSmooth>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Brewing1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityBrewingMachineOne>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Espresso1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityEspressoMachineOne>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Roastery:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityRoasteryMachineOne>(atPos);

                break;
            default:
                break;
        }
    }
    public void TileSelection() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(globalCanvas.transform as RectTransform, Input.mousePosition, globalCanvas.worldCamera, out pos);
            min = new Vector2Int((int)globalCanvas.transform.TransformPoint(pos).x, (int)globalCanvas.transform.TransformPoint(pos).y);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                minSelected = gridPoint;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            selectionMode = true;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));

                // Manage UI selection 
                if (selectionMode == true)
                {
                    selectionRect.SetActive(true);
                    selectionRect.transform.localScale = new Vector2(1, 1);
                    selectionRect.transform.position = new Vector3(min.x, min.y, -10);

                    max = new Vector2Int((int)Input.mousePosition.x + 1, (int)Input.mousePosition.y + 1);
                    Vector2Int minCorner = Vector2Int.Min(min, max);
                    Vector2Int maxCorner = Vector2Int.Max(min, max);

                    selectionRectImg.offsetMin = minCorner;
                    selectionRectImg.offsetMax = maxCorner;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            if (selectionMode == true)
            {
                Debug.Log("Released left-mouse");
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                {
                    Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                    Debug.Log("Max Point: " + gridPoint);
                    maxSelected = gridPoint;
                }
                if (selectedEntities.Length == 0)
                {
                    selectedEntities = Grid.FindEntities(minSelected, maxSelected, true);
                    if (selectedEntities.Length <= 2)
                    {
                        for (int i = 0; i < selectedEntities.Length; i++)
                        {
                            selectedEntities[i].GetComponent<Image>().material = null;
                        }
                        selectedEntities = new EntityBase[0];
                        return;
                    }
                    for (int i = 0; i < selectedEntities.Length; i++)
                    {
                        if (selectedEntities[i].Priority == EntityPriority.Characters) { }
                        else
                        {
                            GameObject currentEntity = selectedEntities[i].gameObject;
                            currentEntity.GetComponent<Image>().material = Resources.Load<Material>("Sprites/UI/Indicators/SelectedEntity");
                        }
                    }
                }
                selectionRect.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (selectedEntities.Length > 0)
            {
                for (int i = 0; i < selectedEntities.Length; i++)
                {
                    if (selectedEntities[i] == null)
                    {
                        return;
                    }
                    GameObject currentEntity = selectedEntities[i].gameObject;
                    currentEntity.GetComponent<Image>().material = null;
                }
                selectedEntities = new EntityBase[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (selectedEntities.Length != 0)
            {
                for (int i = 0; i < selectedEntities.Length; i++)
                {
                    if (selectedEntities[i].Priority == EntityPriority.Characters) {
                        GameObject currentEntity = selectedEntities[i].gameObject;
                        currentEntity.GetComponent<Image>().material = null;
                    }
                    else if (selectedEntities[i].Priority == EntityPriority.Terrain) {
                        GameObject currentEntity = selectedEntities[i].gameObject;
                        currentEntity.GetComponent<Image>().material = null;
                    }
                    else if (selectedEntities[i].Priority == EntityPriority.Buildings) 
                    {
                        GameObject currentEntity = selectedEntities[i].gameObject;
                        currentEntity.GetComponent<Image>().material = null; 
                        Grid.Destroy(selectedEntities[i]); 
                    }
                    
                }
            }
            selectedEntities = new EntityBase[0];
        }
    }
    public void ManageSelected() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTypeSelected = EntityTypeSelected.Tiles;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTypeSelected = EntityTypeSelected.Characters;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentTypeSelected = EntityTypeSelected.Items;
        }
    }
}