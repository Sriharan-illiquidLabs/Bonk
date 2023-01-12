using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOff_AR : MonoBehaviour
{
    private bool isHit = false;
    public bool isExplosive = false;
    public float radius;
    public float upwardModifier=50;
    public float magnitude=50;
    public float explosionForce;
    public float timeToDisappear = 50;
    public bool effectNearByObjects = true;

    private void Start()
    {
        isHit = false;
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /*&& Input.GetKey(KeyCode.LeftShift)*/)
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            isHit = true;
            if (effectNearByObjects)
            {
                Debug.Log("I executed! , effectNearByObjects: " + effectNearByObjects);
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                foreach (var nearObjects in colliders)
                {
                    Rigidbody rb = nearObjects.GetComponent<Rigidbody>();
                    ObjectKnockOut_AR burn = nearObjects.gameObject.GetComponent<ObjectKnockOut_AR>();
                    Ragdoll_controller ragdoll_Controller = nearObjects.gameObject.GetComponent<Ragdoll_controller>();
                    //GasStation_AR gasStation_AR = nearObjects.gameObject.GetComponent<GasStation_AR>();
                    if (rb != null)
                    {
                        
                        if (nearObjects.gameObject != gameObject)
                        {
                            if (burn != null)
                            {
                                burn.isBlasted = true;
                            }
                            if (ragdoll_Controller != null)
                            {
                                ragdoll_Controller.ragDollOn();
                                //ragdoll_Controller?.AddRigidBodyForce(transform, 70f);
                            }

                            if (isExplosive)
                            {
                                //rb.AddExplosionForce(explosionForce, transform.position, radius, upwardModifier);
                            }
                        }
                    }
                }
            }
            else
            {

                //var force = collision.transform.position - transform.position;
                //collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude,ForceMode.VelocityChange);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
