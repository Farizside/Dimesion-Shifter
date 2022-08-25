using UnityEngine;

public class ShardsSaveData : MonoBehaviour
{
    private static ShardsSaveData _instance;

    public static ShardsSaveData Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Initialize()
    {
        //ClearData();
        
        if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)
        {
            LoadData();
        }
        else
        {
            SaveData();
            PlayerPrefs.SetInt("GameStartFirstTime", 1);
        }
    }
    
    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }
    
    public void SaveData()
    {
        // string levelDataString = JsonUtility.ToJson(LevelSystemManager.Instance.LevelData);

        try
        {
            // System.IO.File.WriteAllText(Application.persistentDataPath + "/LevelData.json", levelDataString);
            
            Debug.Log("Data Saved");

        }
        catch (System.Exception e)
        {
            Debug.Log("Error Saving Data:" + e);
            
            throw;
        }
    }

    //Method used to load the data
    private void LoadData()
    {
        try
        {
            string levelDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/LevelData.json");
            
            LevelData levelData = JsonUtility.FromJson<LevelData>(levelDataString); //create LevelData from json
            
            if (levelData != null)
            {
                // LevelSystemManager.Instance.LevelData.levelItemArray = levelData.levelItemArray;
                
                // LevelSystemManager.Instance.LevelData.lastUnlockedLevel = levelData.lastUnlockedLevel;
            }
            Debug.Log("Data Loaded");
        }
        catch (System.Exception e)
        {
            Debug.Log("Error Loading Data:" + e);
            throw;
        }
    }
    
    public void ClearData()
    {
        Debug.Log("Data Cleared");
        LevelData levelData = new LevelData();
        SaveData();
        PlayerPrefs.SetInt("GameStartFirstTime", 1);
    }
}

[System.Serializable]
public class LevelData
{
    public int lastUnlockedLevel = 0;   //has referece to lastUnlockedLevel
    public LevelItem[] levelItemArray;       //reference to level data
}

[System.Serializable]
public class LevelItem                  //level item
{
    public bool unlocked;
    public int starAchieved;
}
