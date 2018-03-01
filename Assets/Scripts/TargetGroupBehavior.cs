using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGroupBehavior : MonoBehaviour {
    public GameObject[] targets;
    public int target_amount;
    public int targets_hit;
    public bool taskActive;
    public Dictionary<int, int> status;
	// Use this for initialization
	void Start () {
        status = new Dictionary<int, int>();
        targets = GameObject.FindGameObjectsWithTag("Target");
        //target_amount = targets.Length;
		target_amount = 3;	//ei toimi muuten jos ei laita määrää
        targets_hit = 0;
        taskActive = false;
	}
    // If the amount of hit targets is target amount, then task is finished
    public bool isTaskFinished() { return status.Count == target_amount; }



    public void initTask() {
        taskActive = true;
        status.Clear();
        for (int i = 0; i<targets.Length;i++) {
            targets[i].GetComponent<TargetBehavior>().raise();

        }
    }
	// Update is called once per frame
	void Update () {
        // Loop through all the targets and check if they are hit
        for (int i = 0; i<targets.Length;i++) {
            if (targets[i].GetComponent<TargetBehavior>().isHit()) {
             // IF hit target is not already added to hit targets, add it.
                if (!status.ContainsKey(i)) {
                    targets_hit += 1;

                    status.Add(i, targets_hit);
                

                }
            }
        }
       
	}
}
