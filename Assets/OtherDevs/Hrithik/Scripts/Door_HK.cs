using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Door_HK : MonoBehaviour
{
    public float smooth =12;
    private Quaternion DoorOpen;
    private Quaternion DoorClosed;
    public GameObject Door;
    public bool isdoorOpen;
    public TextMeshProUGUI _DoorText;
    [SerializeField] GameObject text_Image;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Player_HK")
        {
          
                isdoorOpen = true;
            text_Image.SetActive(true);
            _DoorText.text = "F - Close".ToString();
            DoorOpen = Door.transform.rotation = Quaternion.Euler(0, 90, 0);
                DoorClosed = Door.transform.rotation;
                Door.transform.rotation = Quaternion.Lerp(DoorClosed, DoorOpen, Time.deltaTime * smooth * Time.deltaTime);
            
               
        }
    }
    private void Start()
    {
        text_Image.SetActive(false);
    }
    private void Update()
    {
        if(isdoorOpen==true && Input.GetKey(KeyCode.F))
        {
            isdoorOpen=false;
            DoorOpen = Door.transform.rotation = Quaternion.Euler(0, 0, 0);
            DoorClosed = Door.transform.rotation;
            Door.transform.rotation = Quaternion.Lerp(DoorClosed, DoorOpen, Time.deltaTime * smooth * Time.deltaTime);
        }
    }
}
