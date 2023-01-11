using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_Interactor : MonoBehaviour
{
    public Transform pickupPoint;
    public bool object_detected;
    private bool pickedUp;
    private float launchVelocity = 1.5f;
    private float UpVelocity = 1.5f;
    private int Object_Count = 0;
    public LayerMask mask;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {      
            if (collision.gameObject.tag == "Player_HK")
            {
                object_detected = true;
            }
    }
    private void OnCollisionExit(Collision collision)
    {
        object_detected = false;
        pickedUp = false;
    }
    void Start()
    {
        object_detected = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    // Update is called once per frame
    void Update()
    {

        if (object_detected == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
            Object_Count++;
            this.gameObject.transform.position = pickupPoint.position;
            this.gameObject.transform.localPosition = Vector3.zero;
            this.gameObject.transform.localEulerAngles = new Vector3(0, 180, 90);
          //this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
            Pickup();
            pickedUp = true;
            }
            //else if (Object_Count>=0)
            //{
            //    Debug.Log("Already Holding");
            //}
         
        }

        if(pickedUp==true&& Input.GetKey(KeyCode.F))
        {
            throw_obj();
            object_detected=false;
        }
    }

    private void Pickup()
    {
      
        pickedUp = true;
        transform.parent = pickupPoint.transform;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<BoxCollider>().enabled = false;
    }
    private void throw_obj()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().AddForce(pickupPoint.up * UpVelocity, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(pickupPoint.forward * launchVelocity, ForceMode.Impulse);
        GetComponent<BoxCollider>().enabled = true;
    }
}
