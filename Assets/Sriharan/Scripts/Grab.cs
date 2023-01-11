using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playertransform;

    public static bool pickUp;

    Rigidbody rb;

    bool ok;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F) && ok )
        {
            transform.parent = null;
            rb.isKinematic = false;
            rb.velocity = transform.forward * 5f + new Vector3(0f, 7f, 0f);
            LeanTween.delayedCall(0.2f, ColOn);
            ok = false;
            pickUp = false;
        }
    }

    void ColOn()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(!pickUp)
            {
                GameObject playerGoat = collision.collider.gameObject;
                ok = true;
                rb.isKinematic = true;
                GetComponent<BoxCollider>().isTrigger = true;
                transform.parent = playerGoat.transform;
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
                transform.position = playertransform.position;
                LeanTween.rotateLocal(gameObject, new Vector3(0f, 0f, 90f), 0.1f);
                pickUp = true;
            }
            
           
        }
    }
}
