using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    public GameObject[] Humanprefabs;
    public GameObject[] Instantaiate_points;
    public float humanprefabSpeed;
    public void Instantiatepeople()
    {
        int spawnersID = Random.Range(0, Instantaiate_points.Length);

        GameObject SpawnObj = Humanprefabs[Random.Range(0, Humanprefabs.Length)];
        GameObject People = Instantiate(SpawnObj, Instantaiate_points[spawnersID].transform.position, transform.rotation);


        People.transform.parent = Instantaiate_points[spawnersID].transform;
        People.GetComponent<Animator>().enabled = true;
        People.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        People.AddComponent<BezierWalkerWithSpeed>().enabled = true;
        BezierWalkerWithSpeed bezierref = People.GetComponent<BezierWalkerWithSpeed>();
        bezierref.travelMode = TravelMode.Loop;
        bezierref.speed = 1;
        bezierref.NormalizedT = Instantaiate_points[spawnersID].GetComponent<Spawn_Properties>().gj_normalT;
        bezierref.spline = Instantaiate_points[spawnersID].GetComponent<Spawn_Properties>().gj_Spline;


    }
}