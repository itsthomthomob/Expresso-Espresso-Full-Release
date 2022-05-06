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
    public Canvas parentCanvas;
    public float XAdjust = 10.00f;
    public float YAdjust = 10.00f;
    public Vector3 newPos;
    public EntityGrid Grid;
    public RectTransform root;
    public ItemRotationManager rotationManager;

    private void Awake()
    {
        InitializeGhostEntity();
    }

    private void InitializeGhostEntity() 
    {
        Grid = FindObjectOfType<EntityGrid>();
        GetConstructionUI = FindObjectOfType<TileConstruction>();
        ghostEntity = Grid.Create<GhostEntity>(new Vector2Int());
        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent") as Sprite);
        rotationManager = FindObjectOfType<ItemRotationManager>();
    }

    void Update()
    {
        UpdateTileImage();
        UpdatePosition();
    }

    private void UpdatePosition() 
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
        {
            // Get Grid position relative to UI
            Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
            ghostEntity.Move(gridPoint);
        }
    }

    private void UpdateTileImage()
    {
        if (GetPause.isPaused)
        {
            ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Transparent") as Sprite);
        }

        // If a tile is selected and the game is not paused
        if (GetConstructionUI.curTile != TileConstruction.CurrentTileState.None 
            && 
            GetPause.isPaused == false)
        {
            ghostEntity.gameObject.GetComponent<Image>().material = applyMat;
        }

        if (GetConstructionUI.curTile != TileConstruction.CurrentTileState.None)
        {
            ghostEntity.gameObject.GetComponent<Image>().material = applyMat;
        }

        switch (GetConstructionUI.curTile)
        {
            case TileConstruction.CurrentTileState.Roaster:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery") as Sprite);
                break;
            case TileConstruction.CurrentTileState.Brewer:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front") as Sprite);
                break;
            case TileConstruction.CurrentTileState.Register:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Register") as Sprite);
                break;
            case TileConstruction.CurrentTileState.Espresso:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor1:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor2:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor1") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor3:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor2") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor4:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor3") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor5:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor4") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor6:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor5") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Floor7:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Floor6") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Wall1:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Wall2:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall1") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Wall3:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall2") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Wall4:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Building/Wall3") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Table1:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Table2:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table1") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Table3:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table2") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Table4:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table3") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Chair1:
                switch (rotationManager.rotAmount)
                {
                    case 0:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_front") as Sprite);

                        break;
                    case 90:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left") as Sprite);

                        break;
                    case 180:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_back") as Sprite);

                        break;
                    case 270:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_right") as Sprite);

                        break;
                }
                break;
            case TileConstruction.CurrentTileState.S_Chair2:
                switch (rotationManager.rotAmount)
                {
                    case 0:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_front") as Sprite);

                        break;
                    case 90:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_left") as Sprite);

                        break;
                    case 180:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_back") as Sprite);

                        break;
                    case 270:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rwood chair_right") as Sprite);

                        break;
                }
                break;
            case TileConstruction.CurrentTileState.S_Chair3:
                switch (rotationManager.rotAmount)
                {
                    case 0:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_front") as Sprite);

                        break;
                    case 90:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_left") as Sprite);

                        break;
                    case 180:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_back") as Sprite);

                        break;
                    case 270:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/sgrey chair_right") as Sprite);

                        break;
                }
                break;
            case TileConstruction.CurrentTileState.S_Chair4:
                switch (rotationManager.rotAmount)
                {
                    case 0:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_front") as Sprite);

                        break;
                    case 90:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_left") as Sprite);

                        break;
                    case 180:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_back") as Sprite);

                        break;
                    case 270:
                        ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rgrey chair_right") as Sprite);

                        break;
                }
                break;
            case TileConstruction.CurrentTileState.S_Counter1:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Counter2:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter1") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Counter3:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter2") as Sprite);
                break;
            case TileConstruction.CurrentTileState.S_Barstool:
                ghostEntity.SetGhostSprite(Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_front") as Sprite);
                break;
            case TileConstruction.CurrentTileState.None:
                break; 
            default:
                break; 
        }

    }
}
