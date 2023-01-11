using UnityEngine;

public class AnimalRagdollToggle_AD : MonoBehaviour
{
    [SerializeField]//particle system
    private ParticleSystem hitParticleFX;

    //sounds sources
    public AudioSource hitSound;
    public AudioSource walkSound;

    public float impactForce = 1000f;

    private Animator animalAnimator;
    private Rigidbody detectionRigidBody;
    private BoxCollider detectionBoxCollider;

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidBodies;

    [SerializeField]
    public bool hit;
    //these run before the onEnable method
    private void Awake()
    {
        animalAnimator = GetComponentInChildren<Animator>();
        detectionRigidBody = GetComponent<Rigidbody>();
        detectionBoxCollider = GetComponent<BoxCollider>();

        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
    }

    //this is so that the getcomponents can set in the awake method
    private void OnEnable()
    {
        RagDollToggle(false);
        hit = false;
    }

    public void RagDollToggle(bool active)
    {
        //the ragdoll colliders and rigidbody
        foreach (var cCollider in ragdollColliders)
        {
            cCollider.enabled = active;
        }
        foreach (var cRigidbody in ragdollRigidBodies)
        {
            cRigidbody.detectCollisions = active;
            cRigidbody.isKinematic = !active;
        }

        //audio triggers
        if (active)
        {
            walkSound.volume = 0;
            walkSound.Stop();
            hitSound.Play();
        }

        //for detection layer colliders and rigidbody
        animalAnimator.enabled = !active;
        detectionRigidBody.detectCollisions = !active;
        detectionRigidBody.isKinematic = active;
        detectionBoxCollider.enabled = !active;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            foreach (Rigidbody ragdollrb in ragdollRigidBodies)
            {
                hit = true;

                //Creating particle
                Instantiate(hitParticleFX, transform.position, hitParticleFX.transform.rotation);

                //rigidbody toggles
                ragdollrb.useGravity = true;
                ragdollrb.constraints = RigidbodyConstraints.None;
                ragdollrb.isKinematic = false;
                ragdollrb.mass = 0.5f;

                //force calculation
                var forceDirection = transform.position - collision.transform.position;
                float appliedForce = impactForce * 0.1f;
                ragdollrb.AddForce(new Vector3(forceDirection.x, forceDirection.y + 1.4f, forceDirection.z) * appliedForce);
            }
            RagDollToggle(true);
        }
    }
}
