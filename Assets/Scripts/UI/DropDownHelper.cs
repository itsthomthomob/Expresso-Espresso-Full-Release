using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropDownHelper : MonoBehaviour
{
    public Dropdown dropDown;
    public Sprite[] sprites;

    private void Start()
    {
        dropDown.ClearOptions();

        List<Dropdown.OptionData> tileItems = new List<Dropdown.OptionData>();

        foreach (var item in sprites)
        {
            var tileOption = new Dropdown.OptionData(item);
            if (!tileItems.Contains(tileOption))
            {
                tileItems.Add(tileOption);
            }
        }

        dropDown.AddOptions(tileItems);
    }
}
