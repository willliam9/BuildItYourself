using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CommercialTile : TileBuilding
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private int price;
    [SerializeField]
    private int maxWorker;
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

    public string Name { get => name; set => name = this.gameObject.name ; }
    public string Description { get => description; set => description = value; }
    public int Price { get => price; set => price = value; }
    public int MaxWorker { get => maxWorker; set => maxWorker = value; }
    public int Earning { get => earning; set => earning = value; }
    public int Happiness { get => happiness; set => happiness = value; }
    public int Pollution { get => pollution; set => pollution = value; }
    public int Trash { get => trash; set => trash = value; }
    public int Electricity { get => electricity; set => electricity = value; }
    public int Water { get => water; set => water = value; }

    public CommercialTile() { }

    public CommercialTile(Vector3 position, Vector3 rot, string name, string description, int price, int maxWorker, int earning, int happiness, int pollution, int trash, int electricity, int water)
       : base(position,rot, Building.Commercial, name, description, price, 0, 0, 0, 0, 0, 0)
    {
        Name = name;
        Description = description;
        Price = price;
        MaxWorker = maxWorker;
        Earning = earning;
        Happiness = happiness;
        Pollution = pollution;
        Trash = trash;
        Electricity = electricity;
        Water = water;
    }
}