using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

	public int score;
	public int ballAmount;		//change in inspector
	public Text scoreText;
	public Text ballAmountText;

	public Text highScoreText;
	public int oldHighscore;

	public float threshold;
	public Vector3 startPosition;
    public bool taskActive;

	void Start () {
		score = 0;
		scoreText.text = "Score: " + score.ToString ();
		ballAmountText.text = "Balls: " + ballAmount.ToString ();

		//Returns zero if highscore does not exist
		oldHighscore = PlayerPrefs.GetInt("highscore", 0);
		highScoreText.text = "Highscore: " + oldHighscore.ToString ();

		threshold = -5f;
        taskActive = false;
		//Initial starting position
	
	}
		
	void Update () {
		//Score is updated every frame in case collisions with other objects change it
		scoreText.text = "Score: " + score.ToString ();
	}

    public void setTaskActive() {
        taskActive = true;
    }

    public void disableTask()
    {
        taskActive = false;
    }

    public bool taskStatus() {

        return taskActive;
    }

    void FixedUpdate () {
		//Checks if the ball falls out of the board (goes below the treshold)
		if (transform.position.y < threshold) 
		{
			//Updating the amount of balls
			ballAmount--;
			if (ballAmount >= 0) {	
				ballAmountText.text = "Balls: " + ballAmount.ToString ();

				//Zeroing out the velocity and repositioning the ball to the starting point
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				transform.position = startPosition;
			} else {
				//Saving the possible highscore
				if (score > oldHighscore) {
					highScoreText.text = "Highscore: " + score.ToString ();
					PlayerPrefs.SetInt ("highscore", score);
					PlayerPrefs.Save();
				}
				//Freezing the game and displaying the end screen
				Time.timeScale = 0f;
				GameObject.Find ("Canvas").GetComponent<PauseMenu> ().endScreenUI.SetActive (true);
				Destroy (this);
			}	
		}
	}

    public int getScore() {
        return score;
    }

    
}
