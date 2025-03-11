using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public string name;
    public string worldID;
    public float elapsedTime;
    public int widht;
    public int height;
    public int money;
    public int hapiness;
    public int pollution;
    public int trash;
    public int electrarity;
    public int water;
    public List<SerializableTile> array;

    public GameData()
    {
        name = null;
        worldID = System.Guid.NewGuid().ToString();
        elapsedTime = 0f;
        widht = 0; height = 0;
        money = 0;
        hapiness = 0;
        pollution = 0;
        trash = 0;
        electrarity = 0;
        water = 0;
        array = new List<SerializableTile>();
    }

}
