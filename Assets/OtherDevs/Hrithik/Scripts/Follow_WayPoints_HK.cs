using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_WayPoints_HK : MonoBehaviour
{
    public GameObject[] Waypoints;
    private int Current_wayPoint_index;
    public float Walk_speed =1;
    public float Rotation_speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

       Walk_speed = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(this.transform.position , Waypoints[Current_wayPoint_index].transform.position) < 2 )
        {
            Current_wayPoint_index++;
        }
        
        if(Current_wayPoint_index >= Waypoints.Length)
        {
            Current_wayPoint_index = 0;
        }
        
        //this.transform.LookAt(Waypoints[Current_wayPoint_index].transform);

        Quaternion lookatWaypoint  = Quaternion.LookRotation(Waypoints[Current_wayPoint_index].transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWaypoint, Rotation_speed*Time.deltaTime);

        this.transform.Translate(0, 0, Walk_speed * Time.deltaTime);
    }
  
}
