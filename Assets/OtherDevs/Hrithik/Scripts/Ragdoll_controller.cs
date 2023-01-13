using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;

public class Ragdoll_controller : MonoBehaviour
{
    public static Ragdoll_controller Rc_instance;
    private CapsuleCollider maincollider;
    private Animator animator;
    private GameObject Blood_HK;
    BezierWalkerWithSpeed bezi;
    public bool isGoathit;
    public float DefaultStaringPoint;
    public BezierSpline thisSpline;
    public float thisSpeed;
    public Vector3 pos;
    //public bool isDead;
    //public Spawn_Properties sp;
    // Start is called before the first frame update
    private void Awake()
    {
        Rc_instance= this;  
    }
    void Start()
    {       
        maincollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        GetragDollBits();
        ragDollOff();
        Blood_HK = GameManager.instance.BloodParticles_HK;
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
    public void Update()
    {
        if (isGoathit == true)
        {
           if(Spawn_Properties.Sp_Instance.canbeSpawned = true)
            {
                GameManager.instance.rp.GetComponent<Respawning>().Instantiatepeople();
            }
            //GameManager.instance.rp.GetComponent<Respawning>().Invoke("Instantiatepeople", 2.25f);
            //GameManager.instance.rp.GetComponent<Respawning>().instantiatepos = pos;
            //GameManager.instance.rp.humanprefabNormalT = GetComponent<BezierWalkerWithSpeed>().NormalizedT;
            //GameManager.instance.rp.humaprefabSpline = thisSpline;
            //GameManager.instance.rp.humanprefabSpeed = thisSpeed;
            isGoathit = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /*&& Input.GetKey(KeyCode.LeftShift)*/)
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            maincollider.enabled = false;
            isGoathit = true;

            if(gameObject.tag == "Female_HK")
            {
                Blood_HK = GameManager.instance.BloodParticlesFemale_HK;
            }

            if(gameObject.tag == "IdleHuman_HK" && bezi == null)
            {
                ragDollOn();
                isGoathit=false;
            }

         
            GetComponent<Rigidbody>().isKinematic = false;
            var replacement = Instantiate(Blood_HK, transform.position,
                   transform.rotation);

            Destroy(replacement,5.0f);
            
            if(isGoathit )
            {
                ragDollOn();
               
                
                //GameManager.instance.rp.humanprefabNormalT = gameObject.GetComponent<BezierWalkerWithSpeed>().NormalizedT;
                Destroy(gameObject, 5f);
            
                if(gameObject.tag == "IdleHuman_HK")
                {
                   bezi.enabled = false;
                }
          
            }     
            
            //if(collision.gameObject.tag == "Player" && gameObject.tag == "IdleHuman_HK")
            //{
            //    ragDollOn();
            //}

            AddRigidBodyForce(collision.transform , 70);
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
    public void CleanChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
