using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDrop : MonoBehaviour
{
    public GameObject box;
    public float startTime;
    public float repeatRate;
    public int noOfInstances;
    private int count=0;
    public float antiCount=0;
    private void Start()
    {
        Invoker();
    }
    private void FixedUpdate()
    {
        if (count >= noOfInstances)
        {
            CancelInvoke("launchBox");
        }

        if(antiCount >= noOfInstances)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject,startTime-2);
            }
            antiCount = 0;
            count = 0;
            Invoker();
        }
       
    }
    private void Invoker()
    {
        InvokeRepeating("launchBox", startTime, repeatRate);
    }
    private void launchBox()
    {
        var randomizerX = Random.Range(-2.5f, 2.5f);
        var randomizerZ = Random.Range(-2.5f, 2.5f);
        var vecOffset = new Vector3(randomizerX,0,randomizerZ);
        Instantiate(box,transform.position + vecOffset,Quaternion.identity,transform);
        count++;
    }
}
