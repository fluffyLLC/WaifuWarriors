﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    const float TAP_TIMER = .1f;
    const float BOOST_TIMER = .3f;
    const float BOOST_COOLDOWN = 0;
    const float BOOST = 2;
    const float SPEED = 3;
    float friction = .9f;
    public bool invertX = false;
    public bool invertZ = true;
    bool applyingBoost = false;
    bool listenForBoost = false;
    bool[] xyTap = new bool[2];
    float tapDetectionTimer;
    float boostTimer;
    float boostCooldown;
    Vector2 leftStickPrev;
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    enum MovmentState {RunWalk,Dash,Grapple,Jump}





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

        float totalSpeed;

        if (ApplyBoost(new Vector2(Mathf.Abs(x), Mathf.Abs(z)))) {
            totalSpeed = SPEED + BOOST;
            print("Boosting" + x);
            
        } else {
            print(Mathf.Abs(x) + "," + Mathf.Abs(z));
            print(tapDetectionTimer);
            totalSpeed = SPEED;
        }
        
        acceleration = Vector3.Normalize(new Vector3(x,0,z)) * totalSpeed;

        velocity += acceleration;
        
        pawn.SimpleMove(velocity);

        velocity *= friction;


        
    }

    bool ApplyBoost(Vector2 leftStick) {

        if (leftStick == Vector2.zero && applyingBoost) {
            applyingBoost = false;
            boostTimer = 0;
        }

        if (!applyingBoost && boostCooldown <= 0 && boostTimer <=0) {
            if (DetectDoubleTap(leftStick)) {
                boostTimer = BOOST_TIMER;
                applyingBoost = true;
                return true;
            }
        } else if(boostCooldown > 0) {
            boostCooldown -= Time.deltaTime;
        } else if (boostTimer > 0) {
            boostTimer -= Time.deltaTime;
            return true;
        } else if (boostTimer <= 0 && applyingBoost) {
            boostCooldown = BOOST_COOLDOWN;
            boostTimer = 0;
            applyingBoost = false;
        }

            return false;
    }

    bool DetectDoubleTap(Vector2 leftStick) {
        if (listenForBoost) {
            tapDetectionTimer -= Time.deltaTime;
            if (tapDetectionTimer <= 0) {
                listenForBoost = false;
                tapDetectionTimer = 0;
                xyTap[0] = false;
                xyTap[1] = false;
            } else if (leftStick.x == 1 && xyTap[0] || leftStick.y == 1 && xyTap[1]) {
                //applyBoost = true;
                return true;
            }
        } else if ( leftStickPrev.x == 1 && leftStick.x != 1 || leftStickPrev.y == 1 && leftStick.y != 1 ) {
            listenForBoost = true;
            tapDetectionTimer = TAP_TIMER;
            if (leftStickPrev.x == 1) {
                xyTap[0] = true;
            } else {
                xyTap[1] = true;
            }
        }

        
        leftStickPrev = leftStick;
        return false;
    }

    void ResetBoost() {


    } 


}
