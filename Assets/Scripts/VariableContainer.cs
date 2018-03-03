using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableContainer : MonoBehaviour {
    public StoryTrigger trig;
    public GameObject[] triggers;
    public BallBehaviour bll;
    public TriggerGroupBehaviour abcTriggerGroup;
    public TargetGroupBehavior targets;
    public GateBehavior gate;
    public NextRoomBehaviour nextRoom;
    public NextRoomDoorBehaviour nextRoomDoor;
    public TowerBehaviour trap;
    public CastleBehaviour castleBehaviour;

    public char[] abcTriggers;
   
    public string task = "";
    public string cond = "";
    public int bumpCond;
    public int targetScore;
    public int currentScore;
    public bool taskActive;
    public Text tasktext;
    public int passCond;

    private AudioSource audioSource;
    private AudioClip onemoreturn;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        trig = GameObject.Find("StoryElement").GetComponent<StoryTrigger>();
        triggers = GameObject.FindGameObjectsWithTag("storypad");

        abcTriggerGroup = GameObject.Find("ABCTriggerGroup").GetComponent<TriggerGroupBehaviour>();
        gate = GameObject.Find("Gateway").GetComponent<GateBehavior>();
        targets = GameObject.Find("Targets").GetComponent<TargetGroupBehavior>();
        trap = GameObject.Find("TrapWall").GetComponent<TowerBehaviour>();

        // these are used to refer the index of each trigger
        abcTriggers = new char[] { 'A', 'B', 'C' };

        gate = GameObject.Find("Gateway").GetComponent<GateBehavior>();
        targets = GameObject.Find("Targets").GetComponent<TargetGroupBehavior>();
        nextRoom = GameObject.Find("NextRoom").GetComponent<NextRoomBehaviour>();
        nextRoomDoor = GameObject.Find("NextRoomDoor").GetComponent<NextRoomDoorBehaviour>();

        castleBehaviour = GameObject.Find("TowerRoom").GetComponent<CastleBehaviour>(); // Fetch castle behavior from Tower room.


        bll = GameObject.Find("Ball").GetComponent<BallBehaviour>();

        print("bll " + bll);
        print("abc  " + abcTriggerGroup);

        tasktext.text = "Task: "; // Set initial task text
       
     
 
	}
    public void SetTaskAndCond(string task, string cond) {
        this.task = task;
        this.cond = cond;
    }

	void Update () {

        // Set the task 
        if (task != "" && cond != "" && !bll.taskStatus()) {
           
            audioSource.PlayOneShot(onemoreturn, 1f);
            // checking the task type.
            if (task=="main") {
                Debug.Log("Setting task " + task);
                //ei toimi
                GameObject.Find("Canvas").GetComponent<PauseMenu>().endScreenUI.SetActive(true);

            }
            if (task == "bumpers")
            {
                Debug.Log("Setting task " + task);

                bumpCond = int.Parse(cond);
                tasktext.text = "Task: Raise your score with " + cond + " points."; // set the task text to indicate score challenge
                currentScore = bll.getScore();
                targetScore = currentScore + bumpCond;
                taskActive = true;
                bll.setTaskActive();
            }

            else if (task == "ABCtriggers")
            {
                Debug.Log("Setting task " + task);
                tasktext.text = "Task: Hit the ABC triggers in the following order: " + cond + ".\nProgress: "; // set the task text to indicate score challenge
                // turn the condition (= string) into a int array
                int[] goal = new int[cond.Length];
                for (var i = 0; i < cond.Length; i++)
                {
                    goal[i] = (Array.IndexOf(abcTriggers, cond.ToCharArray()[i]));
                }
                abcTriggerGroup.SetTaskActive(goal);
                taskActive = true;
                bll.setTaskActive();



            } else if (task == "targets") {
                Debug.Log("Setting task " + task);
                targets.initTask();
                Debug.Log("TARGETS RAISED");
                tasktext.text = "Task: Hit all the targets";
                taskActive = true;
                bll.setTaskActive();



            }
            else if (task == "gate") {
                Debug.Log("Setting task " + task);
                passCond = int.Parse(cond);
                taskActive = true;
                bll.setTaskActive();
				tasktext.text = "Task: Pass through the smallest ramp " + cond + " Times.\nTimes passed: " + gate.getTimesPassed();


            }
            else if (task == "trap") {
                Debug.Log("Setting task " + task);
                taskActive = true;
                bll.setTaskActive();
                if (!castleBehaviour.isOpen()) { castleBehaviour.OpenDoors(); }
                

				tasktext.text = "Task: Infiltrate the tower!";

            }
            else if (task == "enterNextRoom")
            {
                Debug.Log("Setting task " + task);
                bll.setTaskActive();
                nextRoom.setTaskActive();
				tasktext.text = "Task: " + cond + " by entering the room on top of the board";
                taskActive = true;
            }
            // this task will get next task automatically when completed
            else if (task == "openDoor")
            {
                Debug.Log("Setting task " + task);
                tasktext.text = "Task: Hit the ABC triggers in the following order: " + cond + ".\nProgress: "; // set the task text to indicate score challenge
                // turn the condition (= string) into a int array
                int[] goal = new int[cond.Length];
                for (var i = 0; i < cond.Length; i++)
                {
                    goal[i] = (Array.IndexOf(abcTriggers, cond.ToCharArray()[i]));
                }
                abcTriggerGroup.SetTaskActive(goal);
                taskActive = true;
                bll.setTaskActive();

                print("commanding to pne doors");
                //castleBehaviour.OpenDoors();


            }

            // Disable story triggers
            for (int i = 0; i < triggers.Length; i++)
            {
                Debug.Log("Disabled triggers: " + i);
                triggers[i].GetComponent<StoryTrigger>().disable();
              

            }
        }
        // if task is active in ball, update task
        if (bll.taskStatus()) {

            // check task type
            
            if (task == "bumpers") {
                currentScore = bll.getScore();
                // Check task condition, if met reset task data
                if (currentScore >= targetScore) {
                    disableTask();
                }
            }
            else if (task == "ABCtriggers")
            {
                // get the current progress
                var progress = cond.Substring(0, abcTriggerGroup.GetTaskProgress());
                tasktext.text = "Task: Hit the ABC triggers in the following order: " + cond + ".\nProgress: " + progress;

                // check if the task is completed
                if (abcTriggerGroup.isTaskCompleted())
                {

                    disableTask();
                }


            }
            else if (task == "targets") {
                if (targets.isTaskFinished()) {
                    Debug.Log("TARGET TASK FINISHED, DISABLING TASK");
                    disableTask();

                }

            }
            else if (task == "gate") {
                if (gate.getTimesPassed() >= passCond)
                {
                    disableTask();
					gate.setTimesPassed (0);

                }
                else {
					tasktext.text = "Task: Pass through the smallest ramp " + cond + " Times.\nTimes passed: " + gate.getTimesPassed();

                }

            } else if (task == "trap") {
                if (trap.isCaptured()) {

                    disableTask();

                }


            }
            else if (task == "enterNextRoom")
            {
                if (nextRoom.isTaskCompleted())
                {
                    disableTask();
                    trig.ActivateNextTask();
                    // close the door
                    //nextRoomDoor.SwitchDoor();
                }


            }
            // this task will get next task automatically when completed
            else if (task == "openDoor")
            {
                // get the current progress
                var progress = cond.Substring(0, abcTriggerGroup.GetTaskProgress());
                tasktext.text = "Task: Hit the ABC triggers in the following order: " + cond + ".\nProgress: " + progress;

                // check if the task is completed
                if (abcTriggerGroup.isTaskCompleted())
                {
                    disableTask();
                    trig.ActivateNextTask();
                    // open the door
                    nextRoomDoor.SwitchDoor();
                }


            }
        }
	}

    // used when a task has been completed and a new one is ready to be accepted
    private void disableTask()
    {
        task = "";
        cond = "";
        tasktext.text = "Task: ";
        taskActive = false;
        bll.disableTask();
        currentScore = 0;
        targetScore = 0;
        // enable story triggers
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].GetComponent<StoryTrigger>().setEnabled();
    
        }
    }

    // Can be used to determine when the ball falls of the board
    // !!!! requires for the ball's have locked physics for the Y-axis, otherwise the player will lose if the ball jumps a bit
    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Rigidbody>() != null)
        {
            //Force to a forward direction (Z-axis of the trigger) is added 
            //to the collision component that has a rigidbody (the ball)
            //col.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            print("exited trigger");
            //Incrementing score that's in the ball script
            //BallBehaviour2 bb = col.gameObject.GetComponent<BallBehaviour2>();
            //bb.ResetBall();
        }
    }
}
