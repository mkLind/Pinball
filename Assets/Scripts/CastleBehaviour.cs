using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour {

    HingeJoint leftDoor;
    HingeJoint rightDoor;
    JointSpring leftSpring;
    JointSpring rightSpring;

    public float leftRestPosition;
    public float rightRestPosition;
    public float leftPressedPosition;
    public float rightPressedPosition;
    public float openStrength;
    public float flipDamper = 150f;

    private bool open = false;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        leftDoor = GameObject.Find("leftCastleDoor").GetComponent<HingeJoint>();
        rightDoor = GameObject.Find("rightCastleDoor").GetComponent<HingeJoint>();
        // Set it using a spring
        leftDoor.useSpring = true;
        leftDoor.useLimits = true;
        rightDoor.useSpring = true;
        rightDoor.useLimits = true;

        OpenDoors();
    }
	
	// Update is called once per frame
	void Update () {
        leftSpring = new JointSpring();
        rightSpring = new JointSpring();
        leftSpring.spring = openStrength;
        rightSpring.spring = openStrength;
        leftSpring.damper = flipDamper;
        rightSpring.damper = flipDamper;

        if (open)
        {
            leftSpring.targetPosition = leftPressedPosition;
            rightSpring.targetPosition = rightPressedPosition;

                
        } else
        {
            leftSpring.targetPosition = leftRestPosition;
            rightSpring.targetPosition = rightRestPosition;
        }
        leftDoor.spring = leftSpring;
        rightDoor.spring = rightSpring;
	}

    public void OpenDoors()
    {
        open = true;
        audioSource.Play();
    }

    public void CloseDoors()
    {
        open = false;
        audioSource.Play();
    }
}
