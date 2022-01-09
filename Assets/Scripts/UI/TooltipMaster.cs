using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipMaster : MonoBehaviour
{
    [Header("UI Attributes")]
    public GameObject TooltipUI;
    public Canvas parentCanvas;
    public float XAdjust = 10.00f;
    public float YAdjust = 10.00f;
    public Vector3 newPos;

    private void Start()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out Vector2 pos);

        pos = new Vector2(pos.x + XAdjust, pos.y + YAdjust);
    }

    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out Vector2 movePos);

        newPos = new Vector2(movePos.x + XAdjust, movePos.y + YAdjust);

        TooltipUI.transform.position = parentCanvas.transform.TransformPoint(newPos);
    }
}
