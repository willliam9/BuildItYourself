using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldListManagement : MonoBehaviour
{
    //private WorldList worldList;

    public static WorldList worldList { get;  private set; }

    public static WorldListManagement worldListManagement { get; private set; }


    void Start()
    {
        LoadWorldList();
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (worldListManagement == null)
        {
            worldListManagement = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

        public static void SaveWorldList()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "WORLDS.json");
        string json = JsonUtility.ToJson(worldList);
        System.IO.File.WriteAllText(filePath, json);
    }

    public static void LoadWorldList()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "WORLDS.json");

        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            worldList = JsonUtility.FromJson<WorldList>(json);
            Debug.Log($"Open World List : " + Application.persistentDataPath);
        }
        else
        {
            worldList = new WorldList();
            Debug.Log($"New World List : " + Application.persistentDataPath);
            SaveWorldList();
        }
    }

    public void CreateNewWorld(string worldName, string worldID)
    {
        World newWorld = new World
        {
            worldName = worldName,
            worldID = worldID,
            creationDate = System.DateTime.Now.ToString() // Utilisez la date actuelle comme date de création
        };

        worldList.worlds.Add(newWorld);
        SaveWorldList();
    }

    // Méthode pour charger un monde existant
    public static void LoadWorld(string worldID, string name)
    {
        // Ajoutez la logique pour charger le monde en fonction de l'ID
        Debug.Log($"Loading World with ID: {worldID}");
        WorldSettings.loader = true;
        WorldSettings.id = worldID;
        WorldSettings.name = name;
        SceneManager.LoadScene("BIY- WORLD");
    }
}
