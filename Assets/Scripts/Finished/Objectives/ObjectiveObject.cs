using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObject : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject Spawner;
    string ObjText;
    int OBJID;
    int OBJEXP;
    int minimum;
    int maximum;
    public int loadedMin;
    bool hasSpawned;
    Status status = Status.New;

    public void LoadStatus(string newStatus) 
    {
        switch (newStatus)
        {
            case "New":
                status = Status.New;
                break;
            case "InProgress":
                status = Status.InProgress;
                break;
            case "Finished":
                status = Status.Finished;
                break;
        }
    }

    public void SetLoadedMin(int newLoaded) 
    { 
        loadedMin = newLoaded;
    }

    public void SetEXP(int newXP) 
    {
        OBJEXP= newXP;
    }

    public GameObject GetSpawner() 
    {
        return Spawner;
    }
    public void SetSpawner(GameObject newSpawner)
    {
        Spawner = newSpawner;
    }
    public void Despawn() 
    {
        Destroy(GetSpawner());
        SetSpawner(null);
        status = Status.Finished;
    }

    public bool GetSpawned() 
    {
        return hasSpawned;
    }

    public void SetSpawned(bool newBool)
    {
        hasSpawned = newBool;
    }

    public int GetMinimum() 
    { 
        return minimum;
    }

    public void SetMinimum(int newMinimum) 
    {
        minimum = newMinimum;
    }

    public int GetMaximum() 
    {
        return maximum;
    }
    public void SetMaximum(int newMax)
    {
        maximum = newMax;
    }

    public string  GetObj()
    {
        return ObjText;
    }
    public void SetObj(string newOBJ) 
    { 
        ObjText = newOBJ; 
    }
    public int GetID() 
    { 
        return OBJID;
    }
    public void SetID(int newID) 
    { 
        OBJID = newID;
    }
    public Status GetStatus() 
    { 
        return status;
    }
    public void SetStatus(Status newStatus) 
    {
        status = newStatus;
    }
    public int GetOBJEXP() 
    {
        return OBJEXP;
    }
}
