using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButton : MonoBehaviour
{
    public GameObject ChangesUI;
    public GameObject ChangesBackground;
    public Button Updates_B;
    public Button CloseUpdates;
    public GameObject notification;
    public bool boolData;

    private void Start()
    {
        Updates_B.onClick.AddListener(OpenUpdatesUI);
        CloseUpdates.onClick.AddListener(CloseUpdatesUI);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("ClickedUpdate2") == 1)
        {
            notification.SetActive(false);
        }
    }

    void OpenUpdatesUI() 
    {
        ChangesUI.SetActive(true);
        ChangesBackground.SetActive(true);

        PlayerPrefs.SetInt("ClickedUpdate2", 1);
    }
    void CloseUpdatesUI()
    {
        ChangesUI.SetActive(false);
        ChangesBackground.SetActive(false);
    }
}