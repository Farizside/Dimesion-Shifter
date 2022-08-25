using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private const string PlayerDataPrefKey = "PlayerData";
    
    public string sceneName;

    public bool isSave;
    public bool isLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var game = FindObjectOfType<GameManager>();
        
        if (other.CompareTag("Player"))
        {
            if (isSave)
            {
                SaveManager.Instance.Save();
                
                PlayerPrefs.SetInt(PlayerDataPrefKey, game.dimensionShard);

                SaveDimensionalShard.Instance.activeSave.dimensionShard = game.dimensionShard;
                
                SaveDimensionalShard.Instance.Save();
            }

            if (isLoad)
            {
                SaveManager.Instance.Load();
            }
                
            SceneManager.LoadScene(sceneName);
            
            game.PlayStageChangeSound();
        }
    }
}
