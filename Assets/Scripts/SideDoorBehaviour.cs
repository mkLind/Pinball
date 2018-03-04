using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoorBehaviour : MonoBehaviour {
	public BallBehaviour bll;
	public int openLimit;
	public Rigidbody door;
	public float change;
	public bool changing;
	public bool closed;
	public bool open;


	// Use this for initialization
	void Start() {
		door = GetComponent<Rigidbody>();

		changing = false;
		closed = true;
		open = false;

	}
	void Update(){
		if (changing) {
			// change position
			door.transform.position += new Vector3(0, -0.01f, 0);


		}
		// if long enough moved, then stop and set state to opened
		if (door.position.y <= -0.95f) {
			changing = false;
			open = true;
			closed = false;
		}
	}
	public void lowerDoor(){
		changing = true;
	}


}
