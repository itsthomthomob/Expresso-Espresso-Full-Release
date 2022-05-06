using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseMenuFilter;
    public EntityBase ghost;
    public TileConstruction tile;
    public Button resume;
    public Button exit;
    public bool isPaused;
    public MasterUIController GetUI;
    public TimeManager getTime;
    private void Start()
    {
        LoadStates();
        tile = FindObjectOfType<TileConstruction>();
        ghost = FindObjectOfType<GhostTile>().ghostEntity;
    }

    private void Update()
    {
        if (!GetUI.isActive)
        {
            ManagePause();
        }
    }

    private void ManagePause() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            getTime.Timer.Stop();
        }

        if (isPaused)
        {
            ghost.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
            pauseMenuFilter.SetActive(true);
            getTime.Timer.Stop();
            getTime.scale = 0;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseMenuFilter.SetActive(false);
            getTime.Timer.Start();
            getTime.scale = 1;
            Time.timeScale = 1;
        }
    }
    private void LoadStates()
    {
        isPaused = false;
        resume.onClick.AddListener(resumeGame);
        exit.onClick.AddListener(exitgame);
    }

    private void exitgame()
    {
        Application.Quit();
    }

    public void resumeGame() 
    { 
        isPaused = false; 
    }
}
