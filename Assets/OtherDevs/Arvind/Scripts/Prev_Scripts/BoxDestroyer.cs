using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{
    private Rigidbody[] pieces;
    private GameObject boxFracture_cache;
    
    public GameObject box;
    public GameObject boxFracture;
    public float pieceFallRate;
    public float pieceFallRadius;
    public BoxCollider coinCollider;
    public bool isHit=false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isHit = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<BoxCollider>().enabled = false;
            box.SetActive(false);
            Invoke("CoinOn", 0.5f);

            if(boxFracture != null)
            {
                boxFracture_cache = Instantiate(boxFracture, transform);

                pieces = boxFracture_cache.GetComponentsInChildren<Rigidbody>();

                if (pieces != null)
                {
                    for (int i = 0; i < pieces.Length; i++)
                    {
                        pieces[i].AddExplosionForce(pieceFallRate, transform.position, pieceFallRadius);
                    }
                }
            }

            if (GetComponentInParent<SkyDrop>())
            {
                GetComponentInParent<SkyDrop>().antiCount++;
            }

        }
    }
    void CoinOn()
    {
        coinCollider.enabled = true;
    }
}
