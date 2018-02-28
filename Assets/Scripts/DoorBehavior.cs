using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {
    public BallBehaviour bll;
    public int openLimit;
    public Rigidbody door;
    public float change;
    public bool changing;
    public bool closed;
    public bool open;


    // Use this for initialization
    void Start() {
        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();
        door = GetComponent<Rigidbody>();

        changing = false;
        closed = true;
        open = false;

    }

    // Update is called once per frame
    void Update() {
        // If score is above limit and door state is closed then  set status to changing to open the door.
        if (bll.getScore() >= openLimit && closed) {
            changing = true;





        }


        openDoor();

    }




    void openDoor() {
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

    void closeDoor() {
        if (changing)
        {

            door.transform.position += new Vector3(0, 0.01f, 0);


        }
        if (door.position.y >= 0.7f)
        {
            changing = false;
            open = false;
            closed = true;
        }

    }
}
