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
    public GhostTile getGhost;

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

    [Header("Audio")]
    public AudioSource CameraAudio;
    public AudioClip singleTile;
    public AudioClip multiTile;
    public AudioClip machineTile;

    [Header("Objectives")]
    public List<EntityBase> AllFloors = new List<EntityBase>();
    public List<EntityBase> AllWalls = new List<EntityBase>();
    public List<EntityBase> AllCounters = new List<EntityBase>();
    public List<EntityRegister> AllRegisters = new List<EntityRegister>();

    private void Start()
    {
		SetStates();
        GhostTile ghostScript = FindObjectOfType<GhostTile>();
    }

    private void Update()
    {
        FindBlocker();

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

    public void FindBlocker() 
    {
        GameObject blocker = GameObject.Find("Blocker");
        if (blocker != null)
        {
            Destroy(blocker);
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
                        if (selectedEntities.Length > 0)
                        {
                            for (int i = 0; i < selectedEntities.Length; i++)
                            {
                                selectedEntities[i].GetComponent<Image>().material = null;
                            }
                            selectedEntities = new EntityBase[0];
                        }
                        Debug.Log("Destroying: " + Grid.GetLastEntity<EntityBase>(gridPoint));
                        if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Characters) { }
                        else if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Terrain) { }
                        else if (Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Buildings
                                || Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Furniture
                                || Grid.GetLastEntity<EntityBase>(gridPoint).Priority == EntityPriority.Foundations) 
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
                EntityBase curEntity = Grid.Create<EntityFloor>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity);
                break;
            case ConstructionSystemUI.SelectedTile.Floor2:
                EntityBase curEntity1 = Grid.Create<EntityFloorTwo>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity1);

                break;
            case ConstructionSystemUI.SelectedTile.Floor3:
                EntityBase curEntity2 = Grid.Create<EntityFloorThree>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity2);

                break;
            case ConstructionSystemUI.SelectedTile.Floor4:
                EntityBase curEntity3 = Grid.Create<EntityFloorFour>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity3);

                break;
            case ConstructionSystemUI.SelectedTile.Floor5:
                EntityBase curEntity4 = Grid.Create<EntityFloorFive>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity4);

                break;
            case ConstructionSystemUI.SelectedTile.Floor6:
                EntityBase curEntity5 = Grid.Create<EntityFloorSix>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity5);

                break;
            case ConstructionSystemUI.SelectedTile.Floor7:
                EntityBase curEntity6 = Grid.Create<EntityFloorSeven>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllFloors.Add(curEntity6);

                break;
            case ConstructionSystemUI.SelectedTile.Wall1:
                EntityBase curEntity7 = Grid.Create<EntityWallBrick>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllWalls.Add(curEntity7);
                break;
            case ConstructionSystemUI.SelectedTile.Wall2:
                EntityBase curEntity8 = Grid.Create<EntityWallPlaster>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllWalls.Add(curEntity8);

                break;
            case ConstructionSystemUI.SelectedTile.Wall3:
                EntityBase curEntity9 = Grid.Create<EntityWallPale>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllWalls.Add(curEntity9);

                break;
            case ConstructionSystemUI.SelectedTile.Wall4:
                EntityBase curEntity10 = Grid.Create<EntityWallGreyBrick>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllWalls.Add(curEntity10);

                break;
            case ConstructionSystemUI.SelectedTile.Register:
                EntityRegister newReg = Grid.Create<EntityRegister>(atPos);
                CameraAudio.PlayOneShot(machineTile, 1.0f);
                AllRegisters.Add(newReg);

                break;
            case ConstructionSystemUI.SelectedTile.Barstool1:
                Grid.Create<EntityBarstool>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);


                break;
            case ConstructionSystemUI.SelectedTile.Counter1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                EntityBase curEntity11 = Grid.Create<EntityCounterMarble>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                AllCounters.Add(curEntity11);

                break;
            case ConstructionSystemUI.SelectedTile.Table1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityTableSmooth>(atPos);
                CameraAudio.PlayOneShot(singleTile, 1.0f);

                break;
            case ConstructionSystemUI.SelectedTile.Chair1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                CameraAudio.PlayOneShot(singleTile, 1.0f);
                Grid.Create<EntityChairSmooth>(atPos);

                break;
            case ConstructionSystemUI.SelectedTile.Brewing1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityBrewingMachineOne>(atPos);
                CameraAudio.PlayOneShot(machineTile, 1.0f);


                break;
            case ConstructionSystemUI.SelectedTile.Espresso1:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityEspressoMachineOne>(atPos);
                CameraAudio.PlayOneShot(machineTile, 1.0f);


                break;
            case ConstructionSystemUI.SelectedTile.Roastery:
                if (Grid.GetLastEntity<EntityBase>(atPos).Priority == EntityPriority.Furniture)
                {
                    return;
                }
                Grid.Create<EntityRoasteryMachineOne>(atPos);
                CameraAudio.PlayOneShot(machineTile, 1.0f);


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
                getGhost.ghostEntity.SetGhostPriority(EntityPriority.Terrain);
                getGhost.ghostEntity.gameObject.SetActive(false);
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