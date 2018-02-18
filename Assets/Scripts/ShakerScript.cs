using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : MonoBehaviour {
	public BallBehaviour bll;
	public Rigidbody ballBody;
	public TrapBehaviour trap;
	public PlungerBase plung;
	public bool shakeActive;

	float speed = 30.0f; //how fast it shakes
	float amount = 0.1f; //how much it shakes
	//float timer = 0f;	//how long it shakes
	float force = 3.0f; //how much ball is moved
	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		plung = GameObject.Find ("plungerBase").GetComponent<PlungerBase> ();
		trap = GameObject.Find("TrapWall").GetComponent<TrapBehaviour> ();
		ballBody = GameObject.Find("Ball").GetComponent<Rigidbody> ();

		Input.gyro.enabled = true; 
		shakeActive = false;
		originalPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (trap.trapStatus () == false && plung.getPressedState() == false && Input.acceleration.magnitude > 1.3f) {
				shakeActive = true;
				//0.82f and 4f are offsets to center the shake
				transform.position = new Vector3 (Mathf.Sin (Time.time * speed) * amount - 0.82f, transform.position.y,	Mathf.Sin (Time.time * speed) * 0.01f + 4f);

				//add force to ball
				float directionZ = Random.Range(-1f, 1f);
				ballBody.AddForce (new Vector3 (directionZ, 0, 0) * force);

		} else {
			shakeActive = false;
			transform.position = originalPos;
		}
	}

	public bool shakeStatus(){
		return shakeActive;
	}
}
