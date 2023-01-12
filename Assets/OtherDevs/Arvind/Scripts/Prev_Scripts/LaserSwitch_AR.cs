using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch_AR : MonoBehaviour
{
   //public Transform transform;
    public GameObject ParticleFX;
    public float laserLength=10f;
    public LayerMask hitLayers;

    LineRenderer LineRender;
    RaycastHit hit;

    private void Start()
    {
        LineRender = GetComponent<LineRenderer>();
        LineRender.enabled = false;
        LineRender.SetPosition(0, transform.position);
    }
    private void Update()
    {
        LineRender.SetPosition(0, transform.position);

        if (Input.GetKey(KeyCode.L))
        {
            LineRender.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            LineRender.enabled = false;
        }

        //laser = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserLength,hitLayers) 
            && Input.GetKey(KeyCode.L))
        {
            if (hit.collider)
            {
                LineRender.SetPosition(1, hit.point);
                Instantiate(ParticleFX, hit.point, Quaternion.identity);
            }
        }
        else LineRender.SetPosition(1, transform.position+transform.forward * laserLength);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * laserLength);
    }
}
