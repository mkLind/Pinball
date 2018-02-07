using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableContainer : MonoBehaviour {
    private StoryTrigger trig;
    private BallBehaviour bll;
    public string task = "";
    public string cond = "";
    public int bumpCond = 0;
    public int targetScore;
    public int currentScore;
    public bool taskActive;


	void Start () {
        trig = GetComponent<StoryTrigger>();
        bll = GetComponent<BallBehaviour>();
        taskActive = false;
        targetScore = 0;
        currentScore = 0;

	}
    public void setTaskAndCond(string task, string cond) {
        this.task = task;
        this.cond = cond;




    }

	void Update () {

        
        if (task != "" && cond != "" && !taskActive) {
            if (task == "bumper") {

                bumpCond = int.Parse(cond);

                currentScore = bll.getScore();
                targetScore = currentScore + bumpCond;
                taskActive = true;
                trig.disable();
            }





        }
        if (taskActive) {
            if (task =="bumper") {
                currentScore = bll.getScore();

                if (currentScore >= targetScore ) {
                    task = "";
                    cond = "";
                    taskActive = false;

                    trig.setEnabled();

                }


            }


        }


		
	}
}
