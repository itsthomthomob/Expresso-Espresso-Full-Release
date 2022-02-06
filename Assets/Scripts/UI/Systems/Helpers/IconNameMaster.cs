using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconNameMaster : MonoBehaviour
{
    [Header("UI Attributes")]
    public GameObject IconMaster;
    public Canvas parentCanvas;
    public float XAdjust = 10.00f;
    public float YAdjust = 10.00f;
    public Vector3 newPos;

    private void Start()
    {
        GetInitialPos();
    }
    void Update()
    {
        UpdatePos();
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

        IconMaster.transform.position = parentCanvas.transform.TransformPoint(newPos);
    }
}
