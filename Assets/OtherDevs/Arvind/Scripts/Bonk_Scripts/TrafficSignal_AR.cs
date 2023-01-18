using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignal_AR : MonoBehaviour
{
    public GameObject[] trafficPosts;
    public int trafficID;
    public float waitTime;
    private void Start()
    {
        trafficID = 0;
        InvokeRepeating(nameof(trafficSwitch), 0, waitTime);
    }
    GameObject currentTrafficPost;
    void trafficSwitch()
    {
        //turnON all the stops
        for(int i=0;i<trafficPosts.Length;i++)
        {
            currentTrafficPost = trafficPosts[i];
            currentTrafficPost.transform.position = new Vector3(currentTrafficPost.transform.position.x,
                                                                0,
                                                                currentTrafficPost.transform.position.z);
        }
        //turnOffOne
        trafficPosts[trafficID].transform.position = new Vector3(trafficPosts[trafficID].transform.position.x,
                                                                10,
                                                                trafficPosts[trafficID].transform.position.z);
        if(trafficID==trafficPosts.Length-1) 
        {
            trafficID = 0;
        }
        else
        {
            trafficID++;
        }

    }
}

