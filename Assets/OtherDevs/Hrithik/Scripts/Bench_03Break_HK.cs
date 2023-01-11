using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench_03Break_HK : MonoBehaviour
{
    public GameObject _Bench03_Obj;
   

    void Start()
    {
        GetBenchBits();
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = true;
        }

    }
    Rigidbody[] Rigidbodies;
    void GetBenchBits()
    {
        Rigidbodies = this.GetComponentsInChildren<Rigidbody>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var Dusteffect = Instantiate(GameManager.instance.Bench_DustParticles_HK, transform.position, transform.rotation);
            foreach (Rigidbody rb in Rigidbodies)
            {
                rb.isKinematic = false;
                rb.mass = 0.01f;
                var magnitude = 70f;
                var force = transform.position - collision.transform.position;
                rb.AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);
            }
            Destroy(_Bench03_Obj,3.5f);
        }
    }
}

