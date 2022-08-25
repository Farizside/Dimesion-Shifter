using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStage : MonoBehaviour
{
    public int shardTotal;

    public string sceneName;

    public Button stageButton;

    public Text textButton;
    
    public LoadData activeLoadData;
    
    private GameObject _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager");

        if (_gameManager.GetComponent<GameManager>().dimensionShard >= shardTotal)
        {
            stageButton.interactable = true;
        }
        
        textButton.text = _gameManager.GetComponent<GameManager>().dimensionShard + "/" + shardTotal;
    }

    public void LoadScene()
    {
        Load();
        
        SceneManager.LoadScene(sceneName);
    }

    private void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + activeLoadData.loadName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveManager.SaveData));
            var stream = new FileStream(dataPath + "/" + activeLoadData.loadName + ".save", FileMode.Open);
            
            activeLoadData = serializer.Deserialize(stream) as LoadData;
            
            stream.Close();
            
            Debug.Log("loaded");
        }
    }
    
    [Serializable]
    public class LoadData
    {
        public string loadName;
    }
}
