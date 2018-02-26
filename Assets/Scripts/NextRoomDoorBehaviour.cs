using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomDoorBehaviour : MonoBehaviour {
    public BallBehaviour bll;
    public int openLimit;
    public Rigidbody door;
    public float change;
    public bool changing;
    public bool closed;
    public bool open;


    // Use this for initialization
    void Start()
    {
        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();
        door = GetComponent<Rigidbody>();

        changing = false;
        closed = true;
        open = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            if (closed)
            {
                door.transform.position += new Vector3(0, -0.01f, 0);
                if (door.position.y <= -0.5f)
                {
                    changing = false;
                    open = true;
                    closed = false;
                }
            }
            if (open)
            {
                door.transform.position += new Vector3(0, 0.01f, 0);
                if (door.position.y >= 0.7f)
                {
                    changing = false;
                    open = false;
                    closed = true;
                }
            }
        }
    }




    public void SwitchDoor()
    {
        changing = true;
    }
}
