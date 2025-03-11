using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    private GameObject DefaultGround;
    private GameObject DefaultWater;

    private GameObject[] groundPrefabs; // Tableau des pr�fabriqu�s de sols (gazon, sable, eau, boue)
    private GameObject[] natureElementsPrefabs; // Tableau des pr�fabriqu�s d'�l�ments naturels (arbres, roches, etc.)
  
    [Header("Generation Settings")]
    public float natureElementProbability = 0.2f; // Probabilit� d'inclure un �l�ment naturel

    [Header("Generate Lac")]
   public float valnoiseScaleMin = 0.05f; // Ajustez cette valeur pour changer la "densit�" du bruit
    public float valnoiseScaleMax = 0.2f; // Ajustez cette valeur pour changer la "densit�" du bruit

    public float valwaterThreshold = 4f; // Ajustez cette valeur pour changer la taille du lac

    private GridManager gridManager;
    private GameManager gameManager;


    private void Start()
    {
        gridManager = GridManager.Instance;

        if (gridManager == null)
        {
            Debug.LogError("GridManager instance is not set. Please ensure GridManager is initialized.");
            return;
        }

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            DefaultGround = gameManager.PrefabDefaultGround;
            DefaultWater = gameManager.PrefabDefaultWater;
            groundPrefabs = gameManager.PrefabsGround;
            natureElementsPrefabs = gameManager.PrefabsNature;
        }

        GenerateSky();

        if(!gameManager.loader)
            GenerateWorld();
        
    }


    /// <summary>
    /// Permet de g�n�r� un monde avec les liste des prefabs pr�ramblies pour que le joueurs est monde avec des arbes
    /// et des �lements naturel 
    /// </summary>
    private void GenerateWorld()
    {
        float mapSize = gridManager.width * gridManager.height;
        float noiseScale = Mathf.Clamp(5f / Mathf.Sqrt(mapSize), valnoiseScaleMin, valnoiseScaleMax);
        float waterThreshold = Mathf.Clamp01(1f - Mathf.Log10(mapSize) / valwaterThreshold);

        for (int x = 0; x < gridManager.width; x++)
        {
            for (int y = 0; y < gridManager.height; y++)
            {
                Vector3 worldPosition = gridManager.GetCellWorldPosition(x, y) + new Vector3(gridManager.cellSize * 0.5f, 0, gridManager.cellSize * 0.5f);

                float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
                bool isWater = noiseValue < waterThreshold;

                // Choisissez le type de tuile en fonction de si c'est de l'eau ou pas
                Tile.TileType type = isWater ? Tile.TileType.Water : Tile.TileType.Ground;
                string name = isWater ? DefaultWater.name : DefaultGround.name;


                // Cr�ez la tuile et attribuez-la � la grille


                Tile tile = new Tile(worldPosition, new Vector3(0, 0, 0), type, Tile.TileSubType.Empty, name, "", 0); // Adaptez les param�tres selon vos besoins
                gridManager.SetValue(x, y, tile);
             //   Debug.Log($" X : ( {tile.Position.x}) + y : {tile.Position.y} + z : {tile.Position.z} + type : {tile.Type}   ");

                if (!isWater && Random.value < natureElementProbability)
                {
                    GameObject natureElementPrefab = natureElementsPrefabs[Random.Range(0, natureElementsPrefabs.Length)];
                    GameObject naturePrefab = Instantiate(natureElementPrefab, worldPosition, Quaternion.identity);
                    naturePrefab.transform.SetParent(this.transform);

                    // Mettez � jour la tuile pour refl�ter la pr�sence d'un �l�ment naturel
                    Tile natureTile = new Tile(worldPosition, new Vector3(0,0,0), Tile.TileType.Nature, Tile.TileSubType.Empty, natureElementPrefab.name, "", 0); // Adaptez comme n�cessaire
                    gridManager.SetValue(x, y, natureTile);
                }

                // Instanciez le prefab de terrain correspondant

                if (isWater)
                    Instantiate(DefaultWater, worldPosition, Quaternion.identity).transform.SetParent(this.transform);
                else
                    Instantiate(DefaultGround, worldPosition, Quaternion.identity).transform.SetParent(this.transform);


            }
        }
    }

    /// <summary>
    /// Permet de g�n�r� des nuages (je voulais faire que selon le temps l'intensit� de la lumiere change et que les nages prduise des ombres)
    /// Auteur :Oli 
    /// </summary>
    private void GenerateSky()
    {
        float time = 0; // Vous pourrez ajuster cette variable pour simuler diff�rents moments de la journ�e
        float cloudProb = 0.01f; // Probabilit� d'instancier un nuage

        // TODO: D�finir la couleur de l'�clairage en fonction du moment de la journ�e

        // G�n�ration des nuages
        for (int x = 0; x < gridManager.width; x++)
        {
            for (int y = 0; y < gridManager.height; y++)
            {
                Vector3 worldPosition = gridManager.GetCellWorldPosition(x, y) + new Vector3(gridManager.cellSize * 0.5f, 8, gridManager.cellSize * 0.5f);

                for (int c = 0; c < gameManager.PrefabsSky.Length - 1; c++)
                {
                    if (Random.value < cloudProb)
                    {
                        GameObject cloud = gameManager.PrefabsSky[c];
                        GameObject cloudInstance = Instantiate(cloud, worldPosition, Quaternion.identity);
                        cloudInstance.transform.SetParent(this.transform);

                        // Assurez-vous que vos pr�fabriqu�s de nuages ont un composant Renderer configur� pour projeter des ombres
                        // Cela peut n�cessiter des ajustements dans les pr�fabriqu�s eux-m�mes via l'�diteur Unity
                    }
                }
            }
        }
    }



}