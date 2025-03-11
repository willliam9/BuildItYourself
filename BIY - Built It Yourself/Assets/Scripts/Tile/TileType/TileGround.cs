using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGround : Tile
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private int price;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }

    public TileGround() { }

    public TileGround(Vector3 position, Vector3 rot, string name, string description, int price)
        : base(position,rot ,TileType.Ground, TileSubType.Empty, name, description, price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
