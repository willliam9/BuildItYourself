using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string worldId;
    [SerializeField]
    private int widht;
    [SerializeField]
    private int height;
    [SerializeField]
    private int money;
    [SerializeField]
    private int happiness;
    [SerializeField]
    private int pollution;
    [SerializeField]
    private int security;
    [SerializeField]
    private int volume;
    [SerializeField]
    private int trash;
    [SerializeField]
    private int electricity;
    [SerializeField]
    private int water;
    [Header("activer le loader")]
    [SerializeField]
    public bool loader = false;
   



    [Header("Prefabs Reference")]
    [Header("DefaultGround")]
    [SerializeField]
    private GameObject prefabDefaultGround;
    [Header("DefaultWater")]
    [SerializeField]
    private GameObject prefabDefaultWater;
    [Header("DefaultSky")]
    [SerializeField]
    private GameObject prefabDefaultSky;
    [Header("Prefabs ground")]
    [SerializeField]
    private GameObject[] prefabGround;
    [Header("Prefabs Building Residential")]
    [SerializeField]
    private GameObject[] prefabsBuildingResidential;
    [Header("Prefabs Building Commercial")]
    [SerializeField]
    private GameObject[] prefabsBuildingCommercial;
    [Header("Prefabs Building Industrial")]
    [SerializeField]
    private GameObject[] prefabsBuildingIndustrial;

    [Header("Prefabs  Leizure")]
    [SerializeField]
    private GameObject[] prefabsBuildingLeizure;
    [Header("Prefabs  Monument")]
    [SerializeField]
    private GameObject[] prefabsBuildingMonument;
    [Header("Prefabs  Tourist")]
    [SerializeField]
    private GameObject[] prefabsBuildingTourist;
    [Header("Prefabs  Supply")]
    [SerializeField]
    private GameObject[] prefabsBuildingSupply;
    [Header("Prefabs  Service")]
    [SerializeField]
    private GameObject[] prefabsBuildingService;

    [Header("Prefabs  Decoration")]
    [SerializeField]
    private GameObject[] prefabsDecoration;
    [Header("Prefabs  Nature")]
    [SerializeField]
    private GameObject[] prefabsNature;
    [Header("Prefabs Sky")]
    [SerializeField]
    private GameObject[] prefabsSky;
    [Header("Arrow")]
    [SerializeField]
    private GameObject prefabArrow;
    [Header("TirdPerson")]
    [SerializeField]
    private bool tirdPerson;

    public string Name { get => name; set => name = value; }
    public string WorldId { get => worldId; set => worldId = value; }
    public int Widht { get => widht; set => widht = value; }
    public int Height { get => height; set => height = value; }
    public int Money { get => money; set => money = value; }
    public int Happiness { get => happiness; set => happiness = value; }
    public int Pollution { get => pollution; set => pollution = value; }
    public int Trash { get => trash; set => trash = value; }
    public int Electricity { get => electricity; set => electricity = value; }
    public int Water { get => water; set => water = value; }
    public int Security { get => security; set=> security = value; }
    public int Volume { get=>volume; set=> volume = value; }
    public float elapsedTime { get; set; }
    public bool TirdPerson { get => tirdPerson; set=> tirdPerson = value; }
    public GameObject PrefabArrow { get => prefabArrow; set => prefabArrow = value; }


    public GameObject PrefabDefaultGround { get => prefabDefaultGround; set => prefabDefaultGround = value; }
    public GameObject PrefabDefaultWater { get => prefabDefaultWater; set => prefabDefaultWater = value; }

    public GameObject PrefabDefaultSky { get => prefabDefaultSky; set => prefabDefaultSky = value; }


    public GameObject[] PrefabsGround { get => prefabGround; set => prefabGround = value; }

    public GameObject[] PrefabsBuildingResidential { get => prefabsBuildingResidential; private set => prefabsBuildingResidential = value; }

    public GameObject[] PrefabsBuildingCommercial { get; private set; }

    public GameObject[] PrefabsBuildingIndustrial { get; private set; }

    public GameObject[] PrefabsBuildingLeizure { get; private set; }

    public GameObject[] PrefabsBuildingMonument { get; private set; }

    public GameObject[] PrefabsBuildingTourist { get; private set; }

    public GameObject[] PrefabsBuildingSupply { get; private set; }

    public GameObject[] PrefabsBuildingService { get; private set; }

    public GameObject[] PrefabsDecoration { get; private set; }

    public GameObject[] PrefabsNature { get => prefabsNature; set => prefabsNature = value; }
    public GameObject[] PrefabsSky { get => prefabsSky; set => prefabsSky = value; }



    public static GameManager gameInstance { get; private set; }

    private float totalTimeElapsed = 0f;

    private GridManager gridManager;

    private void Awake()
    {
        // Pour initialiser le gridManager 

        name = WorldSettings.name;
        widht = WorldSettings.width;
        height = WorldSettings.height;
        worldId = WorldSettings.id;
        loader = WorldSettings.loader;
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager == null)
        {
            Debug.LogError("gridManager null");
            return;
        }


        InitializeSingleton();

    }

    private void InitializeSingleton()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Permet de loader le monde (TODO doit être pouvoir être appeler sur appel et non automatiquement )
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(GameData data)
    {

        if (data == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            //NewGame();
        }
        else
        {
            if (loader)
            {
                Name = data.name;
                WorldId = data.worldID;
                Widht = data.widht;
                Height = data.height;
                Money = data.money;
                Happiness = data.hapiness;
                Pollution = data.pollution;
                Trash = data.trash;
                Electricity = data.electrarity;
                Water = data.water;
                gridManager.setArrayWorld(ConvertToTileArray(data.array, widht, height));
                totalTimeElapsed = data.elapsedTime;
                //this.gameData = dataHandler.Load(data.worldID);

                foreach (var tileData in data.array)
                {
                    Vector3 position = tileData.position; // La position est déjà configurée
                    GameObject prefab = ReturnPrefabsType(tileData.name, tileData.type, tileData.subType);
                    Quaternion rotation = Quaternion.Euler(tileData.rotation.x, tileData.rotation.y, tileData.rotation.z);

                    // Vérifiez si le type est un élément de la nature
                    if (tileData.type != Tile.TileType.Ground && tileData.type != Tile.TileType.Water)
                    {
                        Instantiate(PrefabDefaultGround, position, rotation).transform.SetParent(this.transform);
                        if (tileData.type == Tile.TileType.Building)
                            Debug.LogWarning($" ROT X : {tileData.rotation.x} Y: {tileData.rotation.y} Z: {tileData.rotation.z}  ");
                    }

                    // Ensuite, instanciez l'élément de la nature ou tout autre type de tuile
                    Instantiate(prefab, position, rotation).transform.SetParent(this.transform);
                }
            }
        }


    }


    /// <summary>
    /// Permet d'enregistrer les informatiosn d'un monde (TODO dois être appeler sur appel et non automatiquement)
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        Tile[,] tile = gridManager.GetArrayWorld();

        data.array = ConvertToSerializableList(tile);
        data.name = Name;
        data.worldID = WorldId;
        data.widht = Widht;
        data.height = Height;
        data.money = Money;
        data.hapiness = Happiness;
        data.pollution = Pollution;
        data.trash = Trash;
        data.electrarity = Electricity;
        data.water = Water;
        data.elapsedTime = totalTimeElapsed;

    }


    /// <summary>
    /// Permet de pouvoir rendre l'array tile en une liste sérializable lors de la sauvegarde
    /// Auteur : Oli
    /// </summary>
    /// <param name="tiles"></param>
    /// <returns></returns>
    private List<SerializableTile> ConvertToSerializableList(Tile[,] tiles)
    {
        var list = new List<SerializableTile>();
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                list.Add(new SerializableTile(tiles[i, j]));
            }
        }
        return list;
    }

    /// <summary>
    /// Permet de sérialiser la liste qui a été enregistrer pour la transformer a un array tile 2d
    /// Auteur : Oli 
    /// </summary>
    /// <param name="tiles"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private Tile[,] ConvertToTileArray(List<SerializableTile> tiles, int width, int height)
    {
        Tile[,] tileArray = new Tile[width, height];



        if (tiles.Count != width * height)
        {
            Debug.LogError("Le nombre de tuiles ne correspond pas aux dimensions attendues du tableau.");
            return tileArray;
        }

        //for (int i = 0; i < tiles.Count; i++)
        //{
        //    int x = i % width;
        //    int y = i / width;


        //    SerializableTile st = tiles[i];


        //    Debug.Log($"X: {x} - posx: {st.position.x} | Y: {y} - posy: {st.position.z} ");

        //    tileArray[x, y] = new Tile(new Vector3(st.position.x, st.position.y, st.position.z), new Vector3(st.rotation.x, st.rotation.y, st.rotation.z), st.type, st.subType, st.name, st.description, st.price);

        //}



        for (int i = 0; i < tiles.Count; i++)
        {



            tileArray[(int)tiles[i].position.x, (int)tiles[i].position.z] = new Tile(new Vector3(tiles[i].position.x, tiles[i].position.y, tiles[i].position.z), new Vector3(tiles[i].rotation.x, tiles[i].rotation.y, tiles[i].rotation.z), tiles[i].type, tiles[i].subType, tiles[i].name, tiles[i].description, tiles[i].price);

        }



        return tileArray;
    }

    public GameObject ReturnPrefabsType(string name, Tile.TileType type, Tile.TileSubType subType)
    {
        GameObject prefab = null;

        // Utilisation d'un dictionnaire pour mapper les types de tuiles aux préfabriqués
        Dictionary<(Tile.TileType, Tile.TileSubType), GameObject> prefabDictionary = new Dictionary<(Tile.TileType, Tile.TileSubType), GameObject>
        {
            { (Tile.TileType.Empty, Tile.TileSubType.Empty), PrefabDefaultGround },
            { (Tile.TileType.Ground, Tile.TileSubType.Empty), FindGameObjectByName(prefabGround, name) },
            { (Tile.TileType.Water, Tile.TileSubType.Empty), PrefabDefaultWater },
            { (Tile.TileType.Building, Tile.TileSubType.Residential), GetBuildingPrefabByName(name, Tile.TileSubType.Residential) },
            { (Tile.TileType.Building, Tile.TileSubType.Commercial), GetBuildingPrefabByName(name, Tile.TileSubType.Commercial) },
            { (Tile.TileType.Building, Tile.TileSubType.Industrial), GetBuildingPrefabByName(name, Tile.TileSubType.Industrial) },
            { (Tile.TileType.Building, Tile.TileSubType.Leizure), GetBuildingPrefabByName(name, Tile.TileSubType.Leizure) },
            { (Tile.TileType.Building, Tile.TileSubType.Monument), GetBuildingPrefabByName(name, Tile.TileSubType.Monument) },
            { (Tile.TileType.Building, Tile.TileSubType.Tourist), GetBuildingPrefabByName(name, Tile.TileSubType.Tourist) },
            { (Tile.TileType.Building, Tile.TileSubType.Supply), GetBuildingPrefabByName(name, Tile.TileSubType.Supply) },
            { (Tile.TileType.Building, Tile.TileSubType.Service), GetBuildingPrefabByName(name, Tile.TileSubType.Service) },
            { (Tile.TileType.Decoration, Tile.TileSubType.Empty), FindGameObjectByName(prefabsDecoration, name) },
            { (Tile.TileType.Nature, Tile.TileSubType.Empty), FindGameObjectByName(prefabsNature, name) },
            // D'autres entrées en fonction des combinaisons de type et de sous-type
        };

        // Ajouter une entrée spécifique pour le type de bâtiment avec sous-type
        if (type == Tile.TileType.Building && subType != Tile.TileSubType.Empty)
        {
            prefabDictionary[(type, subType)] = GetBuildingPrefabByName(name, subType);
        }

        // Tentative d'obtenir le préfabriqué correspondant au type et au sous-type
        if (prefabDictionary.TryGetValue((type, subType), out prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogError($"Type non géré: {type}, SubType: {subType}");
            return null;
        }
    }

    private GameObject GetBuildingPrefabByName(string name, Tile.TileSubType subType)
    {
        // Recherche dans les différents tableaux de préfabriqués de bâtiments en fonction du sous-type
        GameObject[] prefabArray = null;
        switch (subType)
        {
            case Tile.TileSubType.Commercial:
                prefabArray = prefabsBuildingCommercial;
                break;
            case Tile.TileSubType.Residential:
                prefabArray = prefabsBuildingResidential;
                break;
            case Tile.TileSubType.Industrial:
                prefabArray = prefabsBuildingIndustrial;
                break;
            case Tile.TileSubType.Leizure:
                prefabArray = prefabsBuildingLeizure;
                break;
            case Tile.TileSubType.Monument:
                prefabArray = prefabsBuildingMonument;
                break;
            case Tile.TileSubType.Tourist:
                prefabArray = PrefabsBuildingTourist;
                break;
            case Tile.TileSubType.Supply:
                prefabArray = PrefabsBuildingSupply;
                break;
            case Tile.TileSubType.Service:
                prefabArray = PrefabsBuildingService;
                break;
        }

        if (prefabArray != null && prefabArray.Length > 0)
        {
            GameObject prefab = prefabArray.FirstOrDefault(p => p != null && p.name == name);
            if (prefab != null)
            {
                return prefab;
            }
        }

        // Aucun préfabriqué de bâtiment trouvé
        return null;
    }


    /// <summary>
    /// Permet de retourner le type du gameObject 
    /// Ateur: William et Olie 
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public Tile.TileSubType ReturnSubType(GameObject prefab)
    {
        Tile.TileSubType type = Tile.TileSubType.Empty;

        if (prefabsBuildingResidential.Contains(prefab))
            type = Tile.TileSubType.Residential;
        else if (prefabsBuildingCommercial.Contains(prefab))
            type = Tile.TileSubType.Commercial;
        else if (prefabsBuildingIndustrial.Contains(prefab))
            type = Tile.TileSubType.Industrial;
        else if (prefabsBuildingLeizure.Contains(prefab))
            type = Tile.TileSubType.Leizure;
        else if (prefabsBuildingMonument.Contains(prefab))
            type = Tile.TileSubType.Monument;
        else if (prefabsBuildingTourist.Contains(prefab))
            type = Tile.TileSubType.Tourist;
        else if (prefabsBuildingSupply.Contains(prefab))
            type = Tile.TileSubType.Supply;
        else if (prefabsBuildingService.Contains(prefab))
            type = Tile.TileSubType.Service;

        return type;
    }

    /// <summary>
    /// Permet de retourner le type du gameObject 
    /// Ateur: Oli 
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public Tile.TileType ReturnType(GameObject prefab)
    {
        Tile.TileType type = Tile.TileType.Empty;

        if (prefabsBuildingCommercial.Contains(prefab) || prefabsBuildingIndustrial.Contains(prefab) || prefabsBuildingResidential.Contains(prefab) ||
            prefabsBuildingLeizure.Contains(prefab)    || prefabsBuildingMonument.Contains(prefab)   || prefabsBuildingTourist.Contains(prefab)     ||
            prefabsBuildingSupply.Contains(prefab)     || prefabsBuildingService.Contains(prefab))
            type = Tile.TileType.Building;
        else if (prefabGround.Contains(prefab))
            type = Tile.TileType.Ground;
        else if (prefabsDecoration.Contains(prefab))
            type = Tile.TileType.Decoration;
        else if (prefabsNature.Contains(prefab))
            type = Tile.TileType.Nature;

        return type;
    }

    /// <summary>
    /// permet de trovuer un gameObject avec son nom en lui passant la liste particuliere utiliser dans ReturnPrefabsType
    /// Auteur : Oli
    /// </summary>
    /// <param name="gameObjects"></param>
    /// <param name="nameToFind"></param>
    /// <returns></returns>
    public GameObject FindGameObjectByName(GameObject[] gameObjects, string nameToFind)
    {
        foreach (GameObject g in gameObjects)
        {
            if (g.name == nameToFind)
            {
                return g;
            }
        }
        return null;
    }


    private void Update()
    {
        if (IsGameRunning())
        {
            float deltaTime = Time.deltaTime;
            totalTimeElapsed += deltaTime;


            elapsedTime = totalTimeElapsed;
        }
    }

    private bool IsGameRunning()
    {

        return true;
    }

}
