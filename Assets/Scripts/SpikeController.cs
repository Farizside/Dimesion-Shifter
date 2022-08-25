using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private const string PlayerDataPrefKey = "PlayerData";
    
    GameObject dieScreen;
    
    private GameObject _gameManager;

    void Awake()
    {
        dieScreen = GameObject.FindGameObjectWithTag("Finish");
        
        _gameManager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        dieScreen.GetComponent<PanelController>().ToggleActive();
        collider2D.gameObject.SetActive(false);
        
        _gameManager.GetComponent<GameManager>().dimensionShard = PlayerPrefs.GetInt(PlayerDataPrefKey);
    }
}