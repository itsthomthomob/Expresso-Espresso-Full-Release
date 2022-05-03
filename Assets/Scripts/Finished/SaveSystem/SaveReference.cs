using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveReference : MonoBehaviour
{
    public string saveFilePath;
    public Image GetImage;

    private void Update()
    {
        if (GetImage.sprite == null) 
        { 
            LoadSaveImage();
        }
    }

    public void LoadGameData() 
    {
        SaveGameSystem saveGameSystem = FindObjectOfType<SaveGameSystem>();
        Debug.Log("Button Clicked, Loading: \n" + saveFilePath);
        PlayerPrefs.SetString("curFilePath", saveFilePath);
        saveGameSystem.LoadGameData(saveFilePath);
    }

    private void LoadSaveImage() 
    {
        if (saveFilePath != "" || saveFilePath != null)
        {
            if (File.Exists(saveFilePath.Replace(".json", ".png")))
            {
                Sprite newSprite = LoadNewSprite(saveFilePath.Replace(".json",".png"));
                GetImage.sprite = newSprite;
            }
        }
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

}
