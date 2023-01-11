using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_Arrow_HS : MonoBehaviour
{
    public Transform[] targetdestination;
    private Vector3 targetpostion;
    public int indexval = 0;
    float distancebw1;
    float distancebw2;
    float distancebw3;
    public float rotspeed = 0.1f;
    Quaternion rotgoal;
    Vector3 direction;
    void Update()
    {
        distancebw1 = Vector3.Distance(transform.position, targetdestination[indexval].position);
        distancebw2 = Vector3.Distance(transform.position, targetdestination[indexval + 1].position);
        distancebw3 = Vector3.Distance(transform.position, targetdestination[indexval + 2].position);
        var minpoint = Mathf.Min(distancebw1, distancebw2, distancebw3);
        if (minpoint == distancebw1)
        {
            targetpostion = targetdestination[indexval].transform.position;
            direction = (targetpostion - transform.position).normalized;
            rotgoal = Quaternion.LookRotation(direction);
            rotgoal = new Quaternion(0, rotgoal.y, 0, rotgoal.w);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotgoal, rotspeed);
        }
        else if (minpoint == distancebw2)
        {
            targetpostion = targetdestination[indexval + 1].transform.position;
            direction = (targetpostion - transform.position).normalized;
            rotgoal = Quaternion.LookRotation(direction);
            rotgoal = new Quaternion(0, rotgoal.y, 0, rotgoal.w);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotgoal, rotspeed);
        }
        else if (minpoint == distancebw3)
        {
            targetpostion = targetdestination[indexval + 2].transform.position;
            direction = (targetpostion - transform.position).normalized;
            rotgoal = Quaternion.LookRotation(direction);
            rotgoal = new Quaternion(0, rotgoal.y, 0, rotgoal.w);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotgoal, rotspeed);
        }
    }
}
