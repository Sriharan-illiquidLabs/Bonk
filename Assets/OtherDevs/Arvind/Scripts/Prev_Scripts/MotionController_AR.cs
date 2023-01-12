using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController_AR : MonoBehaviour
{
    public Transform transform_spline;
    public GameObject car;
    public float timeToRespawn = 10f;

    private GameObject car_cache;
    private ObjectKnockOut_AR objectKnock;
    private Transform transform_cache;
    private bool isDestroyed = false;
    private void Start()
    {
        CreateCar();
    }
    private void LateUpdate()
    {
        if ((objectKnock.isHit || objectKnock.isBlasted) && !isDestroyed)
        {
            isDestroyed = true;
            Invoke("Respawn", timeToRespawn);
        }
        else if (!isDestroyed)
        {
            if (objectKnock.IsPathBlocked)
                return;

            car_cache.transform.position = transform_spline.position;
            car_cache.transform.rotation = transform_spline.rotation;
            car_cache.transform.forward = transform_spline.forward;
        }
        //Debug.Log("Hit: " + objectKnock.isHit + " Blast: " + objectKnock.isBlasted);
    }
    private void Respawn()
    {
        Destroy(car_cache);
        CreateCar();
       
    }
    private void CreateCar()
    {
        car_cache = Instantiate(car, transform);
        objectKnock = car_cache.GetComponent<ObjectKnockOut_AR>();
        isDestroyed = false;
    }
}
