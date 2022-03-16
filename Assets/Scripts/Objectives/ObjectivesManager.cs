using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectivesManager : MonoBehaviour
{
    [Header("Canvas Objects")]
    public GameObject[] Containers;
    public GameObject ObjUIPrefab;

    [Header("Objective Objects")]
    public GameObject AllObjectivesGO;
    public GameObject UnfinishedObjsGO;
    public GameObject FinishedObjsGO;
    public GameObject NewObjsGO;

    List<ObjectiveObject> AllObjectives = new List<ObjectiveObject>();

    List<ObjectiveObject> UnfinishedObjectives = new List<ObjectiveObject>();
    List<ObjectiveObject> NewObjectives = new List<ObjectiveObject>();
    List<ObjectiveObject> FinishedObjectives = new List<ObjectiveObject>();

    private void Awake()
    {
        Component[] LoadObjectives = AllObjectivesGO.GetComponents(typeof(ObjectiveObject));

        for (int i = 0; i < LoadObjectives.Length; i++)
        {
            if (LoadObjectives[i] is ObjectiveObject)
            {
                AllObjectives.Add(LoadObjectives[i] as ObjectiveObject);
            }
        }

        Debug.Log("Registered: " + AllObjectives.Count + " Objectives");

        for (int i = 0; i < AllObjectives.Count; i++) 
        {
            Debug.Log(AllObjectives[i].GetObj());
        }
    }

    private void Update()
    {
        ManageUI();
        UpdateUI();
        ManageObjectiveTypes();
    }

    private void UpdateUI() 
    {
        for (int i = 0; i < AllObjectives.Count; i++)
        {
            if (AllObjectives[i].GetSpawner() != null)
            {
                if (AllObjectives[i].GetStatus() == Status.New ||
                    AllObjectives[i].GetStatus() == Status.InProgress
                    )
                {
                    GameObject GetSpawner = AllObjectives[i].GetSpawner();
                    for (int j = 0; j < GetSpawner.transform.childCount; j++)
                    {
                        Transform GetChild = GetSpawner.transform.GetChild(j);
                        if (GetChild.name == "Obj-Min") 
                        { 
                            TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                            GetText.text = AllObjectives[i].GetMinimum().ToString();
                        }
                    }
                }
            }
        }
    }

    private void ManageUI() 
    {
        if (NewObjectives.Count > 0)
        {
            if (Containers[0].transform.childCount == 0)
            {
                SpawnNewObj(Containers[0].transform);
            }
            if (Containers[1].transform.childCount == 0)
            {
                SpawnNewObj(Containers[1].transform);
            }
            if (Containers[2].transform.childCount == 0)
            {
                SpawnNewObj(Containers[2].transform);
            }
            if (Containers[3].transform.childCount == 0)
            {
                SpawnNewObj(Containers[3].transform);
            }
        }
    }

    private void SpawnNewObj(Transform spawner)
    {
        if (NewObjectives.Count > 0) 
        {
            for (int i = 0; i < NewObjectives.Count; i++)
            {
                if (NewObjectives[i].GetSpawned() == false)
                {
                    ObjectiveObject OBJ = NewObjectives[i];
                    GameObject newObjective = Instantiate(ObjUIPrefab);
                    newObjective.transform.position = spawner.position;
                    newObjective.transform.SetParent(spawner.transform);
                    Debug.Log(spawner.childCount);
                    OBJ.SetSpawner(newObjective);
                    for (int j = 0; j < newObjective.transform.childCount; j++)
                    {
                        Transform GetChild = newObjective.transform.GetChild(j);
                        if (GetChild.name == "Obj-Text")
                        {
                            TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                            GetText.text = "� " + OBJ.GetObj();
                        }
                        else if (GetChild.name == "Obj-Min")
                        {
                            TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                            GetText.text = OBJ.GetMinimum().ToString();
                        }
                        else if (GetChild.name == "Obj-Max")
                        {
                            TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                            GetText.text = OBJ.GetMaximum().ToString();

                        }
                    }
                    // Check types
                    if (OBJ is Objective001)
                    {
                        NewObjsGO.AddComponent<Objective001>();
                    }
                    if (OBJ is Objective002)
                    {
                        NewObjsGO.AddComponent<Objective002>();
                    }
                    if (OBJ is Objective003)
                    {
                        NewObjsGO.AddComponent<Objective003>();
                    }
                    if (OBJ is Objective004)
                    {
                        NewObjsGO.AddComponent<Objective004>();
                    }
                    OBJ.SetSpawned(true);
                    return;
                }
            }
        }
    }

    private void ManageObjectiveTypes() 
    {
        for (int i = 0; i < AllObjectives.Count; i++)
        {
            if (AllObjectives[i].GetStatus() == Status.New)
            {
                NewObjectives.Add(AllObjectives[i]);
            }
            else if (AllObjectives[i].GetStatus() == Status.Finished)
            {
                if (NewObjectives.Contains(AllObjectives[i]))
                {
                    NewObjectives.Remove(AllObjectives[i]);
                    FinishedObjectives.Add(AllObjectives[i]);
                }
                if (AllObjectives[i] is Objective001)
                {
                    Objective001 GetOBJ = AllObjectives[i] as Objective001;
                    if (GetOBJ.GetSpawner() != null)
                    {
                        GetOBJ.Despawn();
                        Destroy(NewObjsGO.GetComponent<Objective001>());
                        FinishedObjsGO.AddComponent<Objective001>();
                    }
                }
                else if (AllObjectives[i] is Objective002)
                {
                    Objective002 GetOBJ = AllObjectives[i] as Objective002;
                    if (GetOBJ.GetSpawner() != null)
                    {
                        GetOBJ.Despawn();
                        Destroy(NewObjsGO.GetComponent<Objective002>());
                        FinishedObjsGO.AddComponent<Objective002>();
                    }
                }
                else if (AllObjectives[i] is Objective003)
                {
                    Objective003 GetOBJ = AllObjectives[i] as Objective003;
                    if (GetOBJ.GetSpawner() != null)
                    {
                        GetOBJ.Despawn();
                        Destroy(NewObjsGO.GetComponent<Objective003>());
                        FinishedObjsGO.AddComponent<Objective003>();
                    }
                }
                else if (AllObjectives[i] is Objective004)
                {
                    Objective004 GetOBJ = AllObjectives[i] as Objective004;
                    if (GetOBJ.GetSpawner() != null)
                    {
                        GetOBJ.Despawn();
                        Destroy(NewObjsGO.GetComponent<Objective004>());
                        FinishedObjsGO.AddComponent<Objective004>();
                    }
                }
            }
        }
    }

}
