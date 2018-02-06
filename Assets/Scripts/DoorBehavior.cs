using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {
    public BallBehaviour bll;
    public int openLimit;
    public Rigidbody door;
    public float change;
    public bool scaling;
    public float scaleFactor;

	// Use this for initialization
	void Start () {
		bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();
        door = GetComponent<Rigidbody>();
        openLimit = 100;
        scaling = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (bll.getScore() >= openLimit) {
            scaling = true;

            if (door.transform.localScale.y >= 0.5f) { 
            scaleFactor = -0.1f;
        } else if (door.transform.localScale.y == 0f) {
            scaleFactor = 0.1f;
        }
            



        }

        if (scaling) {
            door.transform.localScale -= new Vector3(0, scaleFactor, 0);

            if (door.transform.localScale.y >= 0.5f || door.transform.localScale.y == 0f) {
                scaling = false;
            }
        }

		
	}
}
