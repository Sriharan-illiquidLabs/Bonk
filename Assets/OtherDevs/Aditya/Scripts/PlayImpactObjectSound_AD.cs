using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayImpactObjectSound_AD : MonoBehaviour
{
    private AudioSource audioBit;

    void Start()
    {
        audioBit = GetComponent<AudioSource>();    
        audioBit.loop = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            audioBit.Play();
        }
    }
}
