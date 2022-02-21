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
        FindObjects();
    }

    private void FindObjects() 
    {
        root = GameObject.Find("Grid").GetComponent<RectTransform>();
        Grid = FindObjectOfType<EntityGrid>();
        IM = FindObjectOfType<InspectorMaster>();

        InspectorUI = IM.InspectorUI;
        MachineName = IM.MachineName;
        MachineLevel = IM.MachineLevel;
        InspectButton = IM.InspectMachine_B;

        RoasteryHUD = IM.RoasteryUI;
        BrewerHUD = IM.BrewerUI;
        EspressoHUD = IM.EspressoUI;
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
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2Int gridPoint = Vector2Int.RoundToInt(new Vector2(localPoint.x / root.sizeDelta.x + root.pivot.x, localPoint.y / root.sizeDelta.y + root.pivot.y));
                currentEntity = Grid.GetLastEntity<EntityBase>(gridPoint);
            }
            string EntityName = currentEntity.Name;
            switch (EntityName)
            {
                case "Brewer":
                    EntityBrewingMachineOne entity = gameObject.GetComponent<EntityBrewingMachineOne>();
                    MachineName.text = "Brewer";
                    MachineLevel.text = "";
                    InspectButton.onClick.RemoveAllListeners();
                    InspectButton.onClick.AddListener(SetBrewer);
                    InspectButton.onClick.AddListener(CloseIM);
                    break;
                case "Espresso-Machine":
                    EntityEspressoMachineOne entity1 = gameObject.GetComponent<EntityEspressoMachineOne>();
                    MachineName.text = "Espresso";
                    MachineLevel.text = "";
                    InspectButton.onClick.RemoveAllListeners();
                    InspectButton.onClick.AddListener(SetEspresso);
                    InspectButton.onClick.AddListener(CloseIM);
                    break;
                case "RoasterLvl1":
                    EntityRoasteryMachineOne entity2 = gameObject.GetComponent<EntityRoasteryMachineOne>();
                    MachineName.text = "Roastery";
                    MachineLevel.text = "";
                    InspectButton.onClick.RemoveAllListeners();
                    InspectButton.onClick.AddListener(SetRoaster);
                    InspectButton.onClick.AddListener(CloseIM);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetRoaster() 
    {
        RoasteryHUD.SetActive(true);
    }
    private void SetEspresso()
    {
        EspressoHUD.SetActive(true);

    }
    private void SetGrinder()
    {

    }
    private void SetBrewer()
    {
        BrewerHUD.SetActive(true);

    }
    private void CloseIM() { IM.InspectorUI.SetActive(false); }
}
