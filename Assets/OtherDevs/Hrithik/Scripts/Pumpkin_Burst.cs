using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_Burst : MonoBehaviour
{
    private GameObject _Broken_Obj;
    private GameObject _Pumpkin_Particles;

  
    void Start()
    {
        //iskinamaticON = false;
        _Broken_Obj = GameManager.instance.Pumpkin_HK;
        _Pumpkin_Particles = GameManager.instance.Pumpkin_Particles_HK;
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Update Coins when object is destroyed
            collision.gameObject.TryGetComponent<Cheems>(out Cheems cheems);

            if (cheems)
            {
                cheems.UpdateCoins(1);
            }

            var replacement = Instantiate(_Broken_Obj, transform.position, transform.rotation);
            var effect = Instantiate(_Pumpkin_Particles, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(replacement, 3.5f);
            Destroy(effect, 2.5f); 
        }
    }

   
}

