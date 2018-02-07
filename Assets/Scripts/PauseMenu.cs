using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is attached to Canvas, its public gameobjects reference the pause and end screens (set in inspector)
public class PauseMenu : MonoBehaviour {

	public static bool gamePaused = false;
	public GameObject pauseMenuUI;
	public GameObject endScreenUI;

	// Update is called once per frame
	void Update () {
		//Pause menu is displayed when pushing esc
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gamePaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}
		
	public void Resume()
	{
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gamePaused = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gamePaused = true;
	}

	//Retry used in endscreen
	public void Retry ()
	{	
		Resume ();
		SceneManager.LoadScene ("Tommi");	//change name accordingly
	}

	public void LoadMenu()
	{
		Resume ();
		SceneManager.LoadScene ("Menu");
	}

	public void QuitGame()
	{
		Debug.Log ("Quitting game");
		Application.Quit ();
	}
}
