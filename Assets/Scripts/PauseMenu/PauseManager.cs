using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resume;
    public Button exit;
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
        resume.onClick.AddListener(resumeGame);
        exit.onClick.AddListener(exitgame);
    }

    private void exitgame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void resumeGame() { isPaused = false; }

}
