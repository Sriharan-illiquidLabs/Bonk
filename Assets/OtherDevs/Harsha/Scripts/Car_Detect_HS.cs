using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Car_Detect_HS : MonoBehaviour
{
    BezierWalkerWithSpeed bezierWalker;
    bool isToStop;
    float carSpeed;
    BoxCollider boxCollider;

    private void OnValidate()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
          
        boxCollider = GetComponent<BoxCollider>();
        bezierWalker = GetComponent<BezierWalkerWithSpeed>();
        carSpeed = bezierWalker.speed;
        boxCollider.isTrigger = true;
    }
    private void Update()
    {
        //carsAndPlayer = Physics.OverlapBox(boxCollider.center + transform.position, boxCollider.size/2, transform.rotation, mask);
        //if(carsAndPlayer.Length > 0 )
        //{
        //    bezierWalker.speed = 0.0000001f;
        //}
        //else if (bezierWalker.speed < carSpeed)
        //{
        //    bezierWalker.speed = carSpeed;
        //}
    }
  

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Car_AR"))
        {
            isToStop = true;
            bezierWalker.speed = 0.0000001f * Mathf.Sign(carSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Car_AR"))
        {
            isToStop = false;
            bezierWalker.speed = carSpeed;
        }
    }

}
