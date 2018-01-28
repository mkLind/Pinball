using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour {
  
    public Rect windowR = new Rect(100,50,2000,1000);
    public bool triggered;

	// Use this for initialization
	void Start () {
        // Position the window and set the story element to un triggered state
        windowR = new Rect((Screen.width/2)-200, 50, 400, 100);
        triggered = false;
	}
	
// Event handling. Add buttons, their shapes and what they do
    void DoMyWindow(int windowID) {
        if (GUI.Button(new Rect(150, 30, 100, 20), "RUN")) {
            Time.timeScale = 1; // Enable rendering
            triggered = false; // Set to untriggered statew

        } else if (GUI.Button(new Rect(150, 60, 100, 20), "Fight")) {
            Time.timeScale = 1;
            triggered = false;
        }

    }



    // When the Story element gets triggered, Show the UI Window
   void OnGUI()
    {
        if (triggered) {
            windowR = GUI.Window(0, windowR, DoMyWindow, "You encountered a troll. What do you do?");
        }

    }
    // Set the element to triggered state and freeze the scene
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {

            triggered = true;
            Time.timeScale = 0; // scene freezes 
            

        }


        
    }
    void OnTriggerExit(Collider other)
    {
       
    }

}
