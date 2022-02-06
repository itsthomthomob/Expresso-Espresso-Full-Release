using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject ghost;
    public ConstructionSystemUI tile;
    public Button resume;
    public Button exit;
    public bool isPaused;
    public MasterUIController GetUI;

    private void Start()
    {
        LoadStates();
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
        }

        if (isPaused)
        {
            ghost.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            if (tile.currentTile != ConstructionSystemUI.SelectedTile.none)
            {
                ghost.SetActive(true);
            }

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

    public void resumeGame() { isPaused = false; }

}
