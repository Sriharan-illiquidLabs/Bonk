using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Burst_HK : MonoBehaviour
{

    public BoxCollider maincollider;
    public GameObject rig;
    public GameObject glass_Particles;
    
    void start()
    {
        //glass_Particles = Resources.Load("Glass_particles(New)") as GameObject;
        //glass_Particles = GameManager.instance.Glass_Particles_HK;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            if (this.gameObject.tag == "Glass_Cell_HK" &&  GetComponent<House_Burst_HK>() != null)
            {
               var Glasseffect= Instantiate(glass_Particles, transform.position, transform.rotation);
                Destroy(Glasseffect,5.0F);
            }
            foreach (Rigidbody rb in houseRigidbodies)
            {
              
                rb.isKinematic = false;
                var magnitude = 70;
                var force = transform.position - collision.transform.position;
                rb.AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);

            }
           house_burstOn();      
        }
    }
 
    void Start()
    {
        houseBits();
        house_burstOff();
    }
    void Update()
    {
        
    }
    Collider[] houseColliders;
    Rigidbody[] houseRigidbodies;
    void houseBits()
    {
        houseColliders = rig.GetComponentsInChildren<Collider>();
        houseRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }

    public void house_burstOn()
    { 
        foreach (Collider col in houseColliders)
        {
            col.enabled = true;
          
        }

        foreach (Rigidbody rb in houseRigidbodies)
        {
            rb.isKinematic = false;
        }

        maincollider.enabled = false;
       
    }
    public void house_burstOff()
    {
        foreach (Collider col in houseColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rb in houseRigidbodies)
        {
            rb.isKinematic = true;
        }

        maincollider.enabled = true;
    }
}
