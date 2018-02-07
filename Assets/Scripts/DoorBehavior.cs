using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {
    public BallBehaviour bll;
    public int openLimit;
    public Rigidbody door;
    public float change;
    public bool scaling;
    public bool closed;
    public bool open;
    public float scaleFactor;

    // Use this for initialization
    void Start() {
        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();
        door = GetComponent<Rigidbody>();

        scaling = false;
        closed = true;
        open = false;

    }

    // Update is called once per frame
    void Update() {
        if (bll.getScore() >= openLimit && closed) {
            scaling = true;





        }


        openDoor();

    }

    void openDoor() {
        if (scaling) {

            door.transform.position += new Vector3(0, -0.1f, 0);


        }
        if (door.position.y <= -0.5f) {
            scaling = false;
            open = true;
            closed = false;
        }

    }

    void closeDoor() {
        if (scaling)
        {

            door.transform.position += new Vector3(0, 0.1f, 0);


        }
        if (door.position.y >= 0.7f)
        {
            scaling = false;
            open = false;
            closed = true;
        }

    }
}
