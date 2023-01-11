using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HK : MonoBehaviour
{
    public float aI_SPEED;
    // Start is called before the first frame update
    void Start()
    {
        aI_SPEED = Random.Range(1f, 1.8f);
    }

    // Update is called once per frame
    void Update()
    {
       Update_speed();
    }
    public void Update_speed()
    {
        this.GetComponent<BezierWalkerWithSpeed>().speed = aI_SPEED;
    }
}
