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
        if (ghost == null) 
        { 
            ghost = FindObjectOfType<GhostTile>().ghostEntity;
        }
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
            getTime.scale = 0;
            getTime.Timer.Stop();
        }

        if (isPaused)
        {
            //ghost.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
            pauseMenuFilter.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseMenuFilter.SetActive(false);

            if (tile.curTile != TileConstruction.CurrentTileState.None)
            {
                //ghost.gameObject.SetActive(true);
            }
            getTime.Timer.Start();
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
