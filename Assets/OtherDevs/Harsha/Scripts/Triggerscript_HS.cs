using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerscript_HS : MonoBehaviour
{
    public int index;
    public Navigation_Arrow_HS Navigation;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("hit "+index);
            Navigation.indexval = index;
        }
    }
}
