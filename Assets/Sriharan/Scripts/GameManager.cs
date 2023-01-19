using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //// Start is called before the first frame update
    public static GameManager instance;
   
    
    public GameObject cine;
 


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
