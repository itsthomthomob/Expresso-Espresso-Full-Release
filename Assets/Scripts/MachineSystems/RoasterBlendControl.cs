using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RoasterBlendControl : MonoBehaviour
{
    [Header("UI Control Objects")]
    public GameObject BlendUI;
    public Button NewBlend;
    public Button SaveBlend;
    public Button CloseBlend;
    public Button ClearBlendButton;
    public Slider RoasterTemperature;

    [Header("UI Inputs")]
    public TMP_InputField NameInput;
    public Slider TempInput;
    public TMP_Text TempText;
    public TMP_Text ErrorText;

    [Header("Blend Slots")]
    public GameObject[] Slots;
    public GameObject BlendPrefab;
    public GameObject selectedBlend;

    private void Start()
    {
        SetButtons();
    }

    private void Update()
    {
        GetSelectedBlend();
        UpdateTempText();
    }

    private void UpdateTempText() 
    {
        TempText.text = TempInput.value.ToString();
    }

    private void SetButtons() 
    {
        SaveBlend.onClick.AddListener(CreateNewBlend);

        NewBlend.onClick.AddListener(OpenBlendUI);
        CloseBlend.onClick.AddListener(CloseBlendUI);
        ClearBlendButton.onClick.AddListener(ClearBlend);
    }

    private void GetSelectedBlend() 
    {
        EventSystem currentEventSystem = EventSystem.current;
        if (currentEventSystem.currentSelectedGameObject != null)
        {
            if (currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_1" ||
                currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_2" ||
                currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_3" ||
                currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_4" ||
                currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_5" ||
                currentEventSystem.currentSelectedGameObject.gameObject.name == "Blend_6")
            {
                selectedBlend = currentEventSystem.currentSelectedGameObject;
            }
        }
    }

    private void ClearBlend() 
    {
        if (selectedBlend.transform.childCount == 1)
        {
            Destroy(selectedBlend.transform.GetChild(0).gameObject);
        }
    }

    private void CreateNewBlend() 
    {
        if (Slots[0].transform.childCount == 1 &&
            Slots[1].transform.childCount == 1 &&
            Slots[2].transform.childCount == 1 &&
            Slots[3].transform.childCount == 1 &&
            Slots[4].transform.childCount == 1 &&
            Slots[5].transform.childCount == 1)
        {
            ErrorText.text = "All blend slots are full!";
            return;
        }
        else 
        { 
            ErrorText.text = "";
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].transform.childCount == 0)
                {
                    GameObject newBlend = Instantiate(BlendPrefab);
                    newBlend.transform.SetParent(Slots[i].transform);
                    newBlend.transform.position = Slots[i].transform.position;

                    TMP_Text newBlendName = newBlend.transform.GetChild(0).GetComponent<TMP_Text>();
                    newBlendName.text = NameInput.text;

                    TMP_Text newTempText = newBlend.transform.GetChild(2).GetComponent<TMP_Text>();
                    newTempText.text = TempInput.value.ToString();
                    break;
                }
            }
        }
    }

    private void OpenBlendUI() 
    {
        BlendUI.SetActive(true);
    }

    private void CloseBlendUI() 
    {
        BlendUI.SetActive(false);
    }
}
