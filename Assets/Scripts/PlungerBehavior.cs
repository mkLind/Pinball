using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PlungerBehavior : MonoBehaviour
{
    public float minPos = 0f;
    public float currentPos = 0f;
    public float maxPos = -0f;
    public bool pressed;
    public int min;
    public int max;
    public int current;


    public float plungerEnergy = 0f;
    public float plungerEnergyAddition = 0f;
    public float plungerLoadSpeed = 0f;
    public float damper;
    public bool forceApplied = false;
    public bool maxReached;
    public string inputName;
    public Animator anim;
    //public Vector3 curPos;
    public Vector3 restPos;
    public Vector3 curPos;
    Rigidbody plunger;
    Rigidbody plungerBase;
    public int shoot;
    public int Max;
    public int load;
    public int idle;
    public int back;
    public bool ballInTouch;
    public bool isKeyEnabled;


    private AudioSource audioSource;



    PlungerBase plungB;
    RigidbodyConstraints origConstr;
    // Rigidbody of plunger: is kinematic and use gravity true

    // Use this for initialization
    void Start()
    {
        isKeyEnabled = true;
        audioSource = GetComponent<AudioSource>();
        shoot = Animator.StringToHash("Shoot");

        load = Animator.StringToHash("Load");
        idle = Animator.StringToHash("Idle");
        back = Animator.StringToHash("Back");
        ballInTouch = false;



        pressed = false;
        anim = GetComponent<Animator>();
        maxReached = false;

        plungB = GameObject.Find("plungerBase").GetComponent<PlungerBase>();


        plunger = GetComponent<Rigidbody>();
        plunger.freezeRotation = true;


        plunger.constraints = RigidbodyConstraints.FreezePositionZ;
        plunger.constraints = RigidbodyConstraints.FreezePositionY;

        plunger.constraints = RigidbodyConstraints.FreezeRotationX;
        plunger.constraints = RigidbodyConstraints.FreezeRotationY;
        plunger.constraints = RigidbodyConstraints.FreezeRotationZ;

        origConstr = plunger.constraints;
        // PLungers rest position
        restPos = plunger.position;
        currentPos = plunger.position.z;
        forceApplied = false;





    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("KEY S IS ENABLED: " + isKeyEnabled);
        //Keyboard input. Checked key explicitly so touch events do not register.
        //AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0); 

        // if s is pressed
        if (isKeyEnabled) {
            anim.SetTrigger(back);

        }
        
        
        if (Input.GetAxis(inputName) == 1 && isKeyEnabled)
        {
            pressed = true;

            if (!audioSource.isPlaying)
                audioSource.Play();

            if (pressed)
            {
                anim.SetTrigger(load);
                anim.ResetTrigger(idle);
                anim.ResetTrigger(back);
            }
            // If current position of the plunger is greater than the max position
            if (current > max)
            {



                // free Z and move the plunger while reducing position and adding energy to plunger
                //=> plunger moves downwards because of this

                plunger.constraints = origConstr;
                //plunger.MovePosition (transform.position - transform.forward * Time.deltaTime * plungerLoadSpeed);
                currentPos = plunger.position.z;
                plungerEnergy = plungerEnergy + plungerEnergyAddition;
                curPos = plunger.position; // Store current position
                current -= 1; // Integer value marking the position of the plunger


            }
            else
            {

                // Freeze y and z and set position to current position
                plunger.constraints = RigidbodyConstraints.FreezePositionZ;
                plunger.constraints = RigidbodyConstraints.FreezePositionY;
                plunger.position = curPos;
                maxReached = true;





            }
        }
        else
        {

            // Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
            if (pressed)
            {
                isKeyEnabled = false;


                if (!forceApplied)
                {
                    anim.ResetTrigger(load);
                    anim.SetTrigger(shoot);

                    //plunger.AddForce (transform.forward * plungerEnergy * damper);
                  
                 
                       
                    

                    forceApplied = true;
                    
                    maxReached = false;
                }
                if (anim.GetAnimatorTransitionInfo(0).IsName("Load -> Shoot"))
                {
                    anim.SetTrigger(back);
                    anim.ResetTrigger(shoot);

                    if (ballInTouch)
                    {
                        GameObject.Find("Ball").GetComponent<Rigidbody>().AddForce(transform.forward * plungerEnergy * damper);
                        ballInTouch = false;
                    }
                }

                    // if plunger going up
                    if (plungerEnergy > 0 && forceApplied)
                {



                    // release Z, move and add to position while reducing energy
                    plunger.constraints = origConstr;

                    //plunger.MovePosition (transform.position + new Vector3 (0, 0, plungerEnergy * damper) * Time.deltaTime);

                    currentPos = plunger.position.z;
                    // Change current indicator and plunger energy only if plunger energy is higher than zero.

                    plungerEnergy -= plungerEnergyAddition;
                    current += 1 * (int)plungerEnergy;
                   
                        
                    
                    
                }
                else
                {
                    if (audioSource.isPlaying)
                        audioSource.Stop();
                    // freeze y and z set position to rest and energy to 0.
                    plunger.constraints = RigidbodyConstraints.FreezePositionZ;
                    plunger.constraints = RigidbodyConstraints.FreezePositionY;
                    plungerEnergy = 0f;
                    plunger.position = restPos;
                    curPos = restPos;
                    current = min - 1;
                    currentPos = plunger.position.z;
                    forceApplied = false;
                    pressed = false;
                    anim.ResetTrigger(idle);
                    isKeyEnabled = true;
                    
                }

            }
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            anim.SetTrigger(idle);
            isKeyEnabled = true;
            ballInTouch = true;
        }
    }




}