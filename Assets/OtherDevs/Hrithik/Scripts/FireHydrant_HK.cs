using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant_HK : MonoBehaviour
{

    private GameObject _Water_Obj;
    private bool isflowing;
    //[SerializeField] private GameObject _Pumpkin_Particles;
    void Start()
    {
        isflowing = false;
        _Water_Obj = GameManager.instance.water_Particles_HK;
        //_Pumpkin_Particles = GameManager.instance.Pumpkin_Particles_HK;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            var magnitude = 500;
            var force = transform.position - collision.transform.position;
            GetComponent<Rigidbody>().AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);
            if (!isflowing)
            {
                var replacement = Instantiate(_Water_Obj, transform.position, 
                    transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
                isflowing = true;
            }
        
        }
       
    }
}
