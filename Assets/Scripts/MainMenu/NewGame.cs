using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public Button B_newGame;
    public Button exit;

    private void Start()
    {
        B_newGame.onClick.AddListener(LoadNewGame);
        exit.onClick.AddListener(ExitGame);
    }

    void LoadNewGame() 
    {
        SceneManager.LoadScene("Gameplay");
    }
    void ExitGame()
    {
        Application.Quit();
    }

}
