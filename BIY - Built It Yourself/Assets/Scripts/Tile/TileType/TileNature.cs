using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNature : Tile
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private int price;
    [SerializeField]
    private int happiness;
    [SerializeField]
    private int pollution;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }
    public int Happiness { get => happiness; set => happiness = value; }
    public int Pollution { get => pollution; set => pollution = value; }

    public TileNature() { }

    public TileNature(Vector3 position, Vector3 rot, string name, string description, int price, int happiness, int pollution)
        : base(position,rot, TileType.Nature, TileSubType.Empty, name, description, price)
    {
        Name = name;
        Description = description;
        Price = price;
        Happiness = happiness;
        Pollution = pollution;
    }
}