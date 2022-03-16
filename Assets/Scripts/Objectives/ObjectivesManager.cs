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
        if (NewObjectives.Count < 4)
        {
            for (int i = 0; i < Containers.Length; i++)
            {
                if (Containers[i].transform.childCount == 0)
                {
                    SpawnNewObj(Containers[i].transform);
                    Debug.Log(Containers[i].name);
                    break;
                }
            }
        }
    }

    private void SpawnNewObj(Transform spawner)
    {
        if (NewObjectives.Count > 0) 
        {
            if (NewObjectives[0].GetSpawned() == false)
            {
                ObjectiveObject OBJ = NewObjectives[0];
                GameObject newObjective = Instantiate(ObjUIPrefab);
                newObjective.transform.position = spawner.position;
                newObjective.transform.SetParent(spawner.transform);
                OBJ.SetSpawner(newObjective);
                Debug.Log(OBJ.GetID() + " Spawner: " + OBJ.GetSpawner().name);
                for (int i = 0; i < newObjective.transform.childCount; i++)
                {
                    Transform GetChild = newObjective.transform.GetChild(i);
                    if (GetChild.name == "Obj-Text")
                    {
                        TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                        GetText.text = "• " + OBJ.GetObj();
                        Debug.Log("Changed Objective text");
                    }
                    else if (GetChild.name == "Obj-Min")
                    {
                        TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                        GetText.text = OBJ.GetMinimum().ToString();
                        Debug.Log("Changed Minimum text");

                    }
                    else if (GetChild.name == "Obj-Max")
                    {
                        TMP_Text GetText = GetChild.GetComponent<TMP_Text>();
                        GetText.text = OBJ.GetMaximum().ToString();
                        Debug.Log("Changed Maximum text");

                    }
                }
                // Check types
                if (OBJ is Objective001)
                {
                    NewObjsGO.AddComponent<Objective001>();
                }

                OBJ.SetSpawned(true);
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
            }
        }
    }

}
