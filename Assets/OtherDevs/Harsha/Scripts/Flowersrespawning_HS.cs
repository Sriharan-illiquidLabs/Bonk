using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowersrespawning_HS: MonoBehaviour
{
    public float respawntime = 5f;

    private void OnTriggerEnter(Collider collision)
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
