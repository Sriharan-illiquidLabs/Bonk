using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimalRespawn_AD : MonoBehaviour
{
    public GameObject animalPrefab;
    public GameObject childObject;

    [Header("Time Setting")]
    float TimetoDestroy = 7;
    float TimetoRespawn = 12;

    AnimalRagdollToggle_AD animalRagdollToggle;
    private bool triggerOnce;

    void Start()
    {
        AnimalRespawn();
    }

    void Update()
    {
        if(animalRagdollToggle.hit)
        {
            if(triggerOnce)
            {
                triggerOnce = false;
                Destroy(childObject, TimetoDestroy);
                Invoke("AnimalRespawn",TimetoRespawn);
            }
        }
    }

    void AnimalRespawn()
    {
        triggerOnce = true;
        childObject = Instantiate(animalPrefab,transform.position,Quaternion.identity, transform);
        animalRagdollToggle = GetComponentInChildren<AnimalRagdollToggle_AD>();
    }
}
