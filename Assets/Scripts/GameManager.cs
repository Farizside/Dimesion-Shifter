using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string PlayerDataPrefKey = "PlayerData";

    public int dimensionShard;
    
    [SerializeField] AudioSource changeStageSound;
    [SerializeField] AudioSource shardCollectSound;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerDataPrefKey))
        {
            var defaultDimensionalShard = PlayerPrefs.GetInt(PlayerDataPrefKey);
            dimensionShard = defaultDimensionalShard;
        }

        if (SaveDimensionalShard.Instance.hasLoaded)
        {
            dimensionShard = SaveDimensionalShard.Instance.activeSave.dimensionShard;
        }
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayStageChangeSound()
    {
        changeStageSound.Play();
    }

    public void PlayCollectShardSound()
    {
        shardCollectSound.Play();
    }
}
