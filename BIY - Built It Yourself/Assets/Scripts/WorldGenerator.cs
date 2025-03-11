using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    private GameObject DefaultGround;
    private GameObject DefaultWater;

    private GameObject[] groundPrefabs; // Tableau des préfabriqués de sols (gazon, sable, eau, boue)
    private GameObject[] natureElementsPrefabs; // Tableau des préfabriqués d'éléments naturels (arbres, roches, etc.)
  
    [Header("Generation Settings")]
    public float natureElementProbability = 0.2f; // Probabilité d'inclure un élément naturel

    [Header("Generate Lac")]
   public float valnoiseScaleMin = 0.05f; // Ajustez cette valeur pour changer la "densité" du bruit
    public float valnoiseScaleMax = 0.2f; // Ajustez cette valeur pour changer la "densité" du bruit

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
    /// Permet de généré un monde avec les liste des prefabs préramblies pour que le joueurs est monde avec des arbes
    /// et des élements naturel 
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


                // Créez la tuile et attribuez-la à la grille


                Tile tile = new Tile(worldPosition, new Vector3(0, 0, 0), type, Tile.TileSubType.Empty, name, "", 0); // Adaptez les paramètres selon vos besoins
                gridManager.SetValue(x, y, tile);
             //   Debug.Log($" X : ( {tile.Position.x}) + y : {tile.Position.y} + z : {tile.Position.z} + type : {tile.Type}   ");

                if (!isWater && Random.value < natureElementProbability)
                {
                    GameObject natureElementPrefab = natureElementsPrefabs[Random.Range(0, natureElementsPrefabs.Length)];
                    GameObject naturePrefab = Instantiate(natureElementPrefab, worldPosition, Quaternion.identity);
                    naturePrefab.transform.SetParent(this.transform);

                    // Mettez à jour la tuile pour refléter la présence d'un élément naturel
                    Tile natureTile = new Tile(worldPosition, new Vector3(0,0,0), Tile.TileType.Nature, Tile.TileSubType.Empty, natureElementPrefab.name, "", 0); // Adaptez comme nécessaire
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
    /// Permet de généré des nuages (je voulais faire que selon le temps l'intensité de la lumiere change et que les nages prduise des ombres)
    /// Auteur :Oli 
    /// </summary>
    private void GenerateSky()
    {
        float time = 0; // Vous pourrez ajuster cette variable pour simuler différents moments de la journée
        float cloudProb = 0.01f; // Probabilité d'instancier un nuage

        // TODO: Définir la couleur de l'éclairage en fonction du moment de la journée

        // Génération des nuages
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

                        // Assurez-vous que vos préfabriqués de nuages ont un composant Renderer configuré pour projeter des ombres
                        // Cela peut nécessiter des ajustements dans les préfabriqués eux-mêmes via l'éditeur Unity
                    }
                }
            }
        }
    }



}