using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeListManager : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject EmployeeInfoPrefab;
    public GameObject EmployeeList;

    [Header("Employee Management Stats")]
    public List<EntityBase> Employees = new List<EntityBase>(); // new employees go here
    public List<EntitySupport> hiredSupports = new List<EntitySupport>();
    public List<EntityBarista> hiredBaristas = new List<EntityBarista>();
    public List<EntityFront> hiredFronts = new List<EntityFront>();
    public List<EntityBase> GeneratedEmployeesList; // generated employees here
    public int TotalEmployees;

    private void Update()
    {
        GetEmployees();
        GenerateNewEmployees();
    }

    public void OnGameDeserialization(EntityBase loadedEmployee) 
    {
        // Adds employees back to list
        // Generates prefabs again
        GameObject newEmployeeCell = Instantiate(EmployeeInfoPrefab);
        GeneratedEmployeesList.Add(loadedEmployee);

        newEmployeeCell.transform.SetParent(EmployeeList.transform);
        ModifyEmployeeCellContent(newEmployeeCell, loadedEmployee);
    }

    private void GetEmployees()
    {
        for (int i = 0; i < hiredSupports.Count; i++)
        {
            if (!Employees.Contains(hiredSupports[i]))
            {
                Employees.Add(hiredSupports[i]);
            }
        }
        for (int i = 0; i < hiredBaristas.Count; i++)
        {
            if (!Employees.Contains(hiredBaristas[i]))
            {
                Employees.Add(hiredBaristas[i]);
            }
        }
        for (int i = 0; i < hiredFronts.Count; i++)
        {
            if (!Employees.Contains(hiredFronts[i]))
            {
                Employees.Add(hiredFronts[i]);
            }
        }
    }
    private void GenerateNewEmployees()
    {
        for (int i = 0; i < Employees.Count; i++)
        {
            if (!GeneratedEmployeesList.Contains(Employees[i]))
            {
                GameObject newEmployeeCell = Instantiate(EmployeeInfoPrefab);
                GeneratedEmployeesList.Add(Employees[i]);

                newEmployeeCell.transform.SetParent(EmployeeList.transform);
                ModifyEmployeeCellContent(newEmployeeCell, Employees[i]);
            }
        }
    }
    private void ModifyEmployeeCellContent(GameObject EmployeeCell, EntityBase Employee)
    {
        int childCount = EmployeeCell.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var getChild = EmployeeCell.transform.GetChild(i);
            TMP_Text getText = getChild.GetComponent<TMP_Text>();
            if (Employee is EntitySupport)
            {
                EntitySupport support = Employee as EntitySupport;
                if (getChild.name == "Character-Sprite")
                {
                    Debug.Log(support.GetSpriteName());
                    getChild.GetComponent<Image>().sprite = support.Sprite;
                }
                if (getChild.name == "Name")
                {
                    getText.text = support.GetEmployeeName();
                }
                if (getChild.name == "Role")
                {
                    getText.text = support.GetEmployeeRole().ToString();
                }
                if (getChild.name == "Trait")
                {
                    getText.text = support.GetEmployeePersonality().ToString();
                }
                if (getChild.name == "Wage")
                {
                    getText.text = support.GetWageAmount().ToString();
                }
                if (getChild.name == "Skill")
                {
                    getText.text = support.GetSkillModifier().ToString();
                }
                if (getChild.name == "Efficiency")
                {
                    getText.text = support.GetEfficiencyModifier().ToString();
                }
            }
            if (Employee is EntityBarista)
            {
                EntityBarista barista = Employee as EntityBarista;
                if (getChild.name == "Character-Sprite")
                {
                    //Debug.Log(barista.GetSpriteName());
                    getChild.GetComponent<Image>().sprite = barista.Sprite;
                }
                if (getChild.name == "Name")
                {
                    getText.text = barista.GetEmployeeName();
                }
                if (getChild.name == "Role")
                {
                    getText.text = barista.GetEmployeeRole().ToString();
                }
                if (getChild.name == "Trait")
                {
                    getText.text = barista.GetEmployeePersonality().ToString();

                }
                if (getChild.name == "Wage")
                {
                    getText.text = barista.GetWageAmount().ToString();
                }
                if (getChild.name == "Skill")
                {
                    getText.text = barista.GetSkillModifier().ToString();
                }
                if (getChild.name == "Efficiency")
                {
                    getText.text = barista.GetEfficiencyModifier().ToString();
                }
            }
            if (Employee is EntityFront)
            {
                EntityFront front = Employee as EntityFront;
                if (getChild.name == "Character-Sprite")
                {
                    //Debug.Log(front.GetSpriteName());
                    getChild.GetComponent<Image>().sprite = front.Sprite;
                }
                if (getChild.name == "Name")
                {
                    getText.text = front.GetEmployeeName();
                }
                if (getChild.name == "Role")
                {
                    getText.text = front.GetEmployeeRole().ToString();
                }
                if (getChild.name == "Trait")
                {
                    getText.text = front.GetEmployeePersonality().ToString();
                }
                if (getChild.name == "Wage")
                {
                    getText.text = front.GetWageAmount().ToString();
                }
                if (getChild.name == "Skill")
                {
                    getText.text = front.GetSkillModifier().ToString();
                }
                if (getChild.name == "Efficiency")
                {
                    getText.text = front.GetEfficiencyModifier().ToString();
                }
            }
            
        }
        GeneratedEmployeesList.Add(Employee);
    }
}