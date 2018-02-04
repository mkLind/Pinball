using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

	public int score;
	public int ballAmount;
	public Text scoreText;
	public Text ballAmountText;

	public float threshold;
	public Vector3 startPosition;

	void Start () {
		score = 0;
		ballAmount = 2;
		scoreText.text = "Score: " + score.ToString ();
		ballAmountText.text = "Balls: " + ballAmount.ToString ();
		//Initial starting position
		threshold = -5f;
		startPosition = new Vector3 (4.2f, 0.25f, 0.3f);
	}

	//Score is updated every frame in case collisions with other objects change it
	void Update () {
		scoreText.text = "Score: " + score.ToString ();
	}

	void FixedUpdate () {
		//Checks if the ball falls out of the board
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
				//When out of balls
				Destroy (this);
			}	
		}
	}
    public int getScore() {
        return score;
    }
    
}
