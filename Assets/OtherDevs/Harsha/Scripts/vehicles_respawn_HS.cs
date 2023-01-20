using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicles_respawn_HS : MonoBehaviour
{
    public GameObject parentobject;
    public GameObject prefab;
    public Transform spline_transform;
    private GameObject prefab_cache;
    private bool isGone;

    public int destroytime=5;
    public int instansiatetime=6;

    public ObjectKnockOut_HS objectscript;

    public BezierWalkerWithSpeed spline;

    public bool idle;

    private void Start()
    {
        if(!idle)
            spline = GetComponentInParent<BezierWalkerWithSpeed>();

       
        respawnvehicles();
    }

    /*private void Update()
    {
        if ((objectscript.isHit == true) && !isGone)
        {
            isGone = true;
            Destroy(prefab_cache, destroytime);
            Invoke("respawnvehicles", instansiatetime);
        }
        else if (!isGone)
        {
            if (objectscript.IsPathBlocked)
                return;

            prefab_cache.transform.position = spline_transform.position;
            prefab_cache.transform.rotation = spline_transform.rotation;
        }
    }
    */
    public void respawnvehicles()
    {
        prefab_cache = Instantiate(prefab, transform);
        if(!idle)
            spline.speed = 1;
        isGone = false;
    }

    public void checkcollision()
    {
        
        if (/*(objectscript.isHit == true) && */ !isGone)
        {
            Debug.Log("calling");
            isGone = true;
            Destroy(prefab_cache, destroytime);
            Invoke("respawnvehicles", instansiatetime);
            //parentobject.transform.DetachChildren();
            spline.speed = 0;
        }
        else if (!isGone)
        {
            if (objectscript.IsPathBlocked)
             return;
            
        }
    }
}
