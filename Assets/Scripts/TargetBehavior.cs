using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour {
    public Rigidbody target;
    public BallBehaviour bll;
    public bool raised;
    public bool lowered;
    public bool goingUP;
    public bool goingDOWN;
    public bool hit;
    public Vector3 origpos;
    public Animator anim;
    RigidbodyConstraints constr;
    public int rise;
    public int hithash;



	// Use this for initialization
	void Start () {
        rise = Animator.StringToHash("Raise");
        hithash = Animator.StringToHash("Hit");


        anim = GetComponent<Animator>();
        
        target = GetComponent<Rigidbody>();
        raised = false;
        lowered = true;
        goingUP = false;
        goingDOWN = false;
        hit = false;
        constr = target.constraints;
        origpos = target.position;
        bll = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallBehaviour>();
        freeze();

    }
    // Freeze position of the target
    void freeze() {

        target.constraints = RigidbodyConstraints.FreezePositionX;
        target.constraints = RigidbodyConstraints.FreezePositionY;
        target.constraints = RigidbodyConstraints.FreezePositionZ;

    }

    void Update()
    {
        // If target goes upp, unfreeze and move. IF going down, same thing
        if (goingUP) {
            target.constraints = constr;
            raiseTarget();
        }
        else if (goingDOWN) {
            target.constraints = constr;
            lowerTarget();
        }

    }
    public void raise() { goingUP = true; }
    public void lower() { goingDOWN = true; }
    public bool isHit() { return hit; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (raised) {
                bll.score += 100;
                goingDOWN = true;
                lowerTarget();
                hit = true;
            }

        }
    }


   public void raiseTarget()
    {
        if (goingUP)
        {
            // change position
            anim.SetTrigger(rise);
            anim.ResetTrigger(hithash);

            //target.transform.position += new Vector3(0, 1.25f, 0);


        }
        // if long enough moved, then stop and set state to raised
       // if (target.position.y <= origpos.y + 1000f)
        if (anim.GetAnimatorTransitionInfo(0).IsName("Down -> Hit"))
            {
            goingUP = false;
            raised = true;
            lowered = false;

            freeze();
        }

    }

   public void lowerTarget()
    {
        if (goingDOWN)
        {
            anim.SetTrigger(hithash);
            anim.ResetTrigger(rise);


            //target.transform.position += new Vector3(0, -0.25f, 0);


        }
        //if (target.position.y == origpos.y)
        if (anim.GetAnimatorTransitionInfo(0).IsName("Hit -> Down"))
            {
            goingDOWN = false;
            raised = false;
            lowered = true;
            hit = false; // Needs to be set to unhit state
            freeze();
        }

    }

}
