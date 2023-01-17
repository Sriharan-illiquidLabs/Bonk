using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnPlayer_AR : MonoBehaviour
{

    public Transform[] spawnPoints;

    public GameObject[] player;
    public TextMeshProUGUI coinUI;
    public TextMeshProUGUI multiplierUI;

    public GameObject current;
    
    bool dog;
    bool ok;

    public int currentInt;
    void Start()
    {
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1 ) && !ok)
        {
            GameStart(0);
               
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !ok)
        {
            GameStart(1);
           
        }

    }

    void GameStart(int i)
    {
        if (i == currentInt)
            return;
        Destroy(current);
        GameObject player1 = Instantiate(player[i], spawnPoints[Random.RandomRange(0, 4)].transform.position, Quaternion.identity);
        current = player1;
        Cinemachine.CinemachineFreeLook gm = GameManager.instance.cine.GetComponent<Cinemachine.CinemachineFreeLook>();
        gm.Follow = player1.transform;
        gm.LookAt = player1.transform;
        player1.GetComponent<Cheems>().coinCount = coinUI;
        player1.GetComponent<Cheems>().multiplierText = multiplierUI;
        ok = true;
        currentInt = i;
        LeanTween.delayedCall(10f, Restart);
    }


    void Restart()
    {
        ok = false;
    } 



}
