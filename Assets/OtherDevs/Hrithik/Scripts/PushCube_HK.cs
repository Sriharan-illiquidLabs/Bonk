using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCube_HK : MonoBehaviour
{
    //Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            var magnitude = 300;
            var force = transform.position - collision.transform.position;
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(force.x,force.y + 0.8f ,force.z) * magnitude);
          
        };

        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
