using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InspectorHelper : MonoBehaviour
{
    [Header("UI Objects")]
    public RectTransform root;
    public EntityGrid Grid;

    GameObject InspectorUI;
    TMP_Text MachineName;
    TMP_Text MachineLevel;
    Button InspectButton;

    [Header("HUDs")]
    GameObject BrewerHUD;
    GameObject EspressoHUD;
    GameObject RoasteryHUD;

    [Header("Master")]
    InspectorMaster IM;

    private void Start()
    {
        root = GameObject.Find("Grid").GetComponent<RectTransform>();
        Grid = FindObjectOfType<EntityGrid>();
        IM = FindObjectOfType<InspectorMaster>();
        InspectorUI = IM.InspectorUI;
        MachineName = IM.MachineName;
        MachineLevel = IM.MachineLevel;
    }

    private void Update()
    {
        CheckMousePosition();
    }

    public void CheckMousePosition() 
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
        {
            Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));

            if (Grid.GetLastEntity<EntityBase>(gridPoint) is EntityBrewingMachineOne)
            {
                MouseIsOver();
            }
            if (Grid.GetLastEntity<EntityBase>(gridPoint) is EntityEspressoMachineOne)
            {
                MouseIsOver();
            }
            if (Grid.GetLastEntity<EntityBase>(gridPoint) is EntityRoasteryMachineOne)
            {
                MouseIsOver();
            }
        }
    }

    public void MouseIsOver()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            InspectorUI.SetActive(true);
            EntityBase currentEntity = gameObject.GetComponent<EntityBase>();
            string EntityName = currentEntity.GetEntityName();
            switch (EntityName)
            {
                case "Brewer":
                    EntityBrewingMachineOne entity = gameObject.GetComponent<EntityBrewingMachineOne>();
                    MachineName.text = "Brewer";
                    MachineLevel.text = "Level - " + entity.GetLevel().ToString();
                    break;
                case "Espresso-Machine":
                    EntityEspressoMachineOne entity1 = gameObject.GetComponent<EntityEspressoMachineOne>();
                    MachineName.text = "Espresso";
                    MachineLevel.text = "Level - " + entity1.GetLevel().ToString();
                    break;
                case "Roaster":
                    EntityRoasteryMachineOne entity2 = gameObject.GetComponent<EntityRoasteryMachineOne>();
                    MachineName.text = "Roastery";
                    MachineLevel.text = "Level - " + entity2.GetLevel().ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
