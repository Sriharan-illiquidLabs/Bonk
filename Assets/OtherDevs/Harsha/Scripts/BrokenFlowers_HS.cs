using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFlowers_HS : MonoBehaviour
{
    public GameObject BrokenpepperFlower;
    public GameObject peppereffect;
    public GameObject BrokenSunflower;
    public GameObject Sunflowereffect;
    public GameObject brokengrapeflower;
    public GameObject grapeflowereffect;
    public GameObject brokencornplant;
    public GameObject cornplanteffect;
    public GameObject brokencabbageflower;
    public GameObject cabbageflowereffect;
   
    private void Start()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "pepperflower_HS")
            {
                Destroy(gameObject);
                Instantiate(BrokenpepperFlower, transform.position, transform.rotation);
                Instantiate(peppereffect, transform.position, transform.rotation);
            }
            else if(gameObject.tag == "Sunflower_HS")
            {
                Destroy(gameObject);
                Instantiate(BrokenSunflower, transform.position, transform.rotation);
                Instantiate(Sunflowereffect, transform.position, transform.rotation);
            }
            else if(gameObject.tag== "Grapeflower_HS")
            {
                Destroy(gameObject);
                Instantiate(brokengrapeflower, transform.position, transform.rotation);
                Instantiate(grapeflowereffect, transform.position, transform.rotation);
            }
            else if(gameObject.tag =="Cornplant_HS")
            {
                Destroy(gameObject);
                Instantiate(brokencornplant, transform.position, transform.rotation);
                Instantiate(cornplanteffect, transform.position, transform.rotation);
            }
            else if(gameObject.tag =="Cabbageplant_HS")
            {
                Destroy(gameObject);
                Instantiate(brokencabbageflower, transform.position, transform.rotation);
                Instantiate(cabbageflowereffect, transform.position, transform.rotation);
            }

            // Update Coins when object is destroyed
            other.TryGetComponent<Cheems>(out Cheems cheems);

            if (cheems)
            {
                cheems.UpdateCoins(1);
            }
        }
    }
}
