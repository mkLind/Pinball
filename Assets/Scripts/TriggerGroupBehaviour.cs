using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This Script handles the TriggerGroup object's logic.
 * 
 * It keeps track of each obstacle's state (on or off) and 
 * 
 */
public class TriggerGroupBehaviour : MonoBehaviour {

    // can be used to access all child cylinder components
    Transform cylinders;
    // if each cylinder is on or off
    bool[] states;
    bool taskActive;
    bool taskCompleted;
    // this list contains the pattern in which the triggers should be hit to complete goal
    // for example [2, 1, 0] means the triggers should be hit in that order to succeed
    // if any other trigger is hit in between, the progress is reset
    int[] goal;
    int currentGoalIndex;

    void Start()
    {
        cylinders = transform;
        states = new bool[cylinders.childCount];
        taskActive = false;

        for (var i = 0; i < states.Length; i++)
        {
            Debug.Log(cylinders.GetChild(i));
            Debug.Log(states[i]);
        }
        Debug.Log("hello world");
    }
    void Update()
    {
    }


    void ComponentCollided(GameObject child)
    {
        Debug.Log("oh mama");
        //child.GetComponent<Material>().color = Color.red;
        var material = child.GetComponent<Renderer>().material;

        var objectIndex = child.transform.GetSiblingIndex();

        // if ABC trigger task is active, need to use different rules
        if (taskActive)
        {
            if (objectIndex == currentGoalIndex)
            {
                material.color = Color.cyan;
                states[objectIndex] = true;
                // check if task goal was reached
                if (currentGoalIndex + 1 == goal.Length)
                {
                    taskCompleted = true;
                    taskActive = false;
                    ResetTriggers();
                } else
                {
                    currentGoalIndex++;
                }
            }
            else
            {
                ResetTriggers();
                currentGoalIndex = 0;
            }
        }
        // if ABC trigger task is not active
        else
        {
            states[objectIndex] = !states[objectIndex];
            if (states[objectIndex])
                material.color = Color.cyan;
            else
                material.color = Color.red;
        }
    }

    public void ResetTriggers()
    {
        for (var i = 0; i < states.Length; i++)
        {
            states[i] = false;
            cylinders.GetChild(i).gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void SetTaskActive(int[] goal)
    {
        this.goal = goal;
        ResetTriggers();
        taskActive = true;
        taskCompleted = false;
    }

    public bool isTaskCompleted()
    {
        return taskCompleted;
    }

    // returns the index of how far along the goal the player is
    public int GetTaskProgress()
    {
        return currentGoalIndex;
    }
    
}
