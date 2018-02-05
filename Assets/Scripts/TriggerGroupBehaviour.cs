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

    void Start()
    {
        cylinders = transform;
        states = new bool[cylinders.childCount];

        for(var i = 0; i < states.Length; i++)
        {
            Debug.Log(cylinders.GetChild(i));
            Debug.Log(states[i]);
        }
        Debug.Log("hello world");
    }
    void Update()
    {
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("parent laughs tooooo");
        collision.gameObject.GetComponent<Material>().color = Color.red;
    }
    */

    void ComponentCollided(GameObject child)
    {
        Debug.Log("oh mama");
        //child.GetComponent<Material>().color = Color.red;
        var material = child.GetComponent<Renderer>().material;

        var objectIndex = child.transform.GetSiblingIndex();

        states[objectIndex] = !states[objectIndex];
        if (states[objectIndex])
            material.color = Color.cyan;
        else
            material.color = Color.red;
    }
    
}
