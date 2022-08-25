using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    [SerializeField] SpriteRenderer objectSprite;
    [SerializeField] DimensionManager dimensionManager;
    [SerializeField] bool isPlayer;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] AudioSource changeAudio;

    public Dimension currentDimension;

  
    public enum Dimension
    {
        Neutral,
        First,
        Second
    }
    void ChangeColor()
    {
        if(currentDimension == Dimension.First)
        {
            objectSprite.color = dimensionManager.firstDimensionColor;
        }
        else if(currentDimension == Dimension.Second)
        {
            objectSprite.color = dimensionManager.secondDimensionColor;
        }
        else if(currentDimension == Dimension.Neutral)
        {
            objectSprite.color = dimensionManager.neutralDimensionColor;
        }
    }

    public void AdjustCollider(Dimension player)
    {
        if(currentDimension == player || currentDimension == Dimension.Neutral)
        {
            boxCollider2D.enabled = true;
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }
    public void SwitchDimension()
    {
        if(currentDimension == Dimension.First)
        {
            currentDimension = Dimension.Second;
        }
        else if (currentDimension == Dimension.Second)
        {
            currentDimension = Dimension.First;
        }
        if(!isPlayer)
        {
            AdjustCollider(FindObjectOfType<PlayerControl>().GetComponent<DimensionSwitch>().currentDimension);
        }
        ChangeColor();
    }

    void Start()
    {
        dimensionManager = GameObject.FindObjectOfType<DimensionManager>();
        ChangeColor();
        AdjustCollider(FindObjectOfType<PlayerControl>().GetComponent<DimensionSwitch>().currentDimension);
    }

    void Update()
    {
        if(isPlayer && Input.GetKeyUp(KeyCode.F))
        {
            SwitchDimension();
            dimensionManager.AdjustTerrain();
            changeAudio.Play();
        }
    }
}
