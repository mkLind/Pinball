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
			//Force to a forward direction (Z-axis of the trigger) is added 
			//to the collision component that has a rigidbody (the ball)
			col.GetComponent<Rigidbody>().AddForce (new Vector3(force,0,force));

			//Incrementing score that's in the ball script
			BallBehaviour bb = col.gameObject.GetComponent<BallBehaviour>();
			bb.score = bb.score + 50;
		}
	}
}