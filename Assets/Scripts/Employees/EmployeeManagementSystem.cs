using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManagementSystem : MonoBehaviour
{
    [Header("Employee Management")]
    public List<EntitySupport> SupportEmployees;
    public List<EntityBarista> BaristaEmployees;
    public List<EntityFront> FrontEmployees;
    public int TotalEmployees;
    public List<float> payrates;

    private void Start()
    {
        EntitySupport[] currentSupports = FindObjectsOfType<EntitySupport>();
        EntityBarista[] currentBaristas = FindObjectsOfType<EntityBarista>();
        EntityFront[] currentFronts = FindObjectsOfType<EntityFront>();

        if (currentSupports.Length > 0)
        {
            for (int i = 0; i < currentSupports.Length; i++)
            {
                if (!SupportEmployees.Contains(currentSupports[i]))
                {
                  SupportEmployees.Add(currentSupports[i]);
                }
            }
        }
        if (currentBaristas.Length > 0)
        {
            for (int i = 0; i < currentBaristas.Length; i++)
            {
                if (!BaristaEmployees.Contains(currentBaristas[i]))
                {
                    BaristaEmployees.Add(currentBaristas[i]);
                }
            }
        }
        if (currentFronts.Length > 0)
        {
            for (int i = 0; i < currentFronts.Length; i++)
            {
                if (!FrontEmployees.Contains(currentFronts[i]))
                {
                    FrontEmployees.Add(currentFronts[i]);
                }
            }
        }
    }
}