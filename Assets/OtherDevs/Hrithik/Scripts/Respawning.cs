using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    public GameObject[] Humanprefabs;
    public float humanprefabSpeed;
    public float humanprefabNormalT;
    public BezierSpline humaprefabSpline;
    public Vector3 instantiatepos;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    public void Instantiatepeople()
    {
        GameObject SpawnObj = Humanprefabs[Random.Range(0, Humanprefabs.Length)];
        GameObject People = Instantiate(SpawnObj, instantiatepos, transform.rotation);
        People.transform.parent = transform;
        People.GetComponent<Animator>().enabled = true;
        People.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        People.AddComponent<Ragdoll_controller>().enabled = true;
        People.AddComponent<BezierWalkerWithSpeed>().enabled = true;
        People.GetComponent<BezierWalkerWithSpeed>().travelMode = TravelMode.Loop;
        People.GetComponent<BezierWalkerWithSpeed>().NormalizedT = humanprefabNormalT;
        People.GetComponent<BezierWalkerWithSpeed>().spline = humaprefabSpline;
        People.GetComponent<BezierWalkerWithSpeed>().speed = humanprefabSpeed;



    }
}