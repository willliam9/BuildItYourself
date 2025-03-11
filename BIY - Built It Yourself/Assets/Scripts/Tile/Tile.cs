using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        Ground,
        Water,
        Nature,
        Building,
        Decoration
    }

    public enum TileSubType
    {
        Empty,
        Residential,
        Commercial,
        Industrial,
        Leizure,
        Monument,
        Tourist,
        // Autre bâtiment de d'autres sections principales 
        Supply,
        Service,
    }

    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private int price;
    [SerializeField]
    private TileType type;
    [SerializeField]
    private TileSubType subType;
    [SerializeField]
    private Vector3 position;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }
    public TileType Type { get => type; set => type = value; }
    public TileSubType SubType { get => subType; set => subType = value; }
    public Vector3 Position { get => position; set => position = value; }
    public Vector3 Rot { get; set; }

    public Tile() { }

    public Tile(Vector3 position, Vector3 rot, TileType type, TileSubType subType, string name, string description, int price)
    {
        Position = position;
        Rot = rot;
        Type = type;
        SubType = subType;
        Name = name;
        Description = description;
        Price = price;
    }
}

public class TileBuilding : Tile
{
    public enum Building
    {
        Residential,
        Commercial,
        Industrial,
        Service,
        Central,
    }

    [SerializeField]
    private Building buildingType;
    [SerializeField]
    private int earning;
    [SerializeField]
    private int happiness;
    [SerializeField]
    private int pollution;
    [SerializeField]
    private int trash;
    [SerializeField]
    private int electricity;
    [SerializeField]
    private int water;

    public Building BuildingType { get => buildingType; set => buildingType = value; }
    public int Earning { get => earning; set => earning = value; }
    public int Happiness { get => happiness; set => happiness = value; }
    public int Pollution { get => pollution; set => pollution = value; }
    public int Trash { get => trash; set => trash = value; }
    public int Electricity { get => electricity; set => electricity = value; }
    public int Water { get => water; set => water = value; }

    public TileBuilding() { }

    public TileBuilding(Vector3 position, Vector3 rot, Building buildingType, string name, string description, int price, int earning, int happiness, int pollution, int trash, int electricity, int water)
        : base(position,rot,TileType.Building, TileSubType.Empty, name, description, price)
    {
        BuildingType = buildingType;
        Earning = earning;
        Happiness = happiness;
        Pollution = pollution;
        Trash = trash;
        Electricity = electricity;
        Water = water;
    }
}
