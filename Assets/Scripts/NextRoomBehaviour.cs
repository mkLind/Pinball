using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomBehaviour : MonoBehaviour {

    private bool taskActive;
    private bool taskCompleted;

	// Use this for initialization
	void Start () {
        taskActive = false;
	}

    public void setTaskActive()
    {
        taskActive = true;
        taskCompleted = false;
    }

    public bool isTaskCompleted()
    {
        return taskCompleted;
    }

    //When the ball enters the trigger this method is called
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Rigidbody>() != null)
        {
            taskCompleted = true;
            taskActive = false;
        }
    }
}
