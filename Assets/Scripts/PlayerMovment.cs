﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    float speed = 150;
    float friction = .9f;
    public bool invertX = false;
    public bool invertZ = true;
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;





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
        
        float x = Input.GetAxis("LeftJoystick_X")*(invertX ? -1 : 1);
        float z = Input.GetAxis("LeftJoystick_Z")*(invertZ ? -1 : 1);

        acceleration = new Vector3(x * (speed * Time.deltaTime), 0, z * (speed * Time.deltaTime));

        velocity += acceleration;
        
        pawn.SimpleMove(velocity);

        velocity *= friction;
    }


}
