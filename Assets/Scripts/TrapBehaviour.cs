using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour {
	public bool trapActive = new bool();
	public float force = 60.0f;

	private Collision coll;
	private bool callOnce = new bool ();

	//Freezes the ball, waits for 3 seconds and then unfreezes it. Then catapults the ball away from the trap
	IEnumerator Wait(){
		coll.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		yield return new WaitForSeconds (3.0f);
		coll.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		//Adding force to Z-axis only (transform.forward)
		coll.gameObject.GetComponent<Rigidbody>().AddForce (transform.forward * force);
		//enable the trap again, small wait so the ball wont get stuck to the trap before leaving
		yield return new WaitForSeconds (0.1f);
		trapActive = false;
		callOnce = false;
	}

	//When the ball collides with the trap this method is called
	void OnCollisionEnter (Collision col)
	{	
		/*Cheking if the colliding object is the ball and if the collision has already been called once
		in order to not start another coroutine while in the trap*/
		if(col.gameObject.name == "Ball" && !callOnce)
		{
			coll = col;
			//Starting the coroutine
			StartCoroutine(Wait());
			trapActive = true;
			callOnce = true;
		}
	}

	public bool trapStatus(){
		return trapActive;
	}
}
