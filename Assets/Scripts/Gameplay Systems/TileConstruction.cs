using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileConstruction : MonoBehaviour
{
    [Header("UI Objects")]
    public RectTransform root;
    public Camera _main;

    [Header("Grid Objects")]
    private EntityGrid Grid;

    private void Start()
    {
		Grid = FindObjectOfType<EntityGrid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Debug.Log(localPoint.x/root.sizeDelta.x + ", " + localPoint.y/ root.sizeDelta.y);
                Vector2 curPos = new Vector2(localPoint.x / 50, localPoint.y / 50);
                Vector2Int curPosRounded = Vector2Int.RoundToInt(curPos);

                Debug.Log(Grid.GetLastEntity<EntityBase>(curPosRounded));
            }
        }
    }
}
