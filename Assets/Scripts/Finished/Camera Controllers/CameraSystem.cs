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
    public MasterUIController GetUI;
    public float cameraSpeed;

    [Header("Settings")]
    public int zoomStep;
    public int zoomMax = 1000;
    public int zoomMin;

    [Space(10)]

    public RectTransform getGridRT;
    public BoxCollider2D gridBounds;
    Vector2 InitialBoundsOffset = new Vector2();
    Vector2 InitialBoundsSize = new Vector2();
    public float BoundsMapScale;

    float curWidthRT;
    float curHeightRT;
    float curBoundsSizeX;
    float curBoundsSizeY;
    float curWidthDiff;
    float curHeightDiff;

    [Header("HUD Bounds")]
    public BoxCollider2D TopBounds;
    public BoxCollider2D BottomBounds;
    public BoxCollider2D LeftBounds;
    public BoxCollider2D RightBounds;
    public bool canUp = true;
    public bool canDown = true;
    public bool canLeft = true;
    public bool canRight = true;

    private void Start()
    {
        InitialBoundsOffset = gridBounds.offset;
        InitialBoundsSize = gridBounds.size;
        root.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
        root.GetComponent<RectTransform>().pivot = new Vector2(0, -11);
    }

    private void Update()
    {
        UpdateGridLimits();
        CheckBounds();
        if (!GetUI.isActive)
        {
            HandleMovement();
            //HandleBounds();
        }
    }

    /// <summary>
    /// Helps automatically clamp movement by adjusting grid's collider.
    /// </summary>
    private void UpdateGridLimits() 
    {
        // starting grid is always 50, starting bounds is always 2100
        BoundsMapScale = 50f / 2100f; 

        float newColSizeX = 2100f;
        float newColSizeY = 2100f;

        if (getGridRT.rect.width != 50)
        {
            curWidthRT = getGridRT.rect.width;
            curWidthDiff = curWidthRT - 50f;
            newColSizeX = (2100 * BoundsMapScale) * curWidthDiff;
            newColSizeX = newColSizeX + 2100;
        }

        if (getGridRT.rect.height != 50)
        {
            curHeightRT = getGridRT.rect.height;
            curHeightDiff = curHeightRT - 50f;
            newColSizeY = (2100 * BoundsMapScale) * curHeightDiff;
            newColSizeY = newColSizeY + 2100;
        }

        gridBounds.size = new Vector2(newColSizeX, newColSizeY);
    }

    private void CheckBounds() 
    {
        if (!gridBounds.bounds.Intersects(TopBounds.bounds))
        {
            // Not touching TopBounds anymore
            canUp = false;
        }
        else 
        {
            canUp = true;
        }

        if (!gridBounds.bounds.Intersects(BottomBounds.bounds))
        {
            // Not touching TopBounds anymore
            canDown = false;
        }
        else
        {
            canDown = true;

        }

        if (!gridBounds.bounds.Intersects(LeftBounds.bounds))
        {
            // Not touching TopBounds anymore
            canLeft = false;
        }
        else
        {
            canLeft = true;

        }

        if (!gridBounds.bounds.Intersects(RightBounds.bounds))
        {
            // Not touching TopBounds anymore
            canRight = false;
        }
        else
        {
            canRight = true;

        }
    }

    private void HandleMovement()
    {

        if (root.TryGetComponent(out RectTransform rootTrans))
        {
            // Movement controls
            if (Input.GetKey(KeyCode.W) && canUp)
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x, rootTrans.pivot.y + cameraSpeed);
            }
            if (Input.GetKey(KeyCode.A) && canLeft)
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x - cameraSpeed, rootTrans.pivot.y);
            }
            if (Input.GetKey(KeyCode.S) && canDown)
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x, rootTrans.pivot.y - cameraSpeed);
            }
            if (Input.GetKey(KeyCode.D) && canRight)
            {
                rootTrans.pivot = new Vector2(rootTrans.pivot.x + cameraSpeed, rootTrans.pivot.y);
            }

            // Zoom controls
            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("curxQ-Step: " + (rootTrans.sizeDelta.x + zoomStep));
                if ((rootTrans.sizeDelta.x + zoomStep) > zoomMax ||
                    (rootTrans.sizeDelta.y + zoomStep) > zoomMax)
                {
                    // do nothing
                    Debug.Log("Stopping zoom-in.");
                }
                else 
                { 
                    rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x + zoomStep, rootTrans.sizeDelta.y + zoomStep);
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("curxE-Step: " + (rootTrans.sizeDelta.x - zoomStep));

                if (rootTrans.sizeDelta.x - zoomStep < zoomMin ||
                    rootTrans.sizeDelta.y - zoomStep < zoomMin)
                {
                    // do nothing
                }
                else 
                {
                    rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x - zoomStep, rootTrans.sizeDelta.y - zoomStep);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (rootTrans.sizeDelta.x - zoomStep < zoomMin ||
                    rootTrans.sizeDelta.y - zoomStep < zoomMin)
                {
                    // do nothing
                }
                else 
                { 
                    rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x - zoomStep, rootTrans.sizeDelta.y - zoomStep);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (rootTrans.sizeDelta.x + zoomStep > zoomMax ||
                    rootTrans.sizeDelta.y + zoomStep > zoomMax)
                {
                    // do nothing
                }
                else 
                { 
                    rootTrans.sizeDelta = new Vector2(rootTrans.sizeDelta.x + zoomStep, rootTrans.sizeDelta.y + zoomStep);
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
