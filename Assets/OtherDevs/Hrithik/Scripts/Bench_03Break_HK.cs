using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench_03Break_HK : MonoBehaviour
{
    public GameObject _Bench03_Obj;
    public GameObject Bench_DustParticles_HK;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    //Rigidbody[] Rigidbodies;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            var Dusteffect = Instantiate(Bench_DustParticles_HK, transform.position, transform.rotation);
            var replacement = Instantiate(_Bench03_Obj, transform.position, transform.rotation);
            Destroy(replacement,2.5f);
        }
    }
}

