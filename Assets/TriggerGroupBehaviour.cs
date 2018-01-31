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

    //public GameObject[] cylinders;
    Transform cylinders;

    void Start()
    {
        cylinders = transform;

        foreach (Transform cylinder in cylinders)
        {
            print(cylinder);
        }
        Debug.Log("hello world");
    }
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HAHAHAHAHAHAHA");
    }
}
