using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn_Properties : MonoBehaviour
{
    public static Spawn_Properties Sp_Instance;
    public float look_radius = 5;
    public BezierSpline gj_Spline;
    public float gj_normalT;
    public bool isDead;
    //public Transform gj_pos;
    //public bool isClear = false;
    public bool canbeSpawned;
    //private Ragdoll_controller rg_controller;
    // Start is called before the first frame update

    private void Awake()
    {
        Sp_Instance= this;
    }
    void Start()
    {
        gj_Spline = gameObject.GetComponent<BezierWalkerWithSpeed>().Spline;
        gj_normalT = gameObject.GetComponent<BezierWalkerWithSpeed>().NormalizedT;
    }
    Collider[] People;
    void Update()
    {
        People = Physics.OverlapSphere(transform.position, look_radius,LayerMask.GetMask("grabPeople"));

        if(People.Length == 0)
        {
            canbeSpawned = true;
        }
        else
        {
            canbeSpawned= false;
        }
      
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, look_radius);
    }
   
}
