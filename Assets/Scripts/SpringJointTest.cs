using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class SpringJointTest : MonoBehaviour {

    SpringJoint grappel;
    public GameObject cube;

	// Use this for initialization
	void Start () {
        grappel = GetComponent<SpringJoint>();
        //cube = GetComponentInChildren<GameObject>();
        grappel.autoConfigureConnectedAnchor = false;
	}
	
	// Update is called once per frame
	void Update () {
        cube.transform.position = grappel.connectedAnchor;
        if (Input.GetButtonDown("A_Button")) {
            grappel.connectedAnchor = grappel.connectedAnchor + (Vector3.forward * 5);
            
        } else if (Input.GetButtonDown("Y_Button")) {
            grappel.connectedAnchor = grappel.connectedAnchor + (Vector3.back * 5);
           
        } else if (Input.GetButtonDown("B_Button")) {
            grappel.connectedAnchor = grappel.connectedAnchor + (Vector3.right * 5);

        } else if (Input.GetButtonDown("X_Button")) {
            grappel.connectedAnchor = grappel.connectedAnchor + (Vector3.left * 5);

        }
    }
}
