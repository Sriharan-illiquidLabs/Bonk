using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_AD : MonoBehaviour
{

    public int playerSpeed;
    public int rotationSpeed;

    public TMP_Text coinCount;
    public int coinValue = 0;

    public ParticleSystem coinParticle;
    void Start()
    {

    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        transform.Rotate(0f,Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0f);
        
        if(vertical != 0f)
        {
            transform.position += transform.forward * vertical * playerSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("coin_AD"))
        {
            Destroy(collision.gameObject);
            coinValue++;
            Instantiate(coinParticle,collision.collider.transform.position,Quaternion.identity);
            coinCount.text = coinValue.ToString();
        }
    }
}
