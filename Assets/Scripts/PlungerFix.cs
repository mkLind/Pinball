using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	//If ball goes below the plunger put it back
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Ball"))
		{
			other.gameObject.transform.position = new Vector3(4.28f, 0.704f, 2.71f);
		}
	}
}