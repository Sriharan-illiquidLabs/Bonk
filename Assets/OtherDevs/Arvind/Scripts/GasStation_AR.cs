using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation_AR : MonoBehaviour
{
    private bool isHit = false;

    //public bool nearByBlast = false;

    public ParticleSystem blastParticle;
    public ParticleSystem fuelFlamethrowerParticle;
    public Transform petrol_1;
    public Transform petrol_2;

    public GameObject stillObjectGS_1;
    public GameObject stillObjectGS_2;
    public GameObject fractureGS_1;
    public GameObject fractureGS_2;

    private AudioSource blastAudio;



    private void Start()
    {

        isHit = false;
        blastAudio = GetComponent<AudioSource>();
        //nearByBlast = false;

    }
    //private void FixedUpdate()
    //{
    //    if (nearByBlast)
    //    {
    //        Blast();
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /*&& Input.GetKey(KeyCode.LeftShift)*/)
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (!cheems || !cheems.IsBonking)
                return;

            cheems.RewardPlayer();

            Blast();
            BoxCollider[] selfColliders = GetComponents<BoxCollider>();
            foreach(BoxCollider collider in selfColliders)
            {
                collider.enabled = false;
            }
            //Destroy(stillObjectGS_1);
            //Destroy(stillObjectGS_2); 

            //Destroy(fractureGS_1,timeToDisappear);
            //Destroy(fractureGS_2, timeToDisappear);
        }
    }
    private void Blast()
    {
        if (!isHit)
        {
            stillObjectGS_1.SetActive(false);
            stillObjectGS_2.SetActive(false);
            Instantiate(fractureGS_1, stillObjectGS_1.transform.position, stillObjectGS_1.transform.rotation, stillObjectGS_1.transform.parent);
            Instantiate(fractureGS_2, stillObjectGS_2.transform.position, stillObjectGS_2.transform.rotation, stillObjectGS_2.transform.parent);

            Instantiate(blastParticle, petrol_1.position, blastParticle.transform.rotation, petrol_1.transform);
            Instantiate(blastParticle, petrol_2.position, blastParticle.transform.rotation, petrol_2.transform);

            Instantiate(fuelFlamethrowerParticle, petrol_1.position, blastParticle.transform.rotation, petrol_1.transform);
            Instantiate(fuelFlamethrowerParticle, petrol_2.position, blastParticle.transform.rotation, petrol_2.transform);

            if (blastAudio != null) blastAudio.Play();
        }

        isHit = true;

    }
}
