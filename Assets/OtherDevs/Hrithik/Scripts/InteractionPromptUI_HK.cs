using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptUI_HK : MonoBehaviour
{
    private Camera _maincam;
    // Start is called before the first frame update
    void Start()
    {
        _maincam = Camera.main;
     
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
       var rotation = _maincam.transform.rotation;
        transform.LookAt(transform.position +rotation * Vector3.forward, rotation * Vector3.up);          
    } 
}
