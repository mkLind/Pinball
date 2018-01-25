using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehaviour : MonoBehaviour {

	public float force = 50.0f;

	//When the ball enters the trigger this method is called
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Rigidbody>() != null) 
		{
			//Force to a forward direction (Z-axis of the BumperTrigger) is added 
			//to the collision component that has a rigidbody (the ball)
			col.GetComponent<Rigidbody>().AddForce (transform.forward * force);
		}
	}
}