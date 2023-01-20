using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnPlayer_AR : MonoBehaviour
{

    public Transform[] spawnPoints;

    public GameObject player;
    public TextMeshProUGUI coinUI;
    public TextMeshProUGUI multiplierUI;
    void Start()
    {
        GameObject player1 = Instantiate(player, spawnPoints[0].transform.position, Quaternion.identity);
        Cinemachine.CinemachineFreeLook gm = GameManager.instance.cine.GetComponent<Cinemachine.CinemachineFreeLook>();
        gm.Follow = player1.transform;
        gm.LookAt = player1.transform;
        player1.GetComponent<Cheems>().coinCount = coinUI;
        player1.GetComponent<Cheems>().multiplierText = multiplierUI;
    }



}
