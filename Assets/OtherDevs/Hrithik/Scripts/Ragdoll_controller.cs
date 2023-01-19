using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;

public class Ragdoll_controller : MonoBehaviour
{
   
    private CapsuleCollider maincollider;
    private Animator animator;
    private GameObject Blood_HK;
    public GameObject Male_Blood_HK;
    public GameObject Female_Blood_HK;
    BezierWalkerWithSpeed bezi;
    public bool isGoathit;
    public float DefaultStaringPoint;
    public BezierSpline thisSpline;
    public float thisSpeed;
    public Vector3 pos;
    public Respawning rsp;
    private void Awake()
    {
       
    }
    void Start()
    {
      if(rsp == null)
        {
            //print("Desto");
            rsp = GetComponentInParent<Respawning>();
        }



        maincollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        GetragDollBits();
        ragDollOff();
        Blood_HK = Male_Blood_HK;
        bezi = gameObject.GetComponent<BezierWalkerWithSpeed>();

        if (gameObject.tag != "IdleHuman_HK" && gameObject.tag != "vendiChar")
        {
            thisSpline = bezi.Spline;
            thisSpeed = bezi.speed;
        }

        foreach (Rigidbody rb in ragDollRigidbodies)
        {        
         rb.mass = 0.2f;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            maincollider.enabled = false;
            isGoathit = true;

            if(gameObject.tag == "Female_HK")
            {
                Blood_HK = Female_Blood_HK;
            }

            if(gameObject.tag == "IdleHuman_HK" && bezi == null)
            {
                ragDollOn();
                isGoathit=false;
            }
         
            GetComponent<Rigidbody>().isKinematic = false;
            var replacement = Instantiate(Blood_HK, collisionPoint,
                   transform.rotation);

            Destroy(replacement,5.0f);
            
            if(isGoathit )
            {
                if (Spawn_Properties.Sp_Instance.canbeSpawned)
                {
                    //print("Desto");
                    rsp.Instantiatepeople();
                }
                ragDollOn();                                         
                Destroy(gameObject, 5f);
            
                if(gameObject.tag == "IdleHuman_HK")
                {
                   bezi.enabled = false;
                }
          
            }     
          
            AddRigidBodyForce(collision.transform , 50);
        }

    }
    Collider[] ragDollColliders;
    Rigidbody[] ragDollRigidbodies;
    void GetragDollBits()
    {
        ragDollColliders = GetComponentsInChildren<Collider>();
        ragDollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void ragDollOn()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        animator.enabled = false;

        if(bezi != null)
        {
            bezi.enabled = false;
        }
        foreach (Collider col in ragDollColliders)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                col.enabled = true;
                col.isTrigger = false;
            }
        }
        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            if (rb.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
            }
        }
        
        maincollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void ragDollOff()
    {
   
        foreach (Collider col in ragDollColliders)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                col.enabled = false;

            }
        }
        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            if (rb.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                rb.isKinematic = true;
                rb.mass = 0.2f;
            }
        }
        animator.enabled = true;
        maincollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        
    }
   
    public void AddRigidBodyForce(Transform other, float magnitude)
    {
        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            //rb.isKinematic = false;
            var force = transform.position - other.position;
            rb.AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);
        }
    }

    public void Respawnragdoll()
    {
        foreach (Collider col in ragDollColliders)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                col.enabled = false;

            }
        }
        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            if (rb.gameObject.layer != LayerMask.NameToLayer("grabMe"))
            {
                rb.isKinematic = true;
                rb.mass = 0.2f;
            }
        }
        animator.enabled = true;
        maincollider.enabled = true;
        bezi.enabled = true;
        bezi.NormalizedT = DefaultStaringPoint;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
}
