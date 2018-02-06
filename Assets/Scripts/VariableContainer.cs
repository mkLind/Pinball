using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableContainer : MonoBehaviour {
    public StoryTrigger trig;
    public GameObject[] triggers;
    public BallBehaviour bll;
   
    public string task = "";
    public string cond = "";
    public int bumpCond;
    public int targetScore;
    public int currentScore;
    public bool taskActive;
    public Text tasktext;

	void Start () {
        trig = GameObject.Find("StoryElement").GetComponent<StoryTrigger>();
        triggers = GameObject.FindGameObjectsWithTag("storypad");


        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();


        tasktext.text = "Task: Hit a story panel."; // Set initial task text
       
     
 
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
                tasktext.text = "Task: Raise your score with " + cond + " points." ; // set the task text to indicate score challenge
                currentScore = bll.getScore();
                targetScore = currentScore + bumpCond;
                taskActive = true;
                bll.setTaskActive();

                for (int i = 0; i<triggers.Length;i++) {
                    triggers[i].GetComponent<StoryTrigger>().disable();

                }


                
                //trig.disable();
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
                    tasktext.text = "Task: Hit a story panel.";
                    taskActive = false;
                    bll.disableTask();
                    currentScore = 0;
                    targetScore = 0;
                    for (int i = 0; i < triggers.Length; i++)
                    {
                        triggers[i].GetComponent<StoryTrigger>().setEnabled();

                    }

                    //trig.setEnabled();
                   
                }


            }




        }


		
	}
}
