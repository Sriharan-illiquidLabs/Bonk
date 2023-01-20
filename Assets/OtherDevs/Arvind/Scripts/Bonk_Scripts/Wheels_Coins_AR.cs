using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels_Coins_AR : MonoBehaviour
{
    public enum Sign
    {
        Positive = 0, 
        Negative = 1,
    }
    public bool coins;

    public float rotationSpeed=0;

    public Sign sign;
    int signVal => sign == Sign.Positive? 1:-1;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!coins)
        {
            transform.Rotate(rotationSpeed *signVal * Time.deltaTime, 0f, 0f);
        }
        else
        {
            transform.Rotate(0f, 100f * Time.deltaTime, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!audioSource)
            return;


        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            audioSource.Play();
        }
    }
}
