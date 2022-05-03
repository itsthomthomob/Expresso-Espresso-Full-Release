using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveReferenceMainMenu : MonoBehaviour
{
    public string saveFilePath;

    public void LoadGameData()
    {
        SceneManager.LoadScene("Gameplay");
        PlayerPrefs.SetString("curFilePath", saveFilePath);
    }
}