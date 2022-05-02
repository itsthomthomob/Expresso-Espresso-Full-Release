using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SaveGameSystem : MonoBehaviour
{
    [Header("Save Controllers")]
    public string NewFileName;
    public string ClickedSaveName;
    public Button OpenPresave;
    public Button LoadGame;
    public Button SaveGame;
    public Button CancelPresave;
    public Button CloseList;
    public TMP_InputField SaveTitle;
    public GameObject SaveModuleTemplate;
    public GameObject SaveGameUI;
    public Transform SaveModuleContainer;
    public GameObject ListOfSaves;

    public GameObject LoadObjectsContrainer;

    [Header("Game Controllers")]
    public CafeEconomySystem GetEconomy;
    public TimeManager GetTime;
    public MenuManagementSystem GetMenu;
    public StoreLevelManager GetStoreLevel;
    public ObjectivesManager GetObjectives;
    public EntityGrid GameGrid;
    public PauseManager getPause;
    public MasterCafeSystem getMasterCafe;
    public CustomerSpawnSystem getCustomerSystem;

    [Header("Save Containers")]
    public Dictionary<JsonConfigurationFile, GameObject> AllSaves = 
       new Dictionary<JsonConfigurationFile, GameObject>();
    private void Awake()
    {
        InitializeSavePrefabs();
        if (PlayerPrefs.GetString("curFilePath") != "")
        {
            LoadGameData(PlayerPrefs.GetString("curFilePath"));
        }
    }

    private void Start()
    {
        LoadButtons();
        getMasterCafe = FindObjectOfType<MasterCafeSystem>();
        getCustomerSystem = FindObjectOfType<CustomerSpawnSystem>();
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
        GameObject newModule = Instantiate(SaveModuleTemplate, SaveModuleContainer, false);
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
        SaveReference getRef = newModule.GetComponent<SaveReference>();
        getRef.saveFilePath = file;
        AllSaves.Add(newJSON, newModule); // links json file to save prefab
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

    private void LoadButtons()
    {
        OpenPresave.onClick.AddListener(OnOpenSaveInterface);
        LoadGame.onClick.AddListener(OnLoadButton);
        SaveGame.onClick.AddListener(OnSaveGame);
        CancelPresave.onClick.AddListener(OnCancelPresave);
        CloseList.onClick.AddListener(OnCloseList);
    }

    private void OnLoadButton() 
    {
        ListOfSaves.SetActive(true);
        SortSavePrefabs();
    }
    private void OnCloseList() 
    { 
        ListOfSaves.SetActive(false);
    }

    private void OnOpenSaveInterface() 
    {
        SaveGameUI.SetActive(true);
    }

    private void OnCancelPresave() 
    {
        SaveGameUI.SetActive(false);
    }

    private void OnSaveGame() 
    {
        // Get file path name and json
        if (SaveTitle.text != "" || SaveTitle.text != null)
        {
            if (File.Exists(Application.persistentDataPath + "/" + SaveTitle.text + ".json"))
            {
                string getName = GenerateFileName(Application.persistentDataPath + "/" + SaveTitle.text);
                NewFileName = getName;
                CreateSaveModule(SaveTitle.text, DateTime.UtcNow.ToString("MMM ddd d HH:mm"), NewFileName);
                SaveGameData();
                SaveGameUI.SetActive(false);
            }
            else 
            {
                // No other file name with that title exists, save
                string getName = GenerateFileName(Application.persistentDataPath + "/" + SaveTitle.text);
                NewFileName = getName;
                CreateSaveModule(SaveTitle.text, DateTime.UtcNow.ToString("MMM ddd d HH:mm"), NewFileName);
                SaveGameData();
                SaveGameUI.SetActive(false);
            }
        }
    }

    private string GenerateFileName(string curName) 
    {
        Debug.Log("Calling recursion...");
        if (File.Exists(curName + ".json"))
        {
            Debug.Log("New Name: \n" + curName + "(copy)");
            return GenerateFileName(curName + "(copy)");
        }
        else 
        {
            Debug.Log("File does not exist, returning original: \n" + curName);
            GetScreenshot(curName + ".png");
            return curName;
        }
    }

    private void GetScreenshot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
    }

    private void CreateSaveModule(string titleName, string CurrentTime, string fileName) 
    {
        GameObject newModule = Instantiate(SaveModuleTemplate, SaveModuleContainer, false);
        newModule.transform.position = SaveModuleContainer.position;
        newModule.transform.SetParent(SaveModuleContainer);
        for (int i = 0; i < newModule.transform.childCount; i++)
        {
            Transform child = newModule.transform.GetChild(i);
            if (child.name == "SaveName")
            {
                TMP_Text getText = child.GetComponent<TMP_Text>();
                getText.text = titleName;
            }
            if (child.name == "Date")
            {
                TMP_Text getText = child.GetComponent<TMP_Text>();
                getText.text = CurrentTime;
            }
        }
        SaveReference getRef = newModule.GetComponent<SaveReference>();
        getRef.saveFilePath = NewFileName + ".json";
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

    private void SaveGameData()
    {
        JsonConfigurationFile NewSaveData = new JsonConfigurationFile();

        // Save data information
        NewSaveData.gameSaveData = new GameSaveData();
        NewSaveData.gameSaveData.SaveName = SaveTitle.text;
        NewSaveData.gameSaveData.SaveDate = DateTime.UtcNow.ToString("MMM ddd d HH:mm");
        NewSaveData.gameSaveData.ActualDate = DateTime.UtcNow.ToString();

        // Save cafe data
        NewSaveData.cafeData = new CafeData();
        NewSaveData.cafeData.LoanDueDateData = getMasterCafe.LoanEndDate.ToString();
        NewSaveData.cafeData.tookLoanData = getMasterCafe.tookLoan;
        NewSaveData.cafeData.StartingLoanInterestData = getMasterCafe.StartingLoanInterest;
        NewSaveData.cafeData.PopulationRateData = getMasterCafe.PopulationRate;
        NewSaveData.cafeData.WeatherConditionData = getMasterCafe.WeatherCondition;
        NewSaveData.cafeData.MinimumWageData = getMasterCafe.MinimumWage;
        NewSaveData.cafeData.CafeNameData = getMasterCafe.CafeName;

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

        if (NewFileName != "")
        {
            File.WriteAllText(NewFileName+ ".json", json);
        }
    }

    public void LoadGameData(string filePath)
    {
        if (File.Exists(filePath))
        {
            Debug.Log("Loading File: \n" + filePath);
            string GetJSON = File.ReadAllText(filePath);
            JsonConfigurationFile NewSaveData = JsonUtility.FromJson<JsonConfigurationFile>(GetJSON);

            // Load cafe data
            CafeData getCafeData = NewSaveData.cafeData;
            getMasterCafe.LoanEndDate = System.TimeSpan.Parse(getCafeData.LoanDueDateData);
            getMasterCafe.StartingLoanInterest = getCafeData.StartingLoanInterestData;
            getMasterCafe.tookLoan = getCafeData.tookLoanData;
            getMasterCafe.LocationName = getCafeData.LocationNameData;
            getMasterCafe.WeatherCondition = getCafeData.WeatherConditionData;
            getMasterCafe.PopulationRate = getCafeData.PopulationRateData;
            getMasterCafe.MinimumWage = getCafeData.MinimumWageData;
            getMasterCafe.CafeName = getCafeData.CafeNameData;

            // Reset tile construction script
            TileConstruction GetConstruction = FindObjectOfType<TileConstruction>();
            GetConstruction.SelectedEntities = new EntityBase[0];

            // Load Entities
            List<EntityBase>  AllCurrentEntities = new List<EntityBase>();
            GameGrid = FindObjectOfType<EntityGrid>();
            AllCurrentEntities = GameGrid.GetAllEntities();

            EntityData GetEntityData = NewSaveData.entityData;

            EntityElement[] AllStoredEntities = new EntityElement[GetEntityData.entities.Length];

            for (int i = 0; i < AllStoredEntities.Length; i++)
            {
                // Sets all entity data
                AllStoredEntities[i] = GetEntityData.entities[i];
            }

            for (int i = 0; i < AllCurrentEntities.Count; i++)
            {   
                // Destroys all entities
                GameGrid.Destroy(AllCurrentEntities[i]);
            }

            EmployeeListManager getList = FindObjectOfType<EmployeeListManager>();
            getList.hiredBaristas = new List<EntityBarista>();
            getList.hiredSupports = new List<EntitySupport>();
            getList.hiredFronts = new List<EntityFront>();

            getCustomerSystem.allCustomers = new List<EntityCustomer>();

            for (int i = 0; i < AllStoredEntities.Length; i++)
            {
                EntityBase curEntity = null;
                // Creates all entities, sets values
                switch (AllStoredEntities[i].type)
                {
                    // Terrain
                    case "Grass":
                        curEntity = GameGrid.Create<EntityGrass>(AllStoredEntities[i].position);
                        break;
                    case "Concrete":
                        curEntity = GameGrid.Create<EntityConcrete>(AllStoredEntities[i].position);
                        break;
                    // Floors
                    case "Checkered Floor":
                        curEntity = GameGrid.Create<EntityFloorThree>(AllStoredEntities[i].position);
                        break;
                    case "Rough Pale Wooden Floor":
                        curEntity = GameGrid.Create<EntityFloor>(AllStoredEntities[i].position);
                        break;
                    case "Tile Wooden Floor":
                        curEntity = GameGrid.Create<EntityFloorTwo>(AllStoredEntities[i].position);
                        break;
                    case "Pale Diagnal Floor":
                        curEntity = GameGrid.Create<EntityFloorFour>(AllStoredEntities[i].position);
                        break;
                    case "Warm Wooden Floor":
                        curEntity = GameGrid.Create<EntityFloorFive>(AllStoredEntities[i].position);
                        break;
                    case "Warm-Light Wooden Floor":
                        curEntity = GameGrid.Create<EntityFloorSix>(AllStoredEntities[i].position);
                        break;
                    case "Pale Wooden Floor":
                        curEntity = GameGrid.Create<EntityFloorSeven>(AllStoredEntities[i].position);
                        break;
                    // Walls
                    case "Brick Wall":
                        curEntity = GameGrid.Create<EntityWallBrick>(AllStoredEntities[i].position);
                        break;
                    case "Grey Brick Wall":
                        curEntity = GameGrid.Create<EntityWallGreyBrick>(AllStoredEntities[i].position);
                        break;
                    case "Pale Wall":
                        curEntity = GameGrid.Create<EntityWallPale>(AllStoredEntities[i].position);
                        break;
                    case "Plaster Wall":
                        curEntity = GameGrid.Create<EntityWallPlaster>(AllStoredEntities[i].position);
                        break;
                    // Furniture
                    case "Red Barstool":
                        curEntity = GameGrid.Create<EntityBarstool>(AllStoredEntities[i].position);
                        break;
                    case "Grey Chair":
                        curEntity = GameGrid.Create<EntityChairGrey>(AllStoredEntities[i].position);
                        break;
                    case "Red Chair":
                        curEntity = GameGrid.Create<EntityChairRed>(AllStoredEntities[i].position);
                        break;
                    case "Rough Chair":
                        curEntity = GameGrid.Create<EntityChairRough>(AllStoredEntities[i].position);
                        break;
                    case "Smooth Chair":
                        curEntity = GameGrid.Create<EntityChairSmooth>(AllStoredEntities[i].position);
                        break;
                    case "Grey Counter":
                        curEntity = GameGrid.Create<EntityCounterGrey>(AllStoredEntities[i].position);
                        break;
                    case "Marble Counter":
                        curEntity = GameGrid.Create<EntityCounterMarble>(AllStoredEntities[i].position);
                        break;
                    case "Red Counter":
                        curEntity = GameGrid.Create<EntityCounterRed>(AllStoredEntities[i].position);
                        break;
                    case "White Table":
                        curEntity = GameGrid.Create<EntityTableWhite>(AllStoredEntities[i].position);
                        break;
                    case "Square White Table":
                        curEntity = GameGrid.Create<EntityTableSquareWhite>(AllStoredEntities[i].position);
                        break;
                    case "Square Greyy Table":
                        curEntity = GameGrid.Create<EntityTableSquareGrey>(AllStoredEntities[i].position);
                        break;
                    case "Smooth Table":
                        curEntity = GameGrid.Create<EntityTableSmooth>(AllStoredEntities[i].position);
                        break;
                    case "Rough Table":
                        curEntity = GameGrid.Create<EntityTableRough>(AllStoredEntities[i].position);
                        break;
                    case "Grey Table":
                        curEntity = GameGrid.Create<EntityTableGrey>(AllStoredEntities[i].position);
                        break;
                    // Characters come before machines, machines rely on employees
                    case "Customer":
                        curEntity = GameGrid.Create<EntityCustomer>(AllStoredEntities[i].position);
                        getCustomerSystem.allCustomers.Add(curEntity as EntityCustomer);
                        break;
                    case "Support":
                        curEntity = GameGrid.Create<EntitySupport>(AllStoredEntities[i].position);
                        getList.hiredSupports.Add(curEntity as EntitySupport);
                        getList.OnGameDeserialization(curEntity);
                        break;
                    case "Front":
                        curEntity = GameGrid.Create<EntityFront>(AllStoredEntities[i].position);
                        getList.hiredFronts.Add(curEntity as EntityFront);
                        getList.OnGameDeserialization(curEntity);
                        break;
                    case "Barista":
                        curEntity = GameGrid.Create<EntityBarista>(AllStoredEntities[i].position);
                        getList.hiredBaristas.Add(curEntity as EntityBarista);
                        getList.OnGameDeserialization(curEntity);
                        break;
                    // Machines
                    case "RoasterLvl1":
                        curEntity = GameGrid.Create<EntityRoasteryMachineOne>(AllStoredEntities[i].position);
                        break;
                    case "Register":
                        curEntity = GameGrid.Create<EntityRegister>(AllStoredEntities[i].position);
                        break;
                    case "Espresso-Machine":
                        curEntity = GameGrid.Create<EntityEspressoMachineOne>(AllStoredEntities[i].position);
                        break;
                    case "Brewer":
                        curEntity = GameGrid.Create<EntityBrewingMachineOne>(AllStoredEntities[i].position);
                        break;
                    // Items
                    case "Coffee Cup":
                        curEntity = GameGrid.Create<EntityCoffee>(AllStoredEntities[i].position);
                        break;
                
                }
                if (curEntity != null)
                {
                    curEntity.OnDeserialize(AllStoredEntities[i].json);
                }
            }

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
    }

    private void SetObjectiveData(ObjectivesData GetData, ObjectiveObject SetData, int index) 
    {
        SetData.LoadStatus(GetData.ObjectiveStatesData[index]);
        SetData.SetLoadedMin(GetData.ObjectiveMinData[index]);
        SetData.SetMaximum(GetData.maximumData[index]);
    }
}