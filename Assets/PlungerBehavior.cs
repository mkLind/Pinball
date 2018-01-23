using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerBehavior : MonoBehaviour {
    public float minPos = 0f;
    public float currentPos = 0f;
    public float maxPos = -200f;
   
    public float plungerEnergy = 0f;
    public string inputName;
    Vector3 restPos;
    Vector3 curPos;
    Rigidbody plunger;
    Rigidbody plungerBase;
    SpringJoint joint;
    bool collidesWithPlunger = false;
    RigidbodyConstraints origConstr;

	// Use this for initialization
	void Start () {
        joint = new SpringJoint();
        plunger = GetComponent<Rigidbody>();
        plunger.freezeRotation = true;
        origConstr = plunger.constraints; // Y frozen from unity, others free
        // Freeze y and z of the plunger
        plunger.constraints = RigidbodyConstraints.FreezePositionZ;
        plunger.constraints = RigidbodyConstraints.FreezePositionY;
        // PLungers rest position
        restPos = plunger.position;



     
    }
	
	// Update is called once per frame
	void Update () {
        // if s is pressed
        if (Input.GetAxis(inputName)==1)
        {
            // If current position of the plunger is greater than the max position
            if (currentPos > maxPos)
            {
                // free Z and move the plunger while reducing position and adding energy to plunger
                plunger.constraints = origConstr;
                plunger.MovePosition(transform.position - transform.forward * Time.deltaTime);
                currentPos--;
                plungerEnergy = plungerEnergy + 25f;
                curPos = plunger.position; // Store current position

            }
            else {
                // Freeze y and z and set position to current position
                plunger.constraints = RigidbodyConstraints.FreezePositionZ;
                plunger.constraints = RigidbodyConstraints.FreezePositionY;
                plunger.position = curPos;

            }
        }

        else 
        {
            // if plunger going up
            if (currentPos < minPos)
            {
                // release Z, move and add to position while reducing energy
                plunger.constraints = origConstr;
                plunger.MovePosition(transform.position + transform.forward * Time.deltaTime * (plungerEnergy / 100));
                currentPos++;
                plungerEnergy -= 25f;
            }
            else
            {
                // freeze y and z set position to rest and energy to 0.
                plunger.constraints = RigidbodyConstraints.FreezePositionZ;
                plunger.constraints = RigidbodyConstraints.FreezePositionY;
                plungerEnergy = 0f;
                plunger.position = restPos;

            }
        }
      
        
		
	}

    private void addImpulse(Rigidbody other, float force) {
        if (other.CompareTag("Ball")) {
            other.AddForce(transform.forward * force);


        }


    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            collidesWithPlunger = true;

        }



    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Ball"))
        {
            collidesWithPlunger = false;

        }
    }

}
