using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    float impulse;
    int shotBy;
    Vector2 direction;
    Rigidbody physics;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Vector2 aiming,int characterNum ) {
        shotBy = characterNum;
        direction = aiming;
    }
}
