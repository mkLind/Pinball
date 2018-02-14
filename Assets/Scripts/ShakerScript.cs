using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : MonoBehaviour {
	public BallBehaviour bll;
	public Rigidbody ballBody;
	public TrapBehaviour trap;
	public Rigidbody plungBody;
	public PlungerBehavior plung;
	public bool shakeActive;

	float speed = 30.0f; //how fast it shakes
	float amount = 0.2f; //how much it shakes
	float timer = 0f;	//how long it shakes
	float force = 3.0f; //how much ball is moved
	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		bll = GameObject.Find("Ball").GetComponent<BallBehaviour> ();
		trap = GameObject.Find("TrapWall").GetComponent<TrapBehaviour> ();
		plungBody = GameObject.Find ("plunger").GetComponent<Rigidbody> ();
		plung = GameObject.Find ("plunger").GetComponent<PlungerBehavior> ();
		ballBody = GameObject.Find("Ball").GetComponent<Rigidbody> ();

		Input.gyro.enabled = true; 
		shakeActive = false;
		originalPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (/*bll.taskStatus () == false &&*/ trap.trapStatus () == false && Input.acceleration.magnitude > 1.3f) {
			//if (timer <= 1) {
				shakeActive = true;
				plungBody.constraints = RigidbodyConstraints.None;		//muista poistaa nä kun shake 
				plungBody.isKinematic = true;							//loppuu
				transform.position = new Vector3 (Mathf.Sin (Time.time * speed) * amount - 0.82f, transform.position.y,	transform.position.z);

				//add force to ball
				float directionZ = Random.Range(-1f, 1f);
				ballBody.AddForce (new Vector3 (directionZ, 0, 0) * force);

				timer += Time.deltaTime;
			//}
		} else {
			//plung.resetPlungerFreeze ();
			plungBody.isKinematic = false;
			shakeActive = false;
			//timer = 0f;
			transform.position = originalPos;
		}
	}

	public bool shakeStatus(){
		return shakeActive;
	}
}
