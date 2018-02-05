using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	//Function referenced in menu's play-button
	public void PlayGame ()
	{	
		//Change scene name for testing different ones
		SceneManager.LoadScene ("Tommi");
	}

	//Function referenced in menus's quit-button
	public void QuitGame ()
	{
		Debug.Log ("Quit!");
		Application.Quit ();	//Wont quit inside editor
	}
}
