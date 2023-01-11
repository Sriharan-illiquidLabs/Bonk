using BezierSolution;
using UnityEngine;

public class AnimalRespawn_AD : MonoBehaviour
{
    [SerializeField]
    AnimalRagdollToggle_AD animalRagdollToggle;

    private bool triggerOnce = false;

    public GameObject splineObject;
    public GameObject animalObject;
    public GameObject animalPrefab;

    [Header("Time Setting")]
    float TimetoDestroy = 7;
    float TimetoRespawn = 12;

    //this runs after the OnEnable and awake methods in the animalragdolltoggle script
    private void Start()
    {
        RespawnAnimal();
    }

    private void Update()
    {
        if (!animalRagdollToggle.hit)
        {
            animalObject.transform.position = splineObject.transform.position;
            animalObject.transform.rotation = splineObject.transform.rotation;
        }
        else if(triggerOnce)
        {
            triggerOnce = false;
            Destroy(animalObject, TimetoDestroy);
            Invoke("RespawnAnimal", TimetoRespawn);
        }
    }

    void RespawnAnimal()
    {
        triggerOnce = true;

        animalObject = Instantiate(animalPrefab,splineObject.transform.position
            ,Quaternion.identity,transform);
        animalRagdollToggle = animalObject.GetComponent<AnimalRagdollToggle_AD>();
    }
}
