using BezierSolution;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ObjectKnockOut_AR : MonoBehaviour
{

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
    private bool isEventDone=false;

    private bool isPathBlocked = false;
    public bool IsPathBlocked { get { return isPathBlocked; } }

    bool ok;

    public ParticleSystem parti;

    //[SerializeField]
    //private MeshRenderer carBodyMaterial;
    //[SerializeField]
    //private MeshRenderer wheel_1;
    //[SerializeField]
    //private MeshRenderer wheel_2;
    //[SerializeField]
    //private MeshRenderer wheel_3;
    //[SerializeField]
    //private MeshRenderer wheel_4;


    private void Start()
    {
        if (isThisCar)
        {
            carMaterials = GetComponentsInChildren<MeshRenderer>();
            wheels_Coins = GetComponentsInChildren<Wheels_Coins>();
            hitAudio = GetComponent<AudioSource>();
        }
        
        controller = GetComponent<BezierWalkerWithSpeed>();
        
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
            //for(int i = 0; i < carMaterials.Length-1; i++)
            //{
            //    carMaterials[i].material.color = burnColor;
            //}

            //carMaterials[0].material.color = burnColor;
            //carMaterials[1].material.color = burnColor;
            //carMaterials[2].material.color = burnColor;
            //carMaterials[3].material.color = burnColor;
            //carMaterials[4].material.color = burnColor;
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            //if (!cheems)
            //    return;
          

            if (cheems.IsBonking && !ok && !isHit)
            {
                cheems.RewardPlayer();

                Instantiate(parti, transform.position + new Vector3(0f, 2.3f, 0f), Quaternion.identity);
                ok = true;
            }
            
            isHit = true;

            //if (!cheems.IsBonking)
            //{
            //    isPathBlocked = true;
            //    return;
            //}
            //else
            //{
            //    if(!ok)
            //        cheems.RewardPlayer();
            //        ok = true;

            //    isHit = true;
            //}
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            //if (!cheems)
            //    return;

            if (!cheems.IsBonking)
            {
                Invoke(nameof(RestartVehicle), 1f);
                return;
            }
        }
    }

    void RestartVehicle()
    {
        isPathBlocked = false;
    }

    //Function to call for an event taking place only once in its lifetime.
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
            //Audio play.
            if (hitAudio != null)
            {
                hitAudio.Pause();
            }
        }

        //Spline detaching.
        //if (controller != null)
        //{
        //    controller.enabled = false;
        //}
        //Create Particle.
        Instantiate(hitParticleFX,transform.position,hitParticleFX.transform.rotation);
    }
}
