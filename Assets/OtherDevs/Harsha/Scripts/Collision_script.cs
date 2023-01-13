using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_script : MonoBehaviour
{
    
    private float tempspeed;
    private float tempsprintspeed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            tempspeed = collision.gameObject.GetComponent<Cheems>().speed;
            tempsprintspeed = collision.gameObject.GetComponent<Cheems>().sprintspeed;
            collision.gameObject.GetComponent<Cheems>().speed = 0.1f;
            collision.gameObject.GetComponent<Cheems>().sprintspeed = 0.1f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("player exit");
            collision.gameObject.GetComponent<Cheems>().speed= 4;
            collision.gameObject.GetComponent<Cheems>().sprintspeed= 8.5f;
        }
    }
}
