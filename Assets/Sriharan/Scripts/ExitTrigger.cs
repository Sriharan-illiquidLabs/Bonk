using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public Vendi vendi;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("vendiChar"))
        {

            Transform[] vT = vendi.currentSpawn.GetComponentsInChildren<Transform>();
            foreach(Transform t in vT)
            {
                t.tag = "people";
            }
            vendi.currentSpawn = null;
            vendi.Spawn();

        }
    }
}
