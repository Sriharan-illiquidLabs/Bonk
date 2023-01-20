using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MotionController_AR : MonoBehaviour
{
    public Transform transform_spline;
    public GameObject car;
    public float timeToRespawn = 10f;
    public float radius=5;
    private GameObject car_cache;
    private ObjectKnockOut_AR objectKnock;
    private Transform transform_cache;
    private bool isDestroyed = false;

    private void Start()
    {
        //car_cache = Instantiate(car, transform);
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
        ///This if statement is a hack where the collider object is moved up so that the onTriggerExit in the Car_Detect_AR script can work :P
        if (car_cache.gameObject.transform.GetChild(0).GetComponent<BoxCollider>())
        {
            car_cache.gameObject.transform.GetChild(0).transform.Translate(Vector3.up * 10);
            //Debug.Log("Found Box");
        }
        Destroy(car_cache,0.2f);
        //Invoke("DisableCar", 0.2f);
        CreateCar();

    }
    //void DisableCar()
    //{
    //    car_cache.SetActive(false);
    //    CreateCar();
    //}
    private void CreateCar()
    {
        car_cache = Instantiate(car, transform);
        //car_cache.SetActive(true);
        objectKnock = car_cache.GetComponent<ObjectKnockOut_AR>();
        //objectKnock.ResetCar();
        isDestroyed = false;
    }

}
