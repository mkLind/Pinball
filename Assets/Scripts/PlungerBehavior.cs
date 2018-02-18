﻿using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PlungerBehavior : MonoBehaviour {
    public float minPos = 0f;
    public float currentPos = 0f;
    public float maxPos = -0f;
   
    public float plungerEnergy = 0f;
    public float plungerEnergyAddition = 0f;
    public bool forceApplied = false;
    public string inputName;
    //public Vector3 curPos;
    public Vector3 restPos;
    public Vector3 curPos;
    Rigidbody plunger;
    Rigidbody plungerBase;
   	
	PlungerBase plung;
	ShakerScript shake;
	   
    RigidbodyConstraints origConstr;
  // Rigidbody of plunger: is kinematic and use gravity true
	  
	// Use this for initialization
	void Start () {
        
		plung = GameObject.Find ("plungerBase").GetComponent<PlungerBase> ();
		//shake = GameObject.Find ("WholeTable").GetComponent<ShakerScript> ();

       
		plunger = GetComponent<Rigidbody> ();
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
	void Update () {
		//shakerscript in development
		//if (shake.shakeStatus () == false) {

			//Keyboard input. Checked key explicitly so touch events do not register.
			if (Input.GetKey ("s")) {
				// if s is pressed
				if (Input.GetAxis (inputName) == 1) {
					// If current position of the plunger is greater than the max position
					if (curPos.z >= maxPos) {
						// free Z and move the plunger while reducing position and adding energy to plunger
						//=> plunger moves downwards because of this
						plunger.constraints = origConstr;
						plunger.MovePosition (transform.position - transform.forward * Time.deltaTime);
						currentPos = plunger.position.z;
						plungerEnergy = plungerEnergy + plungerEnergyAddition;
						curPos = plunger.position; // Store current position
              
                

					} else {
						// Freeze y and z and set position to current position
						plunger.constraints = RigidbodyConstraints.FreezePositionZ;
						plunger.constraints = RigidbodyConstraints.FreezePositionY;
						plunger.position = curPos;
            		}
        		}
		        else 
		        {
		            // Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
		            if (!forceApplied) {
		                plunger.AddForce(transform.forward*plungerEnergy);
		             
		                forceApplied = true;
		            }
		            // if plunger going up
		            if (plunger.position.z < minPos)
		            {
		                // release Z, move and add to position while reducing energy
		                plunger.constraints = origConstr;
		                plunger.MovePosition(transform.position + new Vector3(0,0,plungerEnergy)* Time.deltaTime);
		                currentPos = plunger.position.z;
		              
		                plungerEnergy -= plungerEnergyAddition;
		            }
		            else
		            {
		                // freeze y and z set position to rest and energy to 0.
		                plunger.constraints = RigidbodyConstraints.FreezePositionZ;
		                plunger.constraints = RigidbodyConstraints.FreezePositionY;
		                plungerEnergy = 0f;
		                plunger.position = restPos;
		                curPos = restPos;
		                currentPos = plunger.position.z;
		                forceApplied = false;
 
					}
				}
			}

		//Touch input
		else if (plung.getPressedState () == true) {
				//When plunger base is pressed
				if (Input.touchCount > 0) {
					// If current position of the plunger is greater than the max position
					if (curPos.z >= maxPos) {
						// free Z and move the plunger while reducing position and adding energy to plunger
						//=> plunger moves downwards because of this
						plunger.constraints = origConstr;
						plunger.MovePosition (transform.position - transform.forward * Time.deltaTime);
						currentPos = plunger.position.z;
						plungerEnergy = plungerEnergy + plungerEnergyAddition;
						curPos = plunger.position; // Store current position



					} else {
						// Freeze y and z and set position to current position
						plunger.constraints = RigidbodyConstraints.FreezePositionZ;
						plunger.constraints = RigidbodyConstraints.FreezePositionY;
						plunger.position = curPos;
					}
				} else {
					// Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
					if (!forceApplied) {
						plunger.AddForce(transform.forward*plungerEnergy);

						forceApplied = true;
					}
					// if plunger going up
					if (plunger.position.z < minPos)
					{
						// release Z, move and add to position while reducing energy
						plunger.constraints = origConstr;
						plunger.MovePosition(transform.position + new Vector3(0,0,plungerEnergy)* Time.deltaTime);
						currentPos = plunger.position.z;

						plungerEnergy -= plungerEnergyAddition;
					}
					else
					{
						// freeze y and z set position to rest and energy to 0.
						plunger.constraints = RigidbodyConstraints.FreezePositionZ;
						plunger.constraints = RigidbodyConstraints.FreezePositionY;
						plungerEnergy = 0f;
						plunger.position = restPos;
						curPos = restPos;
						currentPos = plunger.position.z;
						forceApplied = false;

					}
				}

			} else {
				// Applies once an impulse to the plunger that in turn applies the force to the ball if the plunger and the ball collide.
				if (!forceApplied) {
					plunger.AddForce(transform.forward*plungerEnergy);

					forceApplied = true;
				}
				// if plunger going up
				if (plunger.position.z < minPos)
				{
					// release Z, move and add to position while reducing energy
					plunger.constraints = origConstr;
					plunger.MovePosition(transform.position + new Vector3(0,0,plungerEnergy)* Time.deltaTime);
					currentPos = plunger.position.z;

					plungerEnergy -= plungerEnergyAddition;
				}
				else
				{
					// freeze y and z set position to rest and energy to 0.
					plunger.constraints = RigidbodyConstraints.FreezePositionZ;
					plunger.constraints = RigidbodyConstraints.FreezePositionY;
					plungerEnergy = 0f;
					plunger.position = restPos;
					curPos = restPos;
					currentPos = plunger.position.z;
					forceApplied = false;

				}
			}
		}
	//}
}