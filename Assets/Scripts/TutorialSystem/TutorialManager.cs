using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Module Objects")]
    public GameObject TutorialModule;
    public GameObject[] TutorialPages;

    [Header("UI Controls")]
    public Button NextPage;
    public Button LastPage;
    public Button CloseTutorial;

    [Header("Attributes")]
    public int PageNumber;
    public bool ViewedTutorial = false;

    private void Start()
    {
        NextPage.onClick.AddListener(GetNextPage);
        LastPage.onClick.AddListener(GetLastPage);
        CloseTutorial.onClick.AddListener(CloseTutorialModule);
    }

    private void Update()
    {
        if (ViewedTutorial == false) 
        {
            LoadPage(PageNumber);
        }
    }

    private void LoadPage(int Page) 
    {
        switch (Page)
        {
            case 0:
                TutorialPages[0].SetActive(true);
                TutorialPages[1].SetActive(false);
                TutorialPages[2].SetActive(false);
                TutorialPages[3].SetActive(false);
                break;
            case 1:
                TutorialPages[0].SetActive(false);
                TutorialPages[1].SetActive(true);
                TutorialPages[2].SetActive(false);
                TutorialPages[3].SetActive(false);
                break;
            case 2:
                TutorialPages[0].SetActive(false);
                TutorialPages[1].SetActive(false);
                TutorialPages[2].SetActive(true);
                TutorialPages[3].SetActive(false);
                break;
            case 3:
                TutorialPages[0].SetActive(false);
                TutorialPages[1].SetActive(false);
                TutorialPages[2].SetActive(false);
                TutorialPages[3].SetActive(true);
                break;
        }
    }

    private void GetNextPage() 
    {
        if (PageNumber + 1 < 5)
        {
            PageNumber += 1;
        }
    }
    private void GetLastPage()
    {
        if (PageNumber - 1 != 0)
        {
            PageNumber -= 1;
        }
    }

    private void CloseTutorialModule()
    {
        TutorialModule.SetActive(false);
        ViewedTutorial = true;
    }

}
