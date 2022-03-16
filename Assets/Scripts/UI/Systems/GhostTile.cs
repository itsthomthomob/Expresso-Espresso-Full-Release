using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GhostTile : MonoBehaviour
{
    [Header("UI Controllers")]
    public ConstructionSystemUI GetConstructionUI;
    public PauseManager GetPause;

    [Header("UI Positioning")]
    public GameObject GhostTileObj;
    public Image GhostTileImg;
    public Canvas parentCanvas;
    public float XAdjust = 10.00f;
    public float YAdjust = 10.00f;
    public Vector3 newPos;

    private void Start()
    {
        GetInitialPos();
        GhostTileObj.SetActive(false);
    }
    void Update()
    {
        UpdatePos();
        if (GetPause.isPaused)
        {
            GhostTileObj.SetActive(false);
            return;
        }
        GhostTileImg.sprite = UpdateTileImage();
    }

    private Sprite UpdateTileImage() 
    {
        switch (GetConstructionUI.currentTile)
        {
            case ConstructionSystemUI.SelectedTile.none:
                return null;
            case ConstructionSystemUI.SelectedTile.Floor1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor");
            case ConstructionSystemUI.SelectedTile.Floor2:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor1");
            case ConstructionSystemUI.SelectedTile.Floor3:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor2");
            case ConstructionSystemUI.SelectedTile.Floor4:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor3");
            case ConstructionSystemUI.SelectedTile.Floor5:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor4");
            case ConstructionSystemUI.SelectedTile.Floor6:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor5");
            case ConstructionSystemUI.SelectedTile.Floor7:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Floor6");
            case ConstructionSystemUI.SelectedTile.Wall1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall");
            case ConstructionSystemUI.SelectedTile.Wall2:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall1");
            case ConstructionSystemUI.SelectedTile.Wall3:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall2");
            case ConstructionSystemUI.SelectedTile.Wall4:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Building/Wall3");

            case ConstructionSystemUI.SelectedTile.Table1:
                GhostTileObj.SetActive(true);
                return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Table");
            case ConstructionSystemUI.SelectedTile.Chair1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Furniture/swood chair_left");
            case ConstructionSystemUI.SelectedTile.Counter1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Furniture/Counter1");
            case ConstructionSystemUI.SelectedTile.Barstool1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Furniture/rred chair_back");
            case ConstructionSystemUI.SelectedTile.Brewing1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Machines/Brewing-machine-1-front");
            case ConstructionSystemUI.SelectedTile.Espresso1:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Machines/Espresso-machine-1-front");
            case ConstructionSystemUI.SelectedTile.Roastery:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Machines/Roastery");
            case ConstructionSystemUI.SelectedTile.Register:
                GhostTileObj.SetActive(true);

                return Resources.Load<Sprite>("Sprites/Tiles/Machines/Register");
            default:
                return null;
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out Vector2 movePos);

        newPos = new Vector2(movePos.x + XAdjust, movePos.y + YAdjust);

        GhostTileObj.transform.position = parentCanvas.transform.TransformPoint(newPos);
    }

    
}
