using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBehavior : MonoBehaviour {
    // Publics visible in unity
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 1000000f;
    public float flipDamper = 150f;
    // Name for the pressed key
    public string inputName;
    // Joint of the flipper
    HingeJoint hinge;
	// Use this for initialization
	void Start () {
        // Retreive the hinge joint attached to flipper paddle

        hinge = GetComponent<HingeJoint>();
        // Set it using a spring
        hinge.useSpring = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        // Create a spring and set strength and damper to it
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipDamper;
        // If a key is pressed, move the paddle to pressed position, otherwise move the paddle to rest position
        if (Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPosition;

        }
        else {
            spring.targetPosition = restPosition;
        }
        // Set the newly defined spring for the hinge and set it using limits.

        hinge.spring = spring;
        hinge.useLimits = true;

        
		
	}
}
