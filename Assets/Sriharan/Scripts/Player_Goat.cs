 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RopeMinikit;
using Unity.Mathematics;


public class Player_Goat : MonoBehaviour
{
    public LayerMask mask;

    public GameObject ton;
    public GameObject rTongue;
    public GameObject currentGrab;
    
    
    public GameObject rocket;
    public GameObject[] rocketParti;

    public List<GameObject> bills = new List<GameObject>();

    public Transform vStartPos;
    public Transform vEndpos;
    public Transform billSpawnPoint;
    public Transform cam;

    Vector3[] points;

    public RopeConnection rope;
    public Rope rp;

    public Material tMat;

    public float playerSpeed;
    public float playerSideways;
    public float rotationSpeed;
    public float height;
    public float minHeight;
    public float maxHeight;
    public float minDistance;
    public float maxDistance;
    float mg;

    public int coinValue = 0;
    public int numberOfPoints = 30;

    public TMP_Text coinCount;

    public ParticleSystem coinParticle;

    Rigidbody rb;

    [SerializeField]
    Animator anim;

    bool jump;
    bool roc;
    bool billZone;



    State currentState = State.Idle;

    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
       
       
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 input = new Vector3(0f, horizontal, vertical);

      
        if(Input.GetKey(KeyCode.R) )
        {
            if(!roc)
            {
                roc = true;
                rocketParti[0].SetActive(true);
                rocketParti[1].SetActive(true);
                rocket.SetActive(true);
            }
            SetState(State.Idle);
            rb.AddForce(transform.forward * 10f + new Vector3(0f, 30f, 0f), ForceMode.Impulse);

        }
        else
        {
            if(roc && transform.localPosition.y < -7.5f)
            {
                roc = false;
                rocketParti[0].SetActive(false);
                rocketParti[1].SetActive(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && !jump && horizontal == 0f && !roc)
        {
            jump = true;
            
            
            if (currentState != State.Run)
            {
                SetState(State.Jump);
                LeanTween.delayedCall(0.3f, () =>
                {
                    JumpNow(vertical, false);
                });
                LeanTween.delayedCall(1f, JumActi);

            }
            else
            {
                SetState(State.Jump);
                LeanTween.delayedCall(0.3f, () =>
                {
                    JumpNow(vertical, true);
                });
                LeanTween.delayedCall(1.35f, JumActi);


            }

           


        }

        if (input != Vector3.zero && !jump && !roc)
        {

            if (vertical != 0f)
            {
                Quaternion target = Quaternion
                .Euler(transform.localEulerAngles.x, cam.localEulerAngles.y,
                transform.localEulerAngles.z);

                transform.rotation = Quaternion
                .Slerp(transform.rotation, target,
                10f * Time.deltaTime);

                if (Input.GetKey(KeyCode.LeftShift) && vertical > 0f /*&& horizontal == 0f*/)
                {
                    playerSpeed = 8.5f;
                    SetState(State.Run);
                }
                else
                {
                    playerSpeed = 4f;
                    SetState(State.Walk);
                }
                transform.position += transform.forward * vertical * playerSpeed * Time.deltaTime;
            }
            if (horizontal != 0f)
            {
                if (currentState != State.Idle && vertical > 0f)
                {
                    transform.position += transform.right * horizontal * playerSideways * Time.deltaTime;
                }
                else
                {
                    //transform.Rotate(0f,Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0f);
                    transform.position += transform.right * horizontal * playerSideways * Time.deltaTime;
                    SetState(State.Walk);
                }
            }


        }
        if (input == Vector3.zero && !jump && !roc)
        {
            SetState(State.Idle);
        }

       
        Debug.DrawRay(transform.position + new Vector3(0f, 0.3f, 0f), transform.forward * 5f, Color.red);
        if (Input.GetKeyDown(KeyCode.E) && !billZone)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0f, 0.3f, 0f), transform.TransformDirection(Vector3.forward), out hit, 5f, mask))
            {


                if (hit.collider.gameObject.tag == "impactObject" || hit.collider.gameObject.tag == "people" || hit.collider.gameObject.tag == "vendiChar")
                {
                    rTongue.SetActive(false);
                    rp.material = tMat;
                    rp.simulation.lengthMultiplier = 1.5f;
                    rp.simulation.massPerMeter = 1.7f;
                    rope.rigidbodySettings.body = hit.rigidbody;
                    currentGrab = hit.rigidbody.gameObject;
                    if (currentGrab.gameObject.tag == "impactObjects")
                    {
                        currentGrab.tag = "grabbed";
                    }
                    else if (currentGrab.gameObject.tag == "people")
                    {
                        hit.transform.gameObject.GetComponentInParent<Ragdoll_controller>().ragDollOn();
                    }
                }

            }
            else if (currentGrab != null && !billZone)
            {
                rp.material = null;
                rTongue.SetActive(true);
                LeanTween.moveLocal(rTongue, Vector3.one / 10f, 0.5f).setEasePunch();
                rope.rigidbodySettings.body = null;
                if (currentGrab.gameObject.tag == "grabbed")
                {
                    currentGrab.tag = "impactObject";
                }
                currentGrab = null;

            };
        }

        if (Input.GetKeyDown(KeyCode.E) && billZone)
        {
            if (bills.Count > 0)
            {
                rTongue.SetActive(false);
                rp.material = tMat;

                currentGrab = bills[UnityEngine.Random.Range(0, bills.Count)];
                currentGrab.transform.parent = null;

                rope.rigidbodySettings.body = currentGrab.GetComponent<Rigidbody>();


            }

        }

    }

    void JumActi()
    {
        jump = false;
    }

    void JumpNow(float vertical,bool run)
    {


        if (run)
        {
            rb.velocity = transform.forward * vertical * 8f + new Vector3(0f, 5f, 0f);

        }
        else
        {
            rb.velocity = transform.forward * vertical * 6f + new Vector3(0f, 3.5f, 0f);

        }
    }

    void SetState(State newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case State.Walk:
                Decre(true);
                anim.SetTrigger("walk");
                mg = 0f;
                break;
            case State.Idle:
                Decre(false);
                anim.SetTrigger("idle");
                mg = 0f;
                break;
            case State.Run:
                Decre(true);
                anim.SetTrigger("run");
                mg = 400f;
                break;
            case State.Jump:
                Decre(true);
                anim.SetTrigger("jump");
                mg = 400f;
                break;
            

        }
        currentState = newState;
    }

    void Decre(bool on)
    {
        if (on)
        {
            rp.simulation.stiffness = 0.04f;
        }
        else
        {
            rp.simulation.stiffness = 0.01f;
        }


    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.CompareTag("impactObject"))
        {
            Vector3 dir = other.transform.position -
            transform.position;

            
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, dir.y + 0.8f, dir.z) * mg);
            other.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(UnityEngine.Random.Range(20f, 30f),
            UnityEngine.Random.Range(20f, 30f), UnityEngine.Random.Range(20f, 30f)));

        };

       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin_AD"))
        {
            Destroy(other.gameObject);
            coinValue++;
            Instantiate(coinParticle, other.transform.position, Quaternion.identity);
            coinCount.text = coinValue.ToString();
        }
        if (other.CompareTag("bills"))
        {
            billZone = true;
        }
        if (other.CompareTag("vendi"))
        {
            if( currentGrab != null && currentGrab.gameObject.layer == LayerMask.NameToLayer("CashPick"))
            {
                vStartPos = currentGrab.transform;
                float currentDistance = Vector3.Distance(vStartPos.position, vStartPos.position);
                height = Remap(currentDistance, minDistance, maxDistance, minHeight, maxHeight);

                points = GetProjectilePoints(vStartPos.position, vEndpos.position, height , numberOfPoints);

                currentGrab.transform.rotation = Quaternion.identity;
                currentGrab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                LeanTween.moveSpline(currentGrab, points, 1.5f);
                LeanTween.delayedCall(1.5f, RemoveBill);
            }
            
        }

    }

    public void RemoveBill()
    {
        rp.material = null;
        rTongue.SetActive(true);
        LeanTween.moveLocal(rTongue, Vector3.one / 10f, 0.5f).setEasePunch();
        rope.rigidbodySettings.body = null;
        Destroy(currentGrab, 10f);
        currentGrab = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bills"))
        {
            billZone = false;
            if (currentGrab != null && currentGrab.gameObject.layer == LayerMask.NameToLayer("CashPick"))
            {
                bills.Remove(currentGrab);
                LeanTween.delayedCall(2f, CashRefill);
            }
        }
    }

    void CashRefill()
    {
        GameObject newBill = Instantiate(currentGrab, billSpawnPoint.position, billSpawnPoint.rotation);
        bills.Add(newBill);
    }

    Vector3[] GetProjectilePoints(Vector3 startPos, Vector3 endPos, float height, int numberOfDivisions)
    {
        Vector3[] points1 = new Vector3[numberOfDivisions];

        float increment = 1 / (float)numberOfDivisions;

        float t = 0;
        for (int i = 0; i < numberOfDivisions; i++)
        {

            points1[i] = SmoothCurve.Parabola(startPos, endPos, height, t);
            t += increment;
        }

        return points1;
    }

    public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        float fromAbs = from - fromMin;
        float fromMaxAbs = fromMax - fromMin;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = toMax - toMin;
        float toAbs = toMaxAbs * normal;

        float to = toAbs + toMin;

        return to;
    }

}