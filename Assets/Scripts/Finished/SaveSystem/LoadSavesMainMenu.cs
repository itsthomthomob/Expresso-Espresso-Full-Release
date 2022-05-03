using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadSavesMainMenu : MonoBehaviour
{
    [Header("Interface Controllers")]
    public Button LoadButton;
    public Button CloseSavesButton;
    public Transform SaveModuleContainer;
    public GameObject SavesListGameobject;
    public GameObject SaveModuleTemplate;

    [Header("All Saves")]
    public List<GameObject> AllSavePrefabs = new List<GameObject>();

    [Header("Save Containers")]
    public Dictionary<JsonConfigurationFile, GameObject> AllSaves =
   new Dictionary<JsonConfigurationFile, GameObject>();

    private void Awake()
    {
        SavesListGameobject.SetActive(true);
        InitializeSavePrefabs();
        LoadButton.onClick.AddListener(OpenSavesInterface);
        CloseSavesButton.onClick.AddListener(CloseSaveInterface);
        SavesListGameobject.SetActive(false);
    }

    private void OpenSavesInterface() 
    {
        SavesListGameobject.SetActive(true);
    }

    private void CloseSaveInterface() 
    {
        SavesListGameobject.SetActive(false);
    }

    private void InitializeSavePrefabs()
    {
        foreach (var file in Directory.EnumerateFiles(Application.persistentDataPath, "*.json"))
        {
            Debug.Log("Loading: \n" + file);
            string GetJSON = File.ReadAllText(file);
            JsonConfigurationFile NewSaveData = JsonUtility.FromJson<JsonConfigurationFile>(GetJSON);
            LoadSaveModules(NewSaveData, file);
        }
    }

    private void LoadSaveModules(JsonConfigurationFile newJSON, string file)
    {
        GameObject newModule = Instantiate(SaveModuleTemplate);
        newModule.transform.position = SaveModuleContainer.position;
        newModule.transform.SetParent(SaveModuleContainer);
        for (int i = 0; i < newModule.transform.childCount; i++)
        {
            Transform child = newModule.transform.GetChild(i);
            if (child.name == "SaveName")
            {
                TMP_Text getText = child.GetComponent<TMP_Text>();
                getText.text = newJSON.gameSaveData.SaveName;
            }
            if (child.name == "Date")
            {
                TMP_Text getText = child.GetComponent<TMP_Text>();
                getText.text = newJSON.gameSaveData.SaveDate;
            }
            if (child.name == "SaveImg")
            {
                Image getImage = child.GetComponent<Image>();
                if (File.Exists(file.Replace(".json", ".png")))
                {
                    getImage.sprite = LoadNewSprite(file.Replace(".json", ".png"));
                    Debug.Log("Changed sprite image!");
                }
                else
                {
                    Debug.Log("Picture does not exist: \n" + file.Replace(".json", ".png"));
                }
            }
        }
        SaveReferenceMainMenu getRef = newModule.GetComponent<SaveReferenceMainMenu>();
        getRef.saveFilePath = file;
        AllSavePrefabs.Add(newModule);
    }
    public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Thanks to @Freznosis on Unity Forums
        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }

    public static Texture2D LoadTexture(string FilePath)
    {
        // Thanks to @c68 on Unity Forums
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    private void SortSavePrefabs()
    {
        // Happens when user clicks on "Load Game" button in pause menu

        // Get all dates by reading dictionary's json files
        List<DateTime> dates = new List<DateTime>();
        foreach (KeyValuePair<JsonConfigurationFile, GameObject> entry in AllSaves)
        {
            // Get date of json file
            JsonConfigurationFile file = entry.Key;
            DateTime saveTime = new DateTime();
            if (DateTime.TryParse(file.gameSaveData.ActualDate, out saveTime))
            {
                dates.Add(saveTime);
            }
        }

        // Sort all dates, descending 
        dates.Sort((x, y) => DateTime.Compare(y, x));

        // Match dates to save prefabs, set index
        for (int i = 0; i < dates.Count; i++)
        {
            foreach (KeyValuePair<JsonConfigurationFile, GameObject> entry in AllSaves)
            {
                JsonConfigurationFile file = entry.Key;
                if (file.gameSaveData.ActualDate == dates[i].ToString())
                {
                    // Found date, get index, set
                    int index = dates.IndexOf(dates[i]);
                    entry.Value.transform.SetSiblingIndex(index);
                }
            }
        }
        Debug.Log("Sorted saves.");
    }

}
