using UnityEngine;
using UnityEngine.UI;

public class DimensionShardUI : MonoBehaviour
{
    public Text dimensionShard;

    private GameObject _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        dimensionShard.text = "Dimension Shard: " + _gameManager.GetComponent<GameManager>().dimensionShard;
    }
}
