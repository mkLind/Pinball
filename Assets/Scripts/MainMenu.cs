using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	CanvasGroup canvasGroup;
    AudioSource audioSource;

    private bool fadingIn;

	void Start(){
		canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup> ();
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.volume = 0;
        fadingIn = true;

	}

	void Update(){
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);
		if (canvasGroup.alpha < 0.01f) {
			SceneManager.LoadScene ("Tommi");
		}
        if (fadingIn && audioSource.volume < 1)
        {
            audioSource.volume += Time.fixedDeltaTime;
        } else
        {
            fadingIn = false;
        }
	}
	//Function referenced in menu's play-button
	public void PlayGame ()
	{	
		//PlayerPrefs.SetInt ("highscore", 77);		//testing
		StartCoroutine (DoFade ());
	}

	//Fading out the menu
	IEnumerator DoFade(){
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime;
            audioSource.volume -= Time.deltaTime;
			yield return null;
		}
		canvasGroup.interactable = false;
		yield return null;
	}

	//Function referenced in menus's quit-button
	public void QuitGame ()
	{
		Debug.Log ("Quit!");
		Application.Quit ();	//Wont quit inside editor
	}
}
