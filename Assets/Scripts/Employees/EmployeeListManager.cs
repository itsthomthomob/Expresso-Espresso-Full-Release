//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class EmployeeListManager : MonoBehaviour
//{
//    [Header("UI Objects")]
//    public GameObject EmployeeInfoPrefab;
//    public GameObject EmployeeList;

//    [Header("Employee Management Stats")]
//    //public EntityEmployee[] EmployeesArray; // new employees go here
//    //public List<EntityEmployee> EmployeesListFromArray;
//    //public List<EntityEmployee> GeneratedEmployeesList; // generated employees here
//    public int TotalEmployees;

//    private void Update()
//    {
//        //EmployeesArray = FindObjectsOfType<EntityBarista>();

//        GetEmployees();
//        GenerateNewEmployees();
//    }

//    private void GetEmployees() 
//    {
//        for (int i = 0; i < EmployeesArray.Length; i++)
//        {
//            if (!EmployeesListFromArray.Contains(EmployeesArray[i]))
//            {
//                EmployeesListFromArray.Add(EmployeesArray[i]);
//            }
//        }
//    }

//    private void GenerateNewEmployees() 
//    {
//        for (int i = 0; i < EmployeesListFromArray.Count; i++)
//        {
//            if (!GeneratedEmployeesList.Contains(EmployeesListFromArray[i]))
//            {
//                GameObject newEmployeeCell = Instantiate(EmployeeInfoPrefab);
//                newEmployeeCell.transform.SetParent(EmployeeList.transform);
//                ModifyEmployeeCellContent(newEmployeeCell, EmployeesListFromArray[i]);
//            }
//        }
//    }
//    private void ModifyEmployeeCellContent(GameObject EmployeeCell, EntityEmployee Employee) 
//    {
//        int childCount = EmployeeCell.transform.childCount;
//        for (int i = 0; i < childCount; i++)
//        {
//            var getChild = EmployeeCell.transform.GetChild(i);
//            TMP_Text getText = getChild.GetComponent<TMP_Text>();
//            if (getChild.name == "Character-Sprite")
//            {
//                Debug.Log(Employee.GetSpriteName());
//                getChild.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + Employee.GetSpriteName());
//            }
//            if (getChild.name == "Name")
//            {
//                getText.text = Employee.GetEmployeeName();
//            }
//            if (getChild.name == "Role")
//            {
//                getText.text = Employee.GetEmployeeRole().ToString();
//            }
//            if (getChild.name == "Trait")
//            {
//                getText.text = Employee.GetEmployeePersonality().ToString();
//            }
//            if (getChild.name == "Wage")
//            {
//                getText.text = Employee.GetWageAmount().ToString();
//            }
//            if (getChild.name == "Skill")
//            {
//                getText.text = Employee.GetSkillModifier().ToString();
//            }
//            if (getChild.name == "Efficiency")
//            {
//                getText.text = Employee.GetEfficiencyModifier().ToString();
//            }
//        }
//        GeneratedEmployeesList.Add(Employee);
//    }
//}