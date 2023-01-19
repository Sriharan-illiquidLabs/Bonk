using BezierSolution;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ObjectKnockOut_HS : MonoBehaviour
{
    public vehicles_respawn_HS vechscript;

    [Header("Car Parameters")]
    [SerializeField]
    private bool isThisCar = true;

    [SerializeField]
    private float burnTimeDivisor = 2;

    [SerializeField]
    private MeshRenderer[] carMaterials;

    [SerializeField]
    private Wheels_Coins[] wheels_Coins;


    private float burnSlider = 1f;

    private Color burnColor;

    [Header("General Parameters")]
    [SerializeField]
    private ParticleSystem hitParticleFX;

    //[SerializeField]
    public bool isHit = false;

    [HideInInspector]
    public bool isBlasted = false;

    private AudioSource hitAudio;

    private Rigidbody rb;

    private BezierWalkerWithSpeed controller;
    private bool isEventDone= false;

    private bool isPathBlocked = false;
    public bool IsPathBlocked { get { return isPathBlocked; } }

    public ParticleSystem parti;

    bool ok;

    private void Start()
    {

        vechscript=GetComponentInParent<vehicles_respawn_HS>();
        
        if (isThisCar)
        {
            carMaterials = GetComponentsInChildren<MeshRenderer>();
            wheels_Coins = GetComponentsInChildren<Wheels_Coins>();
        }
        
        controller = GetComponent<BezierWalkerWithSpeed>();
        hitAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        isEventDone = false;
    }
    private void FixedUpdate()
    {
        if (isBlasted || isHit)
        {
            //Event Checking.
            if (!isEventDone)
            {
                EventFunction();
                isEventDone = true;
            }

            if (isThisCar && burnSlider > 0.0f)
            {
                burnSlider -= Time.deltaTime / burnTimeDivisor;
                burnColor = new Color(burnSlider, burnSlider, burnSlider);

                foreach (MeshRenderer renderer in carMaterials)
                {
                    renderer.material.color = burnColor;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("impactObject"))
        {
            isPathBlocked = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (cheems.IsBonking && !ok && !isHit)
            {
                cheems.RewardPlayer();

                Instantiate(parti, transform.position + new Vector3(0f, 2.7f, 0f), Quaternion.identity);
                ok = true;
                isHit = true;
                vechscript.checkcollision();
            }       
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            
        }
        else if (collision.gameObject.CompareTag("impactObject"))
        {
            Invoke(nameof(RestartVehicle), 1f);
        }

    }

    void RestartVehicle()
    {
        isPathBlocked = false;
    }

    private void EventFunction()
    {
        //Release Rigidbody Contraints.
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;

        //Functions for car objects.
        if (isThisCar)
        {
            foreach (Wheels_Coins wheels in wheels_Coins)
            {
                wheels.rotationSpeed = 0;
            }
        }

        //Spline detaching.
        if (controller != null)
        {
            controller.enabled = false;
        }

        //Audio play.
        if(hitAudio != null)
        {
            hitAudio.Play();
        }
        
        //Create Particle.
        Instantiate(hitParticleFX,transform.position,hitParticleFX.transform.rotation);
    }
}
