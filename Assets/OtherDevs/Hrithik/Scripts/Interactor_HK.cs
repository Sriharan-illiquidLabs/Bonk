using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Interactor_HK : MonoBehaviour
{
    [Header ("Interaction properties")]
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private int _numFound;
    [SerializeField] private int _speed;
    [Header("UI Elements")]
    public TextMeshProUGUI _ObjectText;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] GameObject text_Image;
    public LayerMask mask;
    public ParticleSystem Obj;

    void Start()
    {
        text_Image.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & mask) != 0)
        {
           
            _ObjectText.text = collision.gameObject.tag.ToString();
            text_Image.SetActive(true);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce( * _speed, ForceMode.Impulse);
        }
        else if (collision.gameObject.layer == 7)
        {

            _ObjectText.text = "E - PICKUP".ToString();
            text_Image.SetActive(true);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
      
        text_Image.SetActive(false);
    }
    private void FixedUpdate()
    {  
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);   
     }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * _interactionPointRadius;
        
    }
}
