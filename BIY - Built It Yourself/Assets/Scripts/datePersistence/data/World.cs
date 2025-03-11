using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class World
{
    public string worldName;
    public string worldID;
    public string creationDate;
}

[System.Serializable]
public class WorldList
{
    public List<World> worlds = new List<World>();
}