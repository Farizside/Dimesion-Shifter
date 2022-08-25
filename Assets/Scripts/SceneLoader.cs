using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private const string PlayerDataPrefKey = "PlayerData";
    
    private GameObject _gameManager;
    
    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
    }
    
    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _gameManager.GetComponent<GameManager>().dimensionShard = PlayerPrefs.GetInt(PlayerDataPrefKey);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void GoToWorldap()
    {
        SceneManager.LoadScene("World_Map");
    }
}
