using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    DimensionSwitch[] childrenTerrain;
    DimensionSwitch player;

    [SerializeField] public Color32 firstDimensionColor = new Color32(198, 66, 62, 255); 
    [SerializeField] public Color32 secondDimensionColor = new Color32(62, 137, 198, 255);
    [SerializeField] public Color32 neutralDimensionColor = new Color32(255,255,255, 255);

    void Start()
    {
        childrenTerrain = GetComponentsInChildren<DimensionSwitch>();
        player = FindObjectOfType<PlayerControl>().GetComponent<DimensionSwitch>();
    }

    public void ChangeTerrainDimension()
    {
        var playerDimension =  player.currentDimension;
        foreach(DimensionSwitch terrainDimension in childrenTerrain)
        {
            terrainDimension.SwitchDimension();
            terrainDimension.AdjustCollider(playerDimension);
        }
    }

    public void AdjustTerrain()
    {
        var playerDimension =  player.currentDimension;
        foreach(DimensionSwitch terrainDimension in childrenTerrain)
        {
            terrainDimension.AdjustCollider(playerDimension);
        }
    }
}
