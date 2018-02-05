﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableContainer : MonoBehaviour {
    public StoryTrigger trig;
    public BallBehaviour bll;
   
    public string task = "";
    public string cond = "";
    public int bumpCond;
    public int targetScore;
    public int currentScore;
    public bool taskActive;


	void Start () {
        trig = GameObject.Find("StoryElement").GetComponent<StoryTrigger>();
        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();

       
     
 
	}
    public void SetTaskAndCond(string task, string cond) {
        this.task = task;
        this.cond = cond;




    }

	void Update () {

        // Set the task 
        if (task != "" && cond != "" && !bll.taskStatus()) {
            if (task == "bumpers") {

                bumpCond = int.Parse(cond);

                currentScore = bll.getScore();
                targetScore = currentScore + bumpCond;
                taskActive = true;
                bll.setTaskActive();
               

                trig.disable();
            }

     





        }
        // if task is active in ball, update task
        if (bll.taskStatus()) {

            // check task type

            if (task =="bumpers") {
                currentScore = bll.getScore();
                // Check task condition, if met reset task data
                if (currentScore >= targetScore ) {
                    task = "";
                    cond = "";
                    taskActive = false;
                    bll.disableTask();
                    currentScore = 0;
                    targetScore = 0;


                    trig.setEnabled();

                }


            }




        }


		
	}
}
