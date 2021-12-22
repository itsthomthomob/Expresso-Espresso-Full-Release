using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileConstruction : MonoBehaviour
{
    [Header("UI Objects")]
    public RectTransform root;
    public Camera _main;
    public GameObject destroyIcon;

    [Header("Grid Objects")]
    public ConstructionSystem.SelectedTile selectedTile;
    public ConstructionSystem GetConstruction;
    private EntityGrid Grid;

    [Header("Modes")]
    public bool destroyOn;


    private void Start()
    {
		Grid = FindObjectOfType<EntityGrid>();
        GetConstruction = FindObjectOfType<ConstructionSystem>();
        destroyOn = false;
    }

    private void Update()
    {
        selectedTile = GetConstruction.currentTile;

        ManageMode();
        ManageBuilding();
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

    public void BuildTile(Vector2Int atPos) 
    {
        switch (selectedTile)
        {
            case ConstructionSystem.SelectedTile.Floor1:
                Grid.Create<EntityFloor>(atPos);
                break;
            case ConstructionSystem.SelectedTile.Floor2:
                break;
            case ConstructionSystem.SelectedTile.Floor3:
                break;
            case ConstructionSystem.SelectedTile.Floor4:
                break;
            case ConstructionSystem.SelectedTile.Floor5:
                break;
            case ConstructionSystem.SelectedTile.Wall1:
                break;
            default:
                break;
        }
    }
}
