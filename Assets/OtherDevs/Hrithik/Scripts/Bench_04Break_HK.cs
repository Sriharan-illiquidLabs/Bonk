using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench_04Break_HK : MonoBehaviour
{
    private GameObject _Bench04_Obj;
    private GameObject Dusteffect_particles;
    void Start()
    {
        _Bench04_Obj = GameManager.instance.Bench04_HK;
        Dusteffect_particles = GameManager.instance.Bench_DustParticles_HK;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //var magnitude = 500;
            //var force = transform.position - collision.transform.position;
            //gameObject. GetComponent<Rigidbody>().AddForce(new Vector3(force.x, force.y + 0.8f, force.z) * magnitude);

            var replacement = Instantiate(_Bench04_Obj, transform.position, transform.rotation);
            var Dusteffect = Instantiate(Dusteffect_particles, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(replacement, 3.5f);
            Destroy(Dusteffect, 3.5f);
        }
    }
}
