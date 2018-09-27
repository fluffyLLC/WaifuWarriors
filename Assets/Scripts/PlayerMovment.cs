using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    /*
     Unused tap detection variables
      const float TAP_TIMER = .1f;
      bool listenForBoost = false;
      bool[] xyTap = new bool[2];
      float tapDetectionTimer;
      Vector2 leftStickPrev;
     */


    const float BOOST_TIMER = .3f;
    const float BOOST_COOLDOWN = 2;
    const float BOOST = 2;
    const float SPEED = 3.5f;
    float friction = .9f;
    public bool invertX = false;
    public bool invertZ = true;
    bool applyingBoost = false;
    
    float boostTimer;
    float boostCooldown;
    
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    

    enum MovmentState {
        Run,
        Dash,
        Grapple,
        Jump
    }

    MovmentState playerState;



    // float MouseSensitivityX;
    //float MouseSensitivityY;

    CharacterController pawn;
    //Camera cam;


    // Use this for initialization
    void Start() {
        playerState = MovmentState.Run;
        pawn = GetComponent<CharacterController>();
        //cam = GetComponentInChildren<Camera>();
        //MouseSensitivityX = LookSensitivity;
        //MouseSensitivityY = LookSensitivity;
    }

    // Update is called once per frame
    void Update() {

        switch (playerState) {
            case MovmentState.Run:
                DoRun();
                break;
            case MovmentState.Dash:
                break;
            case MovmentState.Grapple:
                break;
            case MovmentState.Jump:
                break;
            default:
                print("Error: PlayerMovement.playerState is out of bounds");
                break;
        }
    }

    void DoRun() {
        float x = Input.GetAxis("LeftJoystick_X") * (invertX ? -1 : 1);
        float z = Input.GetAxis("LeftJoystick_Z") * (invertZ ? -1 : 1);

        //print(x);

        float totalSpeed;

        if (ShouldApplyBoost()) {
            totalSpeed = SPEED + BOOST;
            //print("boosting");
        } else {
            totalSpeed = SPEED;
        }

        acceleration = Vector3.Normalize(new Vector3(x, 0, z)) * totalSpeed;

        DoMove();

    }

    private void DoMove() {

        velocity += acceleration;

        pawn.SimpleMove(velocity);

        velocity *= friction;
    }

    bool ShouldApplyBoost() {
        if (boostTimer > 0) {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0) {
                applyingBoost = false;
                boostTimer = 0;
                boostCooldown = BOOST_COOLDOWN;
            }
            return true;
        } else if(boostCooldown > 0) {
            boostCooldown -= Time.deltaTime;
            if(boostCooldown <= 0){
                boostCooldown = 0;
            }
        } else if (Input.GetButtonDown("LeftStickClick")) {
            boostTimer = BOOST_TIMER;
            applyingBoost = true;
        }
        return false;
    }

    

    /*
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

    */

}
