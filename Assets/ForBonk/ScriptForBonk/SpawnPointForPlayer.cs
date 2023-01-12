using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointForPlayer : MonoBehaviour
{

    public Transform[] spawnPoints;

    public GameObject player;

    void Start()
    {
        GameObject player1 =  Instantiate(player, spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
        Cinemachine.CinemachineFreeLook gm =   GameManager.instance.cine.GetComponent<Cinemachine.CinemachineFreeLook>();
        gm.Follow = player1.transform;
        gm.LookAt = player1.transform;
    }

    void Update()
    {
        
    }
}
