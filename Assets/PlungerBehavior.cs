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
    Rigidbody plunger;

	// Use this for initialization
	void Start () {
        plunger = GetComponent<Rigidbody>();
        plunger.freezeRotation = true;
        plunger.constraints = RigidbodyConstraints.FreezePositionY;
        restPos = plunger.position;
        plunger.velocity = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetAxis(inputName) == 1)
        {
            if (currentPos > -200)
            {
                plunger.MovePosition(transform.position - transform.forward * Time.deltaTime);
                currentPos--;
                plungerEnergy = plungerEnergy + 10f;

            }
        }
        else {

            if (currentPos < 0)
            {
                plunger.MovePosition(transform.position + transform.forward * Time.deltaTime*(plungerEnergy/100));
                currentPos++;
            }
            else {
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

}
