using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width;
    public int height;
    public float cellSize = 1f;

    private Vector3 originPosition;
    private int[,] gridArray;

    private Tile[,] tileArray;


    public static GridManager Instance { get; private set; }

    private GameManager gameManager;

    public void Awake()
    {
        InitializeSingleton();
        InitializeGrid();
        DrawGrid();
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
        else
        {
            width = gameManager.Widht;
            height = gameManager.Height;
        }

    }


    // permet d'initier la grid 
    private void InitializeGrid()
    {
        originPosition = transform.position;

        tileArray = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileArray[x, y] = new Tile(new Vector3(x, 0, y), new Vector3(0, 0, 0), Tile.TileType.Empty, Tile.TileSubType.Empty, "", "", 0) ;
            }
        }
    }


    /// <summary>
    /// Debug void qui sert a tracer des marqueur sur la grid pour diférencier les cases 
    /// </summary>
    private void DrawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                DebugDrawCell(x, y);
            }
        }
    }


    /// <summary>
    /// Debug void permet d'afficher la poistion des chaques cases
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void DebugDrawCell(int x, int y)
    {
        Vector3 cellPosition = GetCellWorldPosition(x, y);
        Debug.DrawLine(cellPosition, cellPosition + new Vector3(cellSize, 0, 0), Color.red, 100f);
        Debug.DrawLine(cellPosition, cellPosition + new Vector3(0, 0, cellSize), Color.red, 100f);
    }

    /// <summary>
    /// Permet d'afficher le Gizmo
    /// </summary>
    private void OnDrawGizmos()
    {

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.black;

        Gizmos.color = Color.red;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 cellCenter = GetCellWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f;
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));

                // Utiliser le style lors de l'appel de Handles.Label pour changer la couleur du texte
                UnityEditor.Handles.Label(cellCenter, $"({x},{z})", labelStyle);
            }
        }
    }


    public Vector3 GetCellWorldPosition(int x, int y)
    {
        return originPosition + new Vector3(x * cellSize, 0, y * cellSize);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        int y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        return new Vector2Int(x, y);
    }

    public void SetValue(int x, int y, Tile tile)
    {
 
            tileArray[x, y] = tile;

    }
    public Tile GetTile(int x, int y)
    {
        if (tileArray[x, y] is null)
            Debug.LogError("tile nulle");

            return tileArray[x, y];
    }



    public Tile[,] GetArrayWorld()
    {
        return tileArray;
    }

    public void setArrayWorld(Tile[,] array)
    {
        tileArray = array;
    }

    public List<Tile> Convert2DArrayToList(Tile[,] tilesArray)
    {
        List<Tile> tilesList = new List<Tile>();
        foreach (Tile tile in tilesArray)
        {
            tilesList.Add(tile);
        }
        return tilesList;
    }


}