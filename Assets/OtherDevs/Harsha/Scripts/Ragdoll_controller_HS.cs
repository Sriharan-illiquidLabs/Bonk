using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;

public class Ragdoll_controller_HS : MonoBehaviour
{

    private CapsuleCollider maincollider;
    private Animator animator;
    private GameObject Blood_HK;
    BezierWalkerWithSpeed bezi;
    public bool ishit;

    // Start is called before the first frame update
    private void Awake()
    {
        

    }
    void Start()
    {
        ishit = false;
        maincollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        GetragDollBits();   
        ragDollOff();
        Blood_HK = GameManager.instance.BloodParticles_HK;
        bezi = GetComponent<BezierWalkerWithSpeed>();
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            ishit = true;
            GetComponent<Rigidbody>().isKinematic = false;
            var replacement = Instantiate(Blood_HK, transform.position,
                   transform.rotation);

            ragDollOn();

            foreach (Rigidbody rb in ragDollRigidbodies)
            {
                rb.isKinematic = false;
                var magnitude = 70f;
                var force = transform.position - collision.transform.position;
                rb.AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);
            }

        }
        else
        {
            ishit = false;
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
                rb.mass =0.2f;
            }
    }

        animator.enabled = true;
        maincollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
