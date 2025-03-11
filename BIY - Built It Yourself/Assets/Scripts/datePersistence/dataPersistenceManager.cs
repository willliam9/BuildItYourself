using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class dataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Conf")]
    [SerializeField] private string fileName;
    [SerializeField] private string worldID;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDatahandler dataHandler;
    private GameManager gameManager;
    public static dataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;

        // Obtenez la référence à GameManager en utilisant FindObjectOfType
        gameManager = FindObjectOfType<GameManager>();

        // Après avoir obtenu la référence, vous pouvez accéder aux propriétés de GameManager
        if (gameManager != null)
        {
            fileName = gameManager.Name;
            worldID = gameManager.WorldId;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void Start()
    {
        this.dataHandler = new FileDatahandler(Application.persistentDataPath, fileName, worldID);
        this.dataPersistenceObjects = FindAllDataPersistenceObecjts();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load(); 
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        // TODO mettre les logs des infos enregistrer
        Debug.Log("Loaded ");
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // TODO mettre les logs des infos enregistrer
        Debug.Log("Saved ");

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObecjts()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
