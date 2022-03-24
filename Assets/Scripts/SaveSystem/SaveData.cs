using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonConfigurationFile 
{
    public TimeManagerData TimeData;
    public CafeEconomyData EconomyData;
    public MenuItemsData ItemsData;
    public ObjectivesData ObjData;
    public StoreLevelData StoreData;
}

[Serializable]
public class TimeManagerData
{
    public string GameTimeDataString = default(TimeSpan).ToString();
}

[Serializable]
public class CafeEconomyData
{
    public float CurrentRevenueData;
    public float CurrentProfitsData;
    public float CurrentExpensesData;
    public float EmployeeCostsData;
    public int CurrentReviewsData;
    public int PositiveReviewsData;
    public int NegativeReviewsData;
}

[Serializable]
public class MenuItemsData 
{
    public float[] priceData;
    public float[] expenseData;
    public int[] menuIDData;
    public string[] itemNameData;
    public string[] DrinkTypeData;
    public string[] MySectionNameData;
    public bool[] ActiveData;
    public int TotalMenuItems;
    public string[] SpawnObjectData;
}

[Serializable]
public class ObjectivesData
{
    public string[] ObjectiveStatesData;
    public int[] ObjectiveIDData;
    public int[] ObjectiveMinData;
    public int TotalObjectives;
    public string[] SpawnerData;
    public string[] ObjTextData;
    public int[] OBJEXPData;
    public int[] maximumData;
    public bool[] hasSpawnedData;
}

[Serializable]
public class StoreLevelData 
{
    public int CurrentLevelData;
    public int CurrentEXPData;
}
