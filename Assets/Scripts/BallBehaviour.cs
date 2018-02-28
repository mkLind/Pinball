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

    private AudioSource moveAudioSource;
    private AudioSource collisionAudioSource;

    private float speed;

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

        var source = GetComponents<AudioSource>();
        moveAudioSource = source[0];
        collisionAudioSource = source[1];
	}
		
	void Update () {
		//Score is updated every frame in case collisions with other objects change it
		scoreText.text = "Score: " + score.ToString ();

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Table")
        {
            collisionAudioSource.volume = speed / 10f;
            collisionAudioSource.Play();
        }
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
        Rigidbody ballBody = GetComponent<Rigidbody>();
        // update the speed of the ball
        speed = ballBody.velocity.magnitude;
        if (!GetComponent<Rigidbody>().IsSleeping())
        {
            // update pitch to match speed
            moveAudioSource.pitch = speed / 100f + 0.95f;
            moveAudioSource.volume = speed / 10f;

            // start the sound if not yet playing
            if (!moveAudioSource.isPlaying)
                moveAudioSource.Play();

        }

        //Checks if the ball falls out of the board (goes below the treshold)
        if (transform.position.y < threshold) 
		{
			//Updating the amount of balls
			ballAmount--;
			if (ballAmount >= 0) {	
				ballAmountText.text = "Balls: " + ballAmount.ToString ();

				//Zeroing out the velocity and repositioning the ball to the starting point
				ballBody.velocity = Vector3.zero;
				ballBody.angularVelocity = Vector3.zero;
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
