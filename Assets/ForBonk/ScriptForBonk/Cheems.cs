using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheems : MonoBehaviour
{

    public Transform cam;
    public float playerSpeed;
    public float playerSideways;
    public float rotationSpeed;
    float mg;


    public int coinValue = 0;
    private int baseCoinValue = 100;
    private int multiplier = 1;
    public TMP_Text coinCount;
    public TMP_Text multiplierText;

    public ParticleSystem coinParticle;

    //public GameObject Bat;
    public Transform trans;
    public Transform bat;

    Rigidbody rb;

    [Header("SFX")]
    [SerializeField] private AudioClip[] bonkSounds;

    [Header("FX")]
    public ParticleSystem parti;


    [SerializeField]
    Animator anim;

    private int xVelHash;
    private int yVelHash;

    bool jump;
    bool bonk;
    public bool IsBonking { get { return bonk; } }

    State currentState = State.Idle;

    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        bonk


    }

    private void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();


        xVelHash = Animator.StringToHash("xVelocity");
        yVelHash = Animator.StringToHash("yVelocity");
    }

   
    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 input = new Vector3(0f, horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space) && !jump && horizontal == 0f && !bonk)
        {
            jump = true;

            SetState(State.Jump);

            LeanTween.delayedCall(1f, JumActi);
            LeanTween.delayedCall(0.3f, () =>
            {
                JumpNow(vertical, true);
            });

        }

        if(Input.GetMouseButtonDown(0) && !Hit() /*&& horizontal == 0f*/)
        {
            bonk = true;
            //Bat.tag = "Player";
            SetState(State.bonk);
            LeanTween.delayedCall(1f, Idle);


            // Temp code need to delete later
            anim.SetFloat(xVelHash, 0);
            anim.SetFloat(yVelHash, 0);
        }



        if (input != Vector3.zero && !jump && !bonk )
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

            // Updating Animations
            if (!jump && !bonk)
            {
                float currentVelocityY = vertical * playerSpeed;
                float currentVelocityX = horizontal * playerSideways;

                anim.SetFloat(xVelHash, currentVelocityX);
                anim.SetFloat(yVelHash, currentVelocityY);
            }
            else
            {
                anim.SetFloat(xVelHash, 0);
                anim.SetFloat(yVelHash, 0);
            }
        }
        if (input == Vector3.zero && !jump && !bonk)
        {
            SetState(State.Idle);
        }
    }


    void Idle()
    {
        //Bat.tag = "Untagged";

        bonk = false;
    }

    bool Hit()
    {
        if(currentState != State.Jump )
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }


    void JumActi()
    {
        jump = false;
    }

    void JumpNow(float vertical, bool run)
    {


        if (run)
        {
            rb.velocity = transform.forward * vertical * 8f + new Vector3(0f, 3.5f, 0f);

        }
        else
        {
            rb.velocity = transform.forward * vertical * 6f + new Vector3(0f, 2f, 0f);

        }
    }

    private void OnCollisionEnter(Collision other)
    {

        //if (other.collider.CompareTag("impactObject"))
        //{
        //    Vector3 dir = other.transform.position -
        //    transform.position;


        //    other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, dir.y + 0.8f, dir.z) * mg);
        //    other.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(UnityEngine.Random.Range(20f, 30f),
        //    UnityEngine.Random.Range(20f, 30f), UnityEngine.Random.Range(20f, 30f)));

        //};


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin_AD"))
        {
            Destroy(other.gameObject, 0.5f);
            //coinValue++;
            UpdateCoins(baseCoinValue);
            Instantiate(coinParticle, other.transform.position, Quaternion.identity);
            //coinCount.text = coinValue.ToString();
        }


    }

    public void RewardPlayer()
    {
        int chance = Random.Range(0, 100);

        if (chance <= 2)
        {
            UpdateMultiplier();
        }

       
       

        UpdateCoins(baseCoinValue);
    }

    IEnumerator UpdateMultiplier()
    {
        multiplier = 2;

        multiplierText.text = "x2";

        yield return new WaitForSeconds(15f);

        multiplier = 1;

        multiplierText.text = "";
    }

    public void UpdateCoins(int _value)
    {
        coinValue += _value * multiplier;
        coinCount.text = coinValue.ToString();

        if (coinValue >= 25000)
        {
            GameOverCondition.Instance.GameOver();
        }
    }


    void SetState(State newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case State.Walk:
                anim.SetTrigger("walk");
                mg = 400f;
                break;
            case State.Idle:
                anim.SetTrigger("idle");
                mg = 0f;
                break;
            case State.Run:
                anim.SetTrigger("run");
                mg = 400f;
                break;
            case State.Jump:
                anim.SetTrigger("jump");
                mg = 400f;
                break;
            case State.bonk:
                anim.SetTrigger("bonk");
                mg = 400f;
                break;


        }
        currentState = newState;
    }

}
