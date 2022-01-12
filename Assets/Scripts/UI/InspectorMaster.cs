using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorMaster : MonoBehaviour
{
    [Header("UI Attributes")]
    public GameObject InspectorUI;
    public TMP_Text MachineName;
    public TMP_Text MachineLevel;
    public Canvas canvas;
    public float XAdjust;
    public float YAdjust;
    public bool isActive;
    public Vector3 newPos;

    private void Start()
    {
        InspectorUI.SetActive(false);
        isActive = gameObject.activeSelf;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos);

        pos = new Vector2(pos.x + XAdjust, pos.y + YAdjust);
    }

    void Update()
    {
        if (isActive)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera,
                out Vector2 movePos);

            newPos = new Vector2(movePos.x + XAdjust, movePos.y + YAdjust);

            InspectorUI.transform.position = canvas.transform.TransformPoint(newPos);
            isActive = !isActive;
        }
    }
}
