using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehavior : MonoBehaviour {

    float impulse = 75;
    Vector3 acceleration;

    //RigidBody phy

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
        physics = GetComponent<Rigidbody>();
        shotBy = characterNum;
        direction = aiming;

        acceleration = Vector3.Normalize(new Vector3(direction.x, 0, direction.y)) * impulse;
        print(direction);

        physics.AddForce(acceleration, ForceMode.Impulse);
    }
}
