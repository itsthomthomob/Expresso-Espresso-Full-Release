using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveGameSystem : MonoBehaviour
{
    [Header("Save Controllers")]
    public Button SaveGame;
    public Button LoadGame;
    public GameObject LoadObjectsContrainer;
    public GameObject ObjectivePrefab;

    [Header("Game Controllers")]
    public CafeEconomySystem GetEconomy;
    public TimeManager GetTime;
    public MenuManagementSystem GetMenu;
    public StoreLevelManager GetStoreLevel;
    public ObjectivesManager GetObjectives;
    public Grid GameGrid;


    private void Start()
    {
        LoadButtons();
    }

    private void LoadButtons()
    {
        SaveGame.onClick.AddListener(SaveGameData);
        LoadGame.onClick.AddListener(LoadGameData);
    }

    private void SaveGameData()
    {
        JsonConfigurationFile NewSaveData = new JsonConfigurationFile();

        // Save entity data
        NewSaveData.entityData = new EntityData();
        EntityBase[] AllEntities = FindObjectsOfType<EntityBase>();
        Debug.Log("AllEntities Saved: " + AllEntities.Length);
        List<EntityElement> Entities = new List<EntityElement>();

        foreach (EntityBase entity in AllEntities) 
        { 
            EntityElement entityElement = new EntityElement();
            entityElement.json = entity.OnSerialize();
            entityElement.type = entity.Name;
            entityElement.position = entity.Position;
            Entities.Add(entityElement);
        }
        Debug.Log("EntityElements: " + Entities.Count);
        NewSaveData.entityData.entities = new EntityElement[Entities.Count];
        for (int i = 0; i < Entities.Count; i++)
        {
            NewSaveData.entityData.entities[i] = Entities[i];
        }

        // Get time data
        NewSaveData.TimeData = new TimeManagerData();
        NewSaveData.TimeData.GameTimeDataString = GetTime.GameTime.ToString();

        // Get economy data
        NewSaveData.EconomyData = new CafeEconomyData();
        NewSaveData.EconomyData.CurrentProfitsData = GetEconomy.CurrentProfits;
        NewSaveData.EconomyData.CurrentExpensesData = GetEconomy.CurrentExpenses;
        NewSaveData.EconomyData.CurrentRevenueData = GetEconomy.CurrentRevenue;
        NewSaveData.EconomyData.CurrentReviewsData = GetEconomy.CurrentReviews;
        NewSaveData.EconomyData.PositiveReviewsData = GetEconomy.PositiveReviews;
        NewSaveData.EconomyData.NegativeReviewsData = GetEconomy.NegativeReviews;

        // Get menu data
        NewSaveData.ItemsData = new MenuItemsData();
        NewSaveData.ItemsData.priceData = GetMenu.GetMenuItemsPriceData();
        Debug.Log("Got price data: " + GetMenu.GetMenuItemsPriceData().ToString());
        NewSaveData.ItemsData.expenseData = GetMenu.GetMenuItemsExpenseData();
        NewSaveData.ItemsData.menuIDData = GetMenu.GetMenuItemIDsData();
        NewSaveData.ItemsData.itemNameData = GetMenu.GetMenuItemsNamesData();
        NewSaveData.ItemsData.DrinkTypeData = GetMenu.GetMenuItemsTypeData();
        NewSaveData.ItemsData.ActiveData = GetMenu.GetMenuItemsActiveData();
        NewSaveData.ItemsData.SpawnObjectData = GetMenu.GetSpawnObjectsData();
        NewSaveData.ItemsData.TotalMenuItems = GetMenu.GetTotalMenuItems();

        // Get Objectives data
        NewSaveData.ObjData = new ObjectivesData();
        NewSaveData.ObjData.ObjectiveStatesData = new string[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.ObjectiveIDData = new int[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.ObjectiveMinData = new int[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.SpawnerData = new string[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.ObjTextData = new string[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.OBJEXPData = new int[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.maximumData = new int[GetObjectives.AllObjectives.Count];
        NewSaveData.ObjData.hasSpawnedData = new bool[GetObjectives.AllObjectives.Count];

        for (int i = 0; i < GetObjectives.AllObjectives.Count; i++)
        {
            NewSaveData.ObjData.ObjectiveStatesData[i] = GetObjectives.AllObjectives[i].GetStatus().ToString();
            NewSaveData.ObjData.ObjectiveIDData[i] = GetObjectives.AllObjectives[i].GetID();
            NewSaveData.ObjData.ObjectiveMinData[i] = GetObjectives.AllObjectives[i].GetMinimum();

            if (GetObjectives.AllObjectives[i].GetSpawner() != null)
            {

                NewSaveData.ObjData.SpawnerData[i] = GetObjectives.AllObjectives[i].GetSpawner().name;

            }

            NewSaveData.ObjData.ObjTextData[i] = GetObjectives.AllObjectives[i].GetObj();
            NewSaveData.ObjData.maximumData[i] = GetObjectives.AllObjectives[i].GetMaximum();
            NewSaveData.ObjData.hasSpawnedData[i] = GetObjectives.AllObjectives[i].GetSpawned();

        }
        GameObject GetAllObjects = GameObject.Find("AllObjectives");
        Component[] AllComponents = GetAllObjects.GetComponents(typeof(Component));
        int count = 0;
        for (int i = 0; i < AllComponents.Length; i++)
        {
            if (AllComponents[i] is ObjectiveObject)
            {
                count++; // AllComponents would include Transform, needs only ObjectiveObject
            }
        }
        NewSaveData.ObjData.TotalObjectives = count;

        // save store data
        NewSaveData.StoreData = new StoreLevelData();
        NewSaveData.StoreData.CurrentLevelData = GetStoreLevel.StoreLevel;
        NewSaveData.StoreData.CurrentEXPData = GetStoreLevel.CurrentEXP;

        string json = JsonUtility.ToJson(NewSaveData);

        File.WriteAllText(Application.persistentDataPath + "/" + "SaveData" + ".json", json);

        Debug.Log(json);
    }

    private void LoadGameData()
    {
        string GetJSON = File.ReadAllText(Application.persistentDataPath + "/" + "SaveData" + ".json");
        JsonConfigurationFile NewSaveData = JsonUtility.FromJson<JsonConfigurationFile>(GetJSON);

        // Load time data
        TimeManagerData LoadTime = NewSaveData.TimeData;
        GetTime.GameTime = System.TimeSpan.Parse(LoadTime.GameTimeDataString);

        // Load economy data
        CafeEconomyData LoadEco = NewSaveData.EconomyData;
        GetEconomy.CurrentProfits = LoadEco.CurrentProfitsData;
        GetEconomy.CurrentExpenses = LoadEco.CurrentExpensesData;
        GetEconomy.CurrentRevenue = LoadEco.CurrentRevenueData;
        GetEconomy.PositiveReviews = LoadEco.PositiveReviewsData;
        GetEconomy.NegativeReviews = LoadEco.NegativeReviewsData;
        GetEconomy.CurrentReviews = LoadEco.CurrentReviewsData;

        // Load menu items
        MenuItemsData LoadMenu = NewSaveData.ItemsData;
        float[] GetAllPrices = LoadMenu.priceData;
        float[] GetAllExpenses = LoadMenu.expenseData;
        int[] GetAllMenuIDs = LoadMenu.menuIDData;
        bool[] GetAllActiveData = LoadMenu.ActiveData;
        string[] GetAllNames = LoadMenu.itemNameData;
        string[] GetAllTypes = LoadMenu.DrinkTypeData;
        string[] GetAllObjects = LoadMenu.SpawnObjectData;


        int NewItemsCount = LoadMenu.TotalMenuItems;
        List<MenuItem> LoadedMenuItems = new List<MenuItem>();
        GetMenu.AllMenuItems = new MenuItem[NewItemsCount];
        GetMenu.MenuItems = new List<MenuItem>();

        for (int i = 0; i < NewItemsCount; i++)
        {
            // Initialize AllMenuItems
            MenuItem newItem = LoadObjectsContrainer.AddComponent<MenuItem>();

            if (newItem != null)
            {
                newItem.SetPrice(GetAllPrices[i]);
                newItem.SetExpense(GetAllExpenses[i]);
                newItem.SetID(GetAllMenuIDs[i]);
                newItem.SetIsActive(GetAllActiveData[i]);
                newItem.SetName(GetAllNames[i]);
                newItem.LoadDrinkType(GetAllTypes[i]);
                newItem.LoadMyObject(GetAllObjects[i]);
            }
            else
            {
                Debug.Log("NewItem is Null");
            }
            LoadedMenuItems.Add(newItem);
        }

        for (int i = 0; i < LoadedMenuItems.Count; i++)
        {
            if (!GetMenu.MenuItems.Contains(LoadedMenuItems[i]))
            {
                GetMenu.MenuItems.Add(LoadedMenuItems[i]);
            }
        }

        // Load Objectives Data
        ObjectivesData LoadOBJs = NewSaveData.ObjData;
        GetObjectives.UnfinishedObjectives = new List<ObjectiveObject>();
        GetObjectives.NewObjectives = new List<ObjectiveObject>();
        GetObjectives.FinishedObjectives = new List<ObjectiveObject>();

        GameObject GetAllObjectsContainer = GameObject.Find("AllObjectives");
        Component[] AllComponents = GetAllObjectsContainer.GetComponents(typeof(ObjectiveObject));
        List<ObjectiveObject> AllObjs = new List<ObjectiveObject>();

        foreach (ObjectiveObject obj in AllComponents)
        {
            AllObjs.Add(obj);
            Debug.Log("Obtained: " + obj.GetID() + " to set data.");
        }

        for (int i = 0; i < AllObjs.Count; i++)
        {
            // Set objective data to match loaded data
            SetObjectiveData(LoadOBJs, AllObjs[i], i);
        }

        // Load store level data
        StoreLevelData GetSData = NewSaveData.StoreData;
        GetStoreLevel.StoreLevel = GetSData.CurrentLevelData;
        GetStoreLevel.CurrentEXP = GetSData.CurrentEXPData;
    }

    private void SetObjectiveData(ObjectivesData GetData, ObjectiveObject SetData, int index) 
    {
        SetData.LoadStatus(GetData.ObjectiveStatesData[index]);
        SetData.SetLoadedMin(GetData.ObjectiveMinData[index]);
        SetData.SetMaximum(GetData.maximumData[index]);
    }


}
