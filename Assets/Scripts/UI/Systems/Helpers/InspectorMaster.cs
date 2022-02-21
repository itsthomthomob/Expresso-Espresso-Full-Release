using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectorMaster : MonoBehaviour
{
    [Header("UI Attributes")]
    public GameObject InspectorUI;
    public Canvas parentCanvas;
    public TMP_Text MachineName;
    public TMP_Text MachineLevel;
    public Button InspectMachine_B;
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
    }

    private void GetSelectedEntity() 
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));

                selectedEntity = Grid.GetLastEntity<EntityBase>(gridPoint);
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

    private void SetButtons() 
    {
        InspectMachine_B.onClick.AddListener(OpenInterface);
    }

    private void OpenInterface()
    {
        RoasteryUI.SetActive(true);
    }
}
