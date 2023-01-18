using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkin_Respawn : MonoBehaviour
{
    public float respawntime = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Invoke("Respawning", respawntime);
        }
    }
    public void Respawning()
    {
        gameObject.SetActive(true);
    }
}
