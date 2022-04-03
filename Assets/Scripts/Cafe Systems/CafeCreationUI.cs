using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CafeCreationUI : MonoBehaviour
{

    public MasterCafeSystem getCafe;

    [Header("Pages")]
    public GameObject BrandingPage;
    public GameObject LoanPage;
    public GameObject LocationsPage;
    public int OnPage = 1;

    [Header("Controllers")]
    public Button Last;
    public Button NextPageButton;

    [Header("Page 1 Objects")]
    public TMP_InputField CafeNameInput;
    public Button CorporateButton;
    public Button CommunityButton;
    public string cafeType;

    [Header("Page 2 Objects")]
    public Button LoanOption1;
    public Button LoanOption2;
    public Button LoanOption3;
    public int curLoanAmount = 0;
    public float curLoanInterest = 0f;
    public TimeSpan curLoanDueDate = new TimeSpan();

    [Header("Page 3 Objects")]
    public Button LocationOption1;
    public Button LocationOption2;
    public Button LocationOption3;
    public Button LocationOption4;
    public Button LocationOption5;
    public Button PlayGame;
    public string curLocName;
    public float curWeatherCon;
    public float curPopRate;
    public float curMinWage;

    private void Start()
    {
        LoadButtons();
    }

    private void Update()
    {
        ManagePages();
        UpdateCafeManager();
    }

    private void UpdateCafeManager() 
    {
        if (getCafe != null) 
        { 
            getCafe.CafeName = CafeNameInput.text;

            if (cafeType == "Corporate")
            {
                getCafe.ThisCafeType = MasterCafeSystem.CafeType.Corporate;
            }
            else 
            {
                getCafe.ThisCafeType = MasterCafeSystem.CafeType.Community;
            }

            getCafe.StartingLoan = curLoanAmount;
            getCafe.StartingLoanInterest = curLoanInterest;
            getCafe.LoanEndDate = curLoanDueDate;

            getCafe.LocationName = curLocName;
            getCafe.MinimumWage = curMinWage;
            getCafe.WeatherCondition = curWeatherCon;
            getCafe.PopulationRate = curPopRate;
        }
    }

    private void ManagePages()
    {
        switch (OnPage)
        {
            case 1:
                BrandingPage.SetActive(true);
                LoanPage.SetActive(false);
                LocationsPage.SetActive(false);
                CheckForStoreName();
                break;
            case 2:
                BrandingPage.SetActive(false);
                LoanPage.SetActive(true);
                LocationsPage.SetActive(false);
                CheckForLoanOption();
                break;
            case 3:
                BrandingPage.SetActive(false);
                LoanPage.SetActive(false);
                LocationsPage.SetActive(true);
                CheckForLocationOption();
                break;
        }
    }

    private void CheckForStoreName()
    {
        if (CafeNameInput.text != "" && CafeNameInput.text != null && cafeType != "")
        {
            NextPageButton.interactable = true;
        }
        else
        {
            NextPageButton.interactable = false;
        }
    }
    private void CheckForLoanOption()
    {
        if (curLoanAmount == 0)
        {
            NextPageButton.interactable = false;
        }
        else
        {
            NextPageButton.interactable = true;
        }
    }
    private void CheckForLocationOption()
    {
        NextPageButton.gameObject.SetActive(false);
        if (curMinWage == 0f)
        {
            PlayGame.interactable = false;
        }
        else
        {
            PlayGame.interactable = true;
        }
    }
    private void LoadButtons()
    {
        NextPageButton.onClick.AddListener(NextPage);
        CorporateButton.onClick.AddListener(OnChoseCorporate);
        CommunityButton.onClick.AddListener(OnChoseCommunity);
        LoanOption1.onClick.AddListener(ChoseLoanOne);
        LoanOption2.onClick.AddListener(ChoseLoanTwo);
        LoanOption3.onClick.AddListener(ChoseLoanThree);
        LocationOption1.onClick.AddListener(ChoseLocOne);
        LocationOption2.onClick.AddListener(ChoseLocTwo);
        LocationOption3.onClick.AddListener(ChoseLocThree);
        LocationOption4.onClick.AddListener(ChoseLocFour);
        LocationOption5.onClick.AddListener(ChoseLocFive);
        PlayGame.onClick.AddListener(OnPlayGame);
    }

    private void OnChoseCorporate() 
    {
        cafeType = "Corporate";
    }
    private void OnChoseCommunity()
    {
        cafeType = "Community";
    }

    private void OnPlayGame() 
    {
        PlayerPrefs.SetString("curFilePath", "");
        SceneManager.LoadScene("Gameplay");
    }

    private void ChoseLoanOne() 
    {
        curLoanInterest = 0.0425f;
        curLoanAmount = 30000;
        curLoanDueDate = new TimeSpan(DateTime.Now.TimeOfDay.Days + 700, DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
    }
    private void ChoseLoanTwo()
    {
        curLoanInterest = 0.05f;
        curLoanAmount = 45000;
        curLoanDueDate = new TimeSpan(DateTime.Now.TimeOfDay.Days + 650, DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);
    }
    private void ChoseLoanThree()
    {
        curLoanInterest = 0.065f;
        curLoanAmount = 55000;
        curLoanDueDate = new TimeSpan(DateTime.Now.TimeOfDay.Days + 600, DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes);

    }

    private void ChoseLocOne() 
    {
        curLocName = "Springdale";
        curWeatherCon = 0.30f;
        curPopRate = 0.80f;
        curMinWage = 11.50f;
    }

    private void ChoseLocTwo() 
    {
        curLocName = "New Clara";
        curWeatherCon = 0.70f;
        curPopRate = 0.50f;
        curMinWage = 13.50f;
    }

    private void ChoseLocThree()
    {
        curLocName = "Hillsville";
        curWeatherCon = 0.40f;
        curPopRate = 0.65f;
        curMinWage = 10.50f;
    }
    private void ChoseLocFour()
    {
        curLocName = "Franks Hill";
        curWeatherCon = 0.20f;
        curPopRate = 0.45f;
        curMinWage = 12.50f;
    }
    private void ChoseLocFive()
    {
        curLocName = "Donsburg";
        curWeatherCon = 0.67f;
        curPopRate = 0.70f;
        curMinWage = 13.00f;
    }

    private void NextPage() 
    {
        if (OnPage != 3)
        {
            OnPage++;
        }
    }
    private void LastPage() 
    {
        if (OnPage != 1)
        {
            OnPage--;
        }
    }
}