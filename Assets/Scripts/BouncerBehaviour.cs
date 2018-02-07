using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerBehaviour : MonoBehaviour {

	public float force = 50.0f;
	private float directionZ;

	//When the ball collides with the bouncer this method is called
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.GetComponent<Rigidbody> () != null) 
		{
			//Force to a random direction in the X-axis is added to the 
			//collision component that has a rigidbody (the ball)
			directionZ = Random.Range(-1f, 1f);
			col.gameObject.GetComponent<Rigidbody>().AddForce (new Vector3 (directionZ, 0, 0) * force);

			//Incrementing score that's in the ball script
			BallBehaviour bb = col.gameObject.GetComponent<BallBehaviour>();
			bb.score = bb.score + 100;
		}
	}
}
