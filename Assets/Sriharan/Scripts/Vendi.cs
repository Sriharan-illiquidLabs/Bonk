using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendi : MonoBehaviour
{
    public Transform t2;
    public Transform spawnPoint;

    public GameObject[] vendiChar;
    public GameObject push;

    public GameObject currentSpawn;


    private void Start()
    {
        Spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CashPick"))
        {
            GameObject cash = other.gameObject;

            cash.LeanMove(transform.position, 0.2f).setOnComplete(() =>
            {
                cash.LeanMove(t2.transform.position, 0.4f);
                LeanTween.delayedCall(1f, Ragg);
            });
        }
    }
    
    async void Ragg()
    {
        currentSpawn.GetComponent<Ragdoll_controller>().ragDollOn();
        Transform[] Ct = currentSpawn.GetComponentsInChildren<Transform>();
        foreach(Transform t in Ct)
        {
            if(t.gameObject.layer == LayerMask.NameToLayer("grabMe"))
            {
                t.tag = "vendiChar";

            }
        }
    }

    public void Spawn()
    {
        if(currentSpawn == null)
        {
            currentSpawn = Instantiate(vendiChar[Random.Range(0,4)], spawnPoint.position, spawnPoint.rotation);
        }
    }
}
