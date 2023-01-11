using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Parti_AD : MonoBehaviour
{
    public float particleDeathTime = 1f;
    void Start()
    {
        Destroy(gameObject, particleDeathTime);
    }
}
