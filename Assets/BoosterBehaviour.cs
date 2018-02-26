using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterBehaviour : MonoBehaviour {

    public float force = 400;
    private float directionZ;

    //When the ball collides with the bouncer this method is called
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ball")
        {

            //print("boosted");
            var normalizedVelocity = col.gameObject.GetComponent<Rigidbody>().velocity.normalized;
            normalizedVelocity.y = 0;
            normalizedVelocity.x = 0;
            print("normalized Velocity " + normalizedVelocity);
            print("multiplied" + normalizedVelocity * force);
            if (normalizedVelocity.z > 0)
                col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force));

            //Incrementing score that's in the ball script
            BallBehaviour bb = col.gameObject.GetComponent<BallBehaviour>();
            bb.score = bb.score + 100;
        }
    }
}
