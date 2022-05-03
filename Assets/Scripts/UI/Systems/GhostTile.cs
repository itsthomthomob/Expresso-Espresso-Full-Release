using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GhostTile : MonoBehaviour
{
    [Header("UI Controllers")]
    public TileConstruction GetConstructionUI;
    public PauseManager GetPause;
    public GhostEntity ghostEntity;
    public Material applyMat;

    [Header("UI Positioning")]
    public Image GhostTileImg;
    public Canvas parentCanvas;
    public float XAdjust = 10.00f;
    public float YAdjust = 10.00f;
    public Vector3 newPos;
    public EntityGrid Grid;

    public RectTransform root;

    private void Start()
    {
        Grid = FindObjectOfType<EntityGrid>();
        EntityBase newEntity = Grid.Create<GhostEntity>(new Vector2Int(0, 0));
        ghostEntity = newEntity as GhostEntity;
        GetConstructionUI = FindObjectOfType<TileConstruction>();
    }

    void Update()
    {
        UpdatePos();
        UpdateTileImage();
    }

    private void UpdatePos()
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
        {
            // Get Grid position relative to UI
            Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
            ghostEntity.Move(gridPoint, 0f);
        }
    }

    private void UpdateTileImage()
    {
        if (ghostEntity.gameObject.GetComponent<Image>().material != applyMat)
        {
            ghostEntity.gameObject.GetComponent<Image>().material = applyMat;
        }
        if (GetPause.isPaused)
        {
            ghostEntity.gameObject.SetActive(false);
            return;
        }

        switch (GetConstructionUI.curTile)
        {
            case TileConstruction.CurrentTileState.Roaster:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery"));
                break;
            case TileConstruction.CurrentTileState.Brewer:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front"));
                break;
            case TileConstruction.CurrentTileState.Register:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Register"));
                break;
            case TileConstruction.CurrentTileState.Espresso:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front"));
                break;
            case TileConstruction.CurrentTileState.S_Floor1:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor"));
                break;
            case TileConstruction.CurrentTileState.S_Floor2:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor1"));
                break;
            case TileConstruction.CurrentTileState.S_Floor3:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor2"));
                break;
            case TileConstruction.CurrentTileState.S_Floor4:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor3"));
                break;
            case TileConstruction.CurrentTileState.S_Floor5:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor4"));
                break;
            case TileConstruction.CurrentTileState.S_Floor6:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor5"));
                break;
            case TileConstruction.CurrentTileState.S_Floor7:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Floor6"));
                break;
            case TileConstruction.CurrentTileState.S_Wall1:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Wall"));
                break;
            case TileConstruction.CurrentTileState.S_Wall2:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Wall1"));
                break;
            case TileConstruction.CurrentTileState.S_Wall3:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Wall2"));
                break;
            case TileConstruction.CurrentTileState.S_Wall4:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Wall3"));
                break;
            case TileConstruction.CurrentTileState.S_Table1:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table"));
                break;
            case TileConstruction.CurrentTileState.S_Table2:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table1"));
                break;
            case TileConstruction.CurrentTileState.S_Table3:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table2"));
                break;
            case TileConstruction.CurrentTileState.S_Table4:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table3"));
                break;
            case TileConstruction.CurrentTileState.S_Chair1:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left"));
                break;
            case TileConstruction.CurrentTileState.S_Chair2:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left"));
                break;
            case TileConstruction.CurrentTileState.S_Chair3:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_left"));
                break;
            case TileConstruction.CurrentTileState.S_Chair4:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_left"));
                break;
            case TileConstruction.CurrentTileState.S_Counter1:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter1"));
                break;
            case TileConstruction.CurrentTileState.S_Counter2:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter2"));
                break;
            case TileConstruction.CurrentTileState.S_Counter3:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter3"));
                break;
            case TileConstruction.CurrentTileState.S_Barstool:
                 
                ghostEntity.gameObject.SetActive(true);
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_front"));
                break;
            case TileConstruction.CurrentTileState.None:
                break; 
            default:
                break; 
        }

    }
}
