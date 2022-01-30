using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public GameObject HourBlockPrefab;
    public int Days = 7;
    public int Hours = 24;
    public float BlockWidth;
    public float BlockHeight;

    private void Start()
    {
        GetBlockData();
        GenerateCalendar();
    }

    private void GetBlockData() 
    {
        RectTransform BlockUI = HourBlockPrefab.GetComponent<RectTransform>();
        BlockWidth = BlockUI.sizeDelta.x;
        BlockHeight = BlockUI.sizeDelta.y;
    }

    void GenerateCalendar() 
    {
        for (int i = 0; i < Days; i++)
        {
            for (int j = 0; j < Hours; j++)
            {
                GameObject NewBlock = Instantiate(HourBlockPrefab);
                HourlyBlock NewHour = NewBlock.GetComponent<HourlyBlock>();
                RectTransform NewUI = NewBlock.GetComponent<RectTransform>();
                NewHour.SetDay(i);
                NewHour.SetHour(j);

                Vector3 currPos = HourBlockPrefab.transform.position;
                NewBlock.transform.position = new Vector3(currPos.x + (BlockWidth * i), currPos.y - (BlockHeight * j));
                 
                NewBlock.transform.SetParent(gameObject.transform);
            }
        }
    }

}
