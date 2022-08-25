using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] bool isTransport;
    [SerializeField] bool isChanger;
    [SerializeField] bool isPortalIn;
    [SerializeField] PortalControl portalOut;
    [SerializeField] AudioSource portalAudio;
    [SerializeField] AudioSource changerAudio;

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(isTransport && isPortalIn)
        {
            collision.gameObject.transform.localPosition = portalOut.gameObject.transform.position;
            portalAudio.Play();
        }
        if(isChanger)
        {
            FindObjectOfType<DimensionManager>().ChangeTerrainDimension();
            changerAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
