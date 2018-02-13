using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour {
    public int timesPassed;
	// Use this for initialization
	void Start () {
        timesPassed = 0;
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            timesPassed++;

        }
    }

    public int getTimesPassed() {
        return timesPassed;
    }
}
