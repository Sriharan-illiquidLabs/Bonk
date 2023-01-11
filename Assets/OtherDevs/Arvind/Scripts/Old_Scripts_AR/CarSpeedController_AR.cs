using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController_AR : MonoBehaviour
{
    public string tagString;
    public float time = 0.5f;
    private BezierWalkerWithSpeed agent;
    private bool stop;

    private float speed;

    private void Start()
    {

        if (GetComponentInParent<BezierWalkerWithSpeed>())
        {
            agent = GetComponentInParent<BezierWalkerWithSpeed>();
        }
        else
        {
            agent = GetComponent<BezierWalkerWithSpeed>();
        }
        speed = agent.speed;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagString) && other.gameObject.layer == this.gameObject.layer)
        {
            agent.speed = 0;
        }
        
    } 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagString) && other.gameObject.layer == this.gameObject.layer)
        {
            Invoke("Release", time);
        }
       
    }
    private void Release()
    {
        agent.speed = speed;
    }
    //#region TEST

    //public void OnPathCompleted()
    //{
    //    agent.NormalizedT = 0;
    //    int splineIndex = Random.Range(0, splines.Length);
    //    agent.spline = splines[splineIndex];
    //}
    //#endregion
}
