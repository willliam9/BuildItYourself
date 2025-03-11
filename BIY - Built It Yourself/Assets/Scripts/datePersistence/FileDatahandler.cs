using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDatahandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    private string worldID = "";

    public FileDatahandler(string dataDirPath, string dataFileName, string worldID)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.worldID = worldID;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, $"{worldID}_{dataFileName}");
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {

                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, $"{data.worldID}_{dataFileName}");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}
