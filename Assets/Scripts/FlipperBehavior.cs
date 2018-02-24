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
	JointSpring spring;

	PlungerBase plung;

	// Use this for initialization
	void Start () {
        // Retreive the hinge joint attached to flipper paddle
      

    


        hinge = GetComponent<HingeJoint>();
        // Set it using a spring
        hinge.useSpring = true;

		hinge.useLimits = true;
		plung = GameObject.Find ("plungerBase").GetComponent<PlungerBase> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Create a spring and set strength and damper to it
		spring = new JointSpring();
		spring.spring = hitStrength;
		spring.damper = flipDamper;

		//Keyboard input. Checked keys explicitly so touch events do not register.
		if (Input.GetKey ("a") || Input.GetKey("d")) {
			// If a key is pressed, move the paddle to pressed position, otherwise move the paddle to rest position
			if (Input.GetAxis (inputName) == 1) {
				spring.targetPosition = pressedPosition;
			} else {
				spring.targetPosition = restPosition;
			}
		}
		//Touch input
		else if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				//For the left half of the screen
				if (touch.position.x < Screen.width / 2) {
					if (gameObject.name == "Flipper l") {
						spring.targetPosition = pressedPosition;
					}
				} else if (touch.position.x > Screen.width / 2 && plung.getPressedState () == false) {
					if (gameObject.name == "Flipper R") {
						spring.targetPosition = pressedPosition;
					}
				}
			}
		} else {
			spring.targetPosition = restPosition;
		}
		// Set the newly defined spring for the hinge and set it using limits.
		hinge.spring = spring;
	}
}
