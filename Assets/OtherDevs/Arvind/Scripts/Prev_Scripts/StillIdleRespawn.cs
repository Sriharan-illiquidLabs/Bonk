using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillIdleRespawn : MonoBehaviour
{

    //public Transform transform_spline;
    public GameObject car;
    public float timeToRespawn = 10f;

    private GameObject car_cache;
    private ObjectKnockOut_AR objectKnock;
    private Wheels_Coins_AR[] wheelStop;
    //private Transform transform_cache;
    private bool isDestroyed = false;
    private void Start()
    {
        CreateCar();
        //objectKnock = GetComponent<ObjectKnockOut_AR>();
    }
    private void LateUpdate()
    {
        if ((objectKnock.isHit || objectKnock.isBlasted) && !isDestroyed)
        {
            isDestroyed = true;

            Destroy(car_cache, timeToRespawn - 1);
            Invoke("CreateCar", timeToRespawn);
        }
        //else if (!isDestroyed)
        //{
        //    car_cache.transform.position = transform_spline.position;
        //    car_cache.transform.rotation = transform_spline.rotation;
        //    car_cache.transform.forward = transform_spline.forward;
        //}
        //Debug.Log("Hit: " + objectKnock.isHit + " Blast: " + objectKnock.isBlasted);
    }
    //private void Respawn()
    //{
    //    CreateCar();


    //}
    private void CreateCar()
    {
        car_cache = Instantiate(car, transform);
        objectKnock = car_cache.GetComponent<ObjectKnockOut_AR>();

        wheelStop = car_cache.GetComponentsInChildren<Wheels_Coins_AR>();
        for (int i = 0; i < wheelStop.Length; i++)
        {
            wheelStop[i].enabled = false;
        }
        //objectKnock.StopWheels();

        if (car_cache.GetComponent<AudioSource>() != null)
        {
            car_cache.GetComponent<AudioSource>().Stop();
            car_cache.GetComponent<AudioSource>().enabled = false;
        }

        isDestroyed = false;
    }
}

