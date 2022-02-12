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
    public GameObject ghostTile;

    [Header("Grid Objects")]
    public ConstructionSystemUI.SelectedTile selectedTile;
    public ConstructionSystemUI GetConstruction;
    public PauseManager GetPause;

    private EntityGrid Grid;

    [Header("Modes")]
    public bool destroyOn;
    public bool isOverUI;
    public bool IsUIActive;

    [Header("Selection Mode")]
    public bool selectionMode;

    public Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
    public Vector2 referenceResolution = new Vector2((int)(1920 / 2), (int)(1080 / 2));

    public Vector2 firstClick = new Vector2();

    [Tooltip("The minimum corner of the selection rectangle.")]
    public Vector2Int min = new Vector2Int();

    [Tooltip("The maximum corner of the selection rectangle.")]
    public Vector2Int max = new Vector2Int();

    [Tooltip("The minimum corner of the selected entities.")]
    public Vector2Int minSelected = new Vector2Int();

    [Tooltip("The maximum corner of the selected entities.")]

    public Vector2Int maxSelected = new Vector2Int();

    public GameObject selectionRect;
    public RectTransform selectionRectImg;
    public EntityTypeSelected currentTypeSelected;
    public EntityBase[] selectedEntities;

    private void Start()
    {
		SetStates();
    }

    private void Update()
    {
        currentResolution = new Vector2(Screen.width, Screen.height);
        if (GetPause.isPaused)
        {
            return;
        }

        IsUIActive = FindObjectOfType<MasterUIController>().isActive;
        selectedTile = GetConstruction.currentTile;

        if (IsUIActive == true || isOverUI == true || ghostTile.activeSelf ||
            selectedTile != ConstructionSystemUI.SelectedTile.none)
        {
            selectionMode = false;
            selectionRect.SetActive(false);
        }

        if (IsUIActive == false)
        {
            ManageMode();
            if (!isOverUI)
            {
                ManageBuilding();
                TileSelection();
                //TO DO: RectangleSelection();
                ManageSelected();
            }
        }
    }

	public void SetStates()
	{
				Grid = FindObjectOfType<EntityGrid>();
        GetConstruction = FindObjectOfType<ConstructionSystemUI>();

        selectionRectImg = selectionRect.GetComponent<RectTransform>();

        currentTypeSelected = EntityTypeSelected.All;

        destroyOn = false;
        selectionMode = false;
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
            case ConstructionSystemUI.SelectedTile.Wall2:
                Grid.Create<EntityWallPlaster>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Wall3:
                Grid.Create<EntityWallPale>(atPos);
                break;
            case ConstructionSystemUI.SelectedTile.Wall4:
                Grid.Create<EntityWallGreyBrick>(atPos);
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

    public void RectangleSelection() 
    {

        float ratioX = currentResolution.x / referenceResolution.x;
        float ratioY = currentResolution.y / referenceResolution.y;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float mousePosX = Input.mousePosition.x / ratioX;
            float mousePosY = Input.mousePosition.y / ratioY;

            selectionRectImg.offsetMin = new Vector2(((mousePosX)), ((mousePosY)));
            firstClick = selectionRectImg.offsetMin; // offsetMin value stored
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            selectionRect.SetActive(true);

            float mousePosX = Input.mousePosition.x / ratioX;
            float mousePosY = Input.mousePosition.y / ratioY;

            // If negative X, negative Y (-1, -1)
            if ((mousePosX - selectionRectImg.offsetMin.x) < 0 && (mousePosY - selectionRectImg.offsetMin.y) < 0)
            {
                selectionRectImg.offsetMax = firstClick; // set max to first Min
                selectionRectImg.offsetMin = new Vector2(((mousePosX)), ((mousePosY))); // set min to mouse pos

                Debug.Log("\n MousePos: " + mousePosX * -1 + ", " + mousePosY * -1 + "\n firstClick" + firstClick);
            }
            // (1, -1)
            else if ((mousePosX - selectionRectImg.offsetMin.x) > 0 && (mousePosY - selectionRectImg.offsetMin.y) < 0)
            {
                selectionRectImg.offsetMax = new Vector2(firstClick.x, mousePosY); // set max to first Min
                selectionRectImg.offsetMin = new Vector2(((mousePosX)), ((firstClick.y))); // set min to mouse pos
                
                Debug.Log("\n MousePos: " + mousePosX * -1 + ", " + mousePosY * -1 + "\n firstClick" + firstClick);
            }
            // (-1, 1)
            else if ((mousePosX - selectionRectImg.offsetMin.x) < 0 && (mousePosY - selectionRectImg.offsetMin.y) > 0)
            {
                selectionRectImg.offsetMax = firstClick; // set max to first Min
                selectionRectImg.offsetMin = new Vector2(((mousePosX)), ((mousePosY))); // set min to mouse pos

                Debug.Log("\n MousePos: " + mousePosX * -1 + ", " + mousePosY * -1 + "\n firstClick" + firstClick);
            }
            else // positive
            { 
                selectionRectImg.offsetMax = new Vector2(((mousePosX)), ((mousePosY)));
            }
        }
    }

    public void TileSelection() 
    {
        // Get tile position
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                minSelected = gridPoint;
            }
        }

        // For dragging selection rectangle
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (selectedEntities.Length > 0)
            {
                for (int i = 0; i < selectedEntities.Length; i++)
                {
                    selectedEntities[i].GetComponent<Image>().material = null;
                }
                selectedEntities = new EntityBase[0];
            }

            if (IsUIActive == false || isOverUI == false)
            {
                selectionMode = true;
            }

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));

                // Manage UI selection 
                if (selectionMode == true)
                {
                    if (GetConstruction.currentTile == ConstructionSystemUI.SelectedTile.none)
                    { 
                        
                        maxSelected = gridPoint;

                        selectedEntities = Grid.FindEntities(minSelected, maxSelected, true);

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
                    else 
                    {
                        selectionMode = false;
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (selectionMode == true)
            {
                selectionRect.SetActive(false);
                selectionMode = false;
            }
        }
            // Clear selected entities, exit selection mode
            if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GetConstruction.currentTile != ConstructionSystemUI.SelectedTile.none)
            {
                GetConstruction.currentTile = ConstructionSystemUI.SelectedTile.none;
                ghostTile.SetActive(false);
            }

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

        // Delete all entities except for terrain and character priorities
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