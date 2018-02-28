using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PlungerBehavior : MonoBehaviour {
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








    PlungerBase plungB;
	RigidbodyConstraints origConstr;
	// Rigidbody of plunger: is kinematic and use gravity true

	// Use this for initialization
	void Start () {

        pressed = false;
        anim = GetComponent<Animator>();
        maxReached = false;

		plungB = GameObject.Find ("plungerBase").GetComponent<PlungerBase> ();
     

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
    void FixedUpdate () {
        //Keyboard input. Checked key explicitly so touch events do not register.
        AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0); 
        
		if (Input.GetKey ("s")) {
			// if s is pressed
			if (Input.GetAxis (inputName) == 1) {
				pressed = true;
				// If current position of the plunger is greater than the max position
				if (current > max) {
                   


                    // free Z and move the plunger while reducing position and adding energy to plunger
                    //=> plunger moves downwards because of this

                    plunger.constraints = origConstr;
					plunger.MovePosition (transform.position - transform.forward * Time.deltaTime * plungerLoadSpeed);
					currentPos = plunger.position.z;
					plungerEnergy = plungerEnergy + plungerEnergyAddition;
					curPos = plunger.position; // Store current position
					current -= 1; // Integer value marking the position of the plunger


				} else {

					// Freeze y and z and set position to current position
					plunger.constraints = RigidbodyConstraints.FreezePositionZ;
					plunger.constraints = RigidbodyConstraints.FreezePositionY;
					plunger.position = curPos;
                    maxReached = true;

                   
                }
			} else {
				if (pressed) {
					// Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
					if (!forceApplied) {

						plunger.AddForce (transform.forward * plungerEnergy * damper);

                  
						forceApplied = true;
                        maxReached = false;
                    }
					// if plunger going up
					if (current < min && forceApplied) {
                       


                        // release Z, move and add to position while reducing energy
                        plunger.constraints = origConstr;

						plunger.MovePosition (transform.position + new Vector3 (0, 0, plungerEnergy * damper) * Time.deltaTime);

						currentPos = plunger.position.z;
						// Change current indicator and plunger energy only if plunger energy is higher than zero.
						if (plungerEnergy > 0) {
							plungerEnergy -= plungerEnergyAddition;
							current += 1 * (int)plungerEnergy;
						}

					} else {
                       
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

					}
				}
			}
		} 
		//Touch input
		else if (plungB.getPressedState () == true) {	//Attach PlungerBase script to plungerBase gameobject!
			//When plunger base is pressed
			if (Input.touchCount > 0) {
				pressed = true;
				// If current position of the plunger is greater than the max position
				if (current > max) {
                    

                    // free Z and move the plunger while reducing position and adding energy to plunger
                    //=> plunger moves downwards because of this

                    plunger.constraints = origConstr;
					plunger.MovePosition (transform.position - transform.forward * Time.deltaTime);
					currentPos = plunger.position.z;
					plungerEnergy = plungerEnergy + plungerEnergyAddition;
					curPos = plunger.position; // Store current position
					current -= 1; // Integer value marking the position of the plunger


				} else {
                    
                    // Freeze y and z and set position to current position
                    plunger.constraints = RigidbodyConstraints.FreezePositionZ;
					plunger.constraints = RigidbodyConstraints.FreezePositionY;
					plunger.position = curPos;


				}
			} else {
				if (pressed) {
					// Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
					if (!forceApplied) {

						plunger.AddForce (transform.forward * plungerEnergy * damper);

						forceApplied = true;
					}
					// if plunger going up
					if (current < min && forceApplied) {
                      


                        // release Z, move and add to position while reducing energy
                        plunger.constraints = origConstr;

						plunger.MovePosition (transform.position + new Vector3 (0, 0, plungerEnergy * damper) * Time.deltaTime);

						currentPos = plunger.position.z;
						// Change current indicator and plunger energy only if plunger energy is higher than zero.
						if (plungerEnergy > 0) {
							plungerEnergy -= plungerEnergyAddition;
							current += 1 * (int)plungerEnergy;
						}

					} else {

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
                      

                    }
				}
			}
		}else {
			if (pressed) {
				// Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
				if (!forceApplied) {

					plunger.AddForce (transform.forward * plungerEnergy * damper);

					forceApplied = true;
				}
				// if plunger going up
				if (current < min && forceApplied) {



					// release Z, move and add to position while reducing energy
					plunger.constraints = origConstr;

					plunger.MovePosition (transform.position + new Vector3 (0, 0, plungerEnergy * damper) * Time.deltaTime);

					currentPos = plunger.position.z;
					// Change current indicator and plunger energy only if plunger energy is higher than zero.
					if (plungerEnergy > 0) {
						plungerEnergy -= plungerEnergyAddition;
						current += 1 * (int)plungerEnergy;
					}

				} else {

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

				}
			}
		}
	}



   
}
