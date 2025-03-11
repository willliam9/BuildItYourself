using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableTile
{
    public string name;
    public string description;
    public int price;
    public Tile.TileType type;
    public Tile.TileSubType subType;
    public Vector3 position;
    public Vector3 rotation;

    public SerializableTile(Tile tile)
    {
        this.name = tile.Name;
        this.description = tile.Description;
        this.price = tile.Price;
        this.type = tile.Type;
        this.subType = tile.SubType;
        this.position = tile.Position;
        this.rotation = tile.Rot;

    }
}