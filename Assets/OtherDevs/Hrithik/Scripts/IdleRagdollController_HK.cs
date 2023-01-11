using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;

public class IdleRagdollController_HK: MonoBehaviour
{

    public CapsuleCollider maincollider;
    public GameObject rig;
    public Animator animator;
    public static Ragdoll_controller instance;
    public GameObject Blood_HK;
    // Start is called before the first frame update
    void Start()
    {
        GetragDollBits();
        ragDollOff();
        Blood_HK = GameManager.instance.BloodParticles_HK;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            var replacement = Instantiate(Blood_HK, rig.transform.position,
                   transform.rotation);
            foreach (Rigidbody rb in ragDollRigidbodies)
            {
                rb.isKinematic = false;
                var magnitude = 1500;
                var force = transform.position - collision.transform.position;
                rb.AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);
            }
            ragDollOn();
        }

    }
    Collider[] ragDollColliders;
    Rigidbody[] ragDollRigidbodies;
    void GetragDollBits()
    {
        ragDollColliders = rig.GetComponentsInChildren<Collider>();
        ragDollRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }
    public void ragDollOn()
    {
        animator.enabled = false;
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            rb.isKinematic = false;
        }
        maincollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void ragDollOff()
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rb in ragDollRigidbodies)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;
        maincollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
