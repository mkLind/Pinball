using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponentBehaviour : MonoBehaviour {

    private GameObject parent; 

	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        // the parent element keeps track of the states of each component in this trigger group
        // => inform the parent that this component has been hit
        parent.SendMessage("ComponentCollided", transform.gameObject);
    }
}
