using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerBase : MonoBehaviour {
	
	public bool plungerIsPressed;

	// Use this for initialization
	void Start () {
		plungerIsPressed = false;
	}

	//Used to determine the duration of when the plunger is pressed down
	void OnMouseDown(){
		plungerIsPressed = true;
	}
	void OnMouseExit(){
		plungerIsPressed = false;
	}

	public bool getPressedState(){
		return plungerIsPressed;
	}
}
