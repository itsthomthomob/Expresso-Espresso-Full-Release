using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManagementSystem : MonoBehaviour
{
    [Header("Employee Management")]
    public List<EntityEmployee> Employees;
    public int TotalEmployees;
    public List<float> payrates;

    private void Start()
    {
        EntityEmployee[] currentEmployees = FindObjectsOfType<EntityEmployee>();
        if (currentEmployees.Length > 0)
        {
            for (int i = 0; i < currentEmployees.Length; i++)
            {
                if (!Employees.Contains(currentEmployees[i]))
                {
                    Employees.Add(currentEmployees[i]);
                }
            }
        }
    }
}