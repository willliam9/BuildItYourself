using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class ButonBuildingData : MonoBehaviour
{
    [SerializeField]
    private string _nameBuild = "";

    [SerializeField]
    private Tile.TileType _typeBuild = Tile.TileType.Empty;

    [SerializeField]
    private Tile.TileSubType _subTypeBuild = Tile.TileSubType.Empty;

    public static string nameBuild { get; private set; }
    public static Tile.TileType typeBuild { get; private set; }
    public static Tile.TileSubType subTypeBuild { get; private set; }
}