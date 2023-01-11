using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmers_respawn_HS : MonoBehaviour
{
    public GameObject prefab;
    public Transform spline_transform;
    private GameObject prefab_cache;
    public Ragdoll_controller_HS script;
    private bool isGone;

    public int destroytime=5;
    public int instansiatetime=6;

    private void Start()
    {
        respawnvehicles();
    }
    private void Update()
    {
        if((script.ishit== true) && !isGone)
        {
            isGone = true;
            Destroy(prefab_cache,destroytime);
            Invoke("respawnvehicles",instansiatetime);
        }
        else if(!isGone)
        {

            prefab_cache.transform.position = spline_transform.position;
            prefab_cache.transform.rotation = spline_transform.rotation;
        }
    }

    public void respawnvehicles()
    {
        prefab_cache = Instantiate(prefab, transform);
        prefab_cache.transform.parent = null;
        if(prefab_cache.transform.parent==null)
        {
            print("null");
        }
        script = prefab_cache.GetComponent<Ragdoll_controller_HS>();
        isGone = false;
    }
}
