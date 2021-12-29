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
    public ConstructionSystem.SelectedTile selectedTile;
    public ConstructionSystem GetConstruction;
    private EntityGrid Grid;

    [Header("Modes")]
    public bool destroyOn;

    [Header("Square Mode")]
    public bool squareMode;
    public int ClickCount;
    public Vector2Int spawnPos;
    public Vector3 firstPos;
    public Vector2Int secondPos;

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
        GetConstruction = FindObjectOfType<ConstructionSystem>();

        selectionRectImg = selectionRect.GetComponent<RectTransform>();
        selectionRect.SetActive(false);

        currentTypeSelected = EntityTypeSelected.All;

        destroyOn = false;
        squareMode = false;
        selectionMode = false;
    }

    private void Update()
    {
        selectedTile = GetConstruction.currentTile;

        ManageMode();
        ManageBuilding();
        TileSelection();
        ManageSelected();
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
            if (squareMode == false)
            {
                if (selectedTile != ConstructionSystem.SelectedTile.none)
                {
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                    {
                        Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                        if (destroyOn)
                        {
                            Debug.Log("Destroying: " + Grid.GetLastEntity<EntityBase>(gridPoint));
                            if (Grid.GetLastEntity<EntityBase>(gridPoint) is EntityAlien)
                            {
                                // Do nothing, can't destroy characters
                                Debug.Log("Can't destroy characters");
                            }
                            else 
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            squareMode = true;
            Vector2Int gridPoint = new Vector2Int();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
                {
                    gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                   
                    // BUG: first pos and second pos are 0

                    if (ClickCount == 0)
                    {
                        spawnPos = gridPoint;
                        ClickCount = 1;
                        Debug.Log("Got first pos: " + spawnPos);
                    }
                    
                    if (ClickCount == 1)
                    {
                        if (gridPoint != spawnPos)
                        {
                            secondPos = gridPoint;
                            ClickCount = 2;
                            Debug.Log("Got second pos" + secondPos);
                        }
                    }

                    if (ClickCount == 2)
                    {
                        EntityBase[] buildSquare;
                        Debug.Log("Building square: " + spawnPos + " to " + secondPos);
                        buildSquare = Grid.FindEntities(spawnPos, secondPos);
                        Debug.Log("Declared square: " + buildSquare.Length);
                        for (int i = 0; i < buildSquare.Length; i++)
                        {
                            switch (selectedTile)
                            {
                                case ConstructionSystem.SelectedTile.Floor1:
                                    Grid.Create<EntityFloor>(buildSquare[i].Position);
                                    break;
                            }
                        }
                        ClickCount = 0;
                        Debug.Log("Built square");
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            squareMode = false;
        }
    }
    public void BuildTile(Vector2Int atPos) 
    {
        switch (selectedTile)
        {
            case ConstructionSystem.SelectedTile.Floor1:
                Grid.Create<EntityFloor>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Floor2:
                Grid.Create<EntityFloorTwo>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Floor3:
                Grid.Create<EntityFloorThree>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Floor4:
                Grid.Create<EntityFloorFour>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Floor5:
                Grid.Create<EntityFloorSix>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Wall1:
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
            if (squareMode == false)
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

                        max = new Vector2Int((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                        Vector2Int minCorner = Vector2Int.Min(min, max);
                        Vector2Int maxCorner = Vector2Int.Max(min, max);

                        selectionRectImg.offsetMin = minCorner;
                        selectionRectImg.offsetMax = maxCorner;
                    }
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
                    selectedEntities = Grid.FindEntities(minSelected, maxSelected);
                    for (int i = 0; i < selectedEntities.Length; i++)
                    {
                        GameObject currentEntity = selectedEntities[i].gameObject;
                        currentEntity.GetComponent<Image>().material = Resources.Load<Material>("Sprites/UI/Indicators/SelectedEntity");
                    }
                }

                selectionRect.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (selectedEntities.Length != 0)
            {
                for (int i = 0; i < selectedEntities.Length; i++)
                {
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
                    Grid.DestroyAll(selectedEntities[i].Position);
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