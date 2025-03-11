using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WorldSettings : MonoBehaviour
{
    public static string name { get; set; }
    public static int width { get; set; }
    public static int height { get; set; }
    public static string id { get; set; }
    public static bool loader { get; set; }

    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private TMP_InputField text;

    public void CreatWorld()
    {
      

        id = Guid.NewGuid().ToString();
        Debug.Log($"Name : {name} - Widht : {width} - Height {height} - id : {id}");
        SceneManager.LoadScene("BIY- WORLD");
       WorldListManagement.worldListManagement.CreateNewWorld(name,id);
        loader = false;
    }

    public void SetName()
    {
        
        name = text.text;
        Debug.Log(text.text);
    }

    public void GetDropDownValue()
    {
        int index = dropDown.value;

        string selectOption = dropDown.options[index].text;

        switch (selectOption)
        {
            case "Petit":
                width = 50;
                height = 50;
                break;
            case "Moyen":
                width = 70;
                height = 70;
                break;
            case "Grand":
                width = 100;
                height = 100;
                break;
            default:
                width = 50;
                height = 50;
             break;
        }

    }

}
