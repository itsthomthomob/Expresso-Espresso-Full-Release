using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterUIController : MonoBehaviour
{
    public List<GameObject> UI_Objects;
    public GameObject currentlyActiveUI;
    public TileConstruction GetConstructionUI;

    public bool isActive;

    private void Start()
    {
        GetConstructionUI = FindObjectOfType<TileConstruction>();
    }

    private void Update()
    {
        CloseOnEscape();
        FindActiveUI();
    }

    private void FindActiveUI() 
    {
        if (isActive == false)
        {
            for (int i = 0; i < UI_Objects.Count; i++)
            {
                if (UI_Objects[i].activeSelf)
                {
                    currentlyActiveUI = UI_Objects[i];
                    isActive = true;
                }
            }
        }
    }

    private void CloseOnEscape() 
    {
        if (isActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentlyActiveUI != null) 
                { 
                    if (currentlyActiveUI.name == "Construction-UI")
                    {
                        GetConstructionUI.isConstructionOpen = false;
                        currentlyActiveUI = null;
                        isActive = false;
                    }
                    else if (currentlyActiveUI.name == "Shop-UI")
                    {
                        currentlyActiveUI.SetActive(false);
                        currentlyActiveUI = null;
                        isActive = false;
                    }
                    else 
                    {
                        currentlyActiveUI.SetActive(false);
                        Debug.Log("Set: " + currentlyActiveUI.name + " inactive.");

                        isActive = false;
                    }
                }
            }
        }
    }
}
