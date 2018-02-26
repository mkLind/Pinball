using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehaviour : MonoBehaviour {

	public float force = 300.0f;
    private BallBehaviour bb;

    private void Start()
    {
        bb = GameObject.Find("Ball").GetComponent<BallBehaviour>();
    }

    //When the ball enters the trigger this method is called
    void OnCollisionEnter(Collision col)
	{
		if (col.collider.GetComponent<Rigidbody>() != null) 
		{
			//Force to a forward direction (Z-axis of the trigger) is added 
			//to the collision component that has a rigidbody (the ball)
			col.gameObject.GetComponent<Rigidbody>().AddForce (new Vector3(force,0,0));

			//Incrementing score that's in the ball script
			bb.score = bb.score + 50;
		}
	}
}