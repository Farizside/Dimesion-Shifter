using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveDimensionalShard : MonoBehaviour
{
    public static SaveDimensionalShard Instance;
    
    public SaveData activeSave;

    public bool hasLoaded;
    
    private void Awake()
    {
        Instance = this;
        
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DeleteSavedData();
            
            PlayerPrefs.DeleteAll();
            
            Debug.Log("Deleted");
        }
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        
        serializer.Serialize(stream, activeSave);
        
        stream.Close();
        Debug.Log("saved");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            
            activeSave = serializer.Deserialize(stream) as SaveData;
            
            stream.Close();

            hasLoaded = true;
            Debug.Log("loaded");
        }
    }

    public void DeleteSavedData()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }
    
    [System.Serializable]
    public class SaveData
    {
        public string saveName;

        public int dimensionShard;
    }
}
