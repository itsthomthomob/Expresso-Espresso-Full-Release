using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraSystem : MonoBehaviour, IDragHandler
{
    [Header("Controllers")]
    public GameObject root;
    public Camera _main;
    public float cameraSpeed;
    public int zoomStep;
    public MasterUIController GetUI;

    private void Update()
    {
        if (!GetUI.isActive)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (root.TryGetComponent(out RectTransform rootTrans))
        {
            // Movement controls
            if (Input.GetKey(KeyCode.W))
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x, rootTrans.pivot.y + cameraSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x - cameraSpeed, rootTrans.pivot.y);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x, rootTrans.pivot.y - cameraSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x + cameraSpeed, rootTrans.pivot.y);
            }

            // Zoom controls
            if (Input.GetKey(KeyCode.Q))
            {
                rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x + zoomStep, rootTrans.sizeDelta.y + zoomStep);
            }
            if (Input.GetKey(KeyCode.E))
            {
                rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x - zoomStep, rootTrans.sizeDelta.y - zoomStep);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x - zoomStep, rootTrans.sizeDelta.y - zoomStep);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x + zoomStep, rootTrans.sizeDelta.y + zoomStep);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
