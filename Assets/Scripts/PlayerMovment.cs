using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    float speed = 20;
    float friction = .8f;
    public bool invertX = false;
    public bool invertZ = true;




    

    // float MouseSensitivityX;
    //float MouseSensitivityY;

    CharacterController pawn;
    Camera cam;


    // Use this for initialization
    void Start() {
        pawn = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        //MouseSensitivityX = LookSensitivity;
        //MouseSensitivityY = LookSensitivity;
    }

    // Update is called once per frame
    void Update() {
        
        DoMove();
    }

    private void DoMove() {
        Vector3 velocity = Vector3.zero;

        float x = Input.GetAxis("LeftJoystick_X")*(invertX ? -1 : 1);
        float z = Input.GetAxis("LeftJoystick_Z")*(invertZ ? -1 : 1);
        //print(x + "," + z);

        velocity.x += x * (speed*Time.deltaTime);
        velocity.z +=  z * (speed*Time.deltaTime);

        transform.position += velocity;

        velocity *= friction;

        //pawn.SimpleMove(velocity);
    }

  
}
