using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject Pumpkin_HK;
    public GameObject Pumpkin_Particles_HK;
    public GameObject Bench03_HK;
    public GameObject Bench04_HK;
    public GameObject water_Particles_HK;
    public GameObject BloodParticles_HK;
    public GameObject BloodParticlesFemale_HK;
    public GameObject Bench_DustParticles_HK;
    public GameObject Glass_Particles_HK;
    public AudioSource Glass_Sound_HK;
    public GameObject BrokenPepperflower_HS;
    public GameObject BrokenSunflower_HS;
    public GameObject Brokengrapeflower_HS;
    public GameObject Brokencornplant_HS;
    public GameObject brokencabbageplant_HS;
    public Respawning rp;
    public Ragdoll_controller rc;
    public Spawn_Properties sp;

    public GameObject cine;
    public TMP_Text coinCount;


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
