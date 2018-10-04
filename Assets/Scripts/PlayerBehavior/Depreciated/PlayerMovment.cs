using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class handles the movment of the player character within the scene
/// </summary>
public class PlayerMovment : MonoBehaviour {

    /*
     Unused tap detection variables
      const float TAP_TIMER = .1f;
      bool listenForBoost = false;
      bool[] xyTap = new bool[2];
      float tapDetectionTimer;
      Vector2 leftStickPrev;
     */

   

    

    



    /// <summary>
    /// this contains all the diffrent movment states
    /// </summary>
    enum MovmentState {
        Run,
        Dash,
        Grapple,
        Jump
    }

    /// <summary>
    /// this contains the current state of the player 
    /// </summary>
    MovmentState playerState;




    /// <summary>
    /// Pawn is a refrence to the player controller
    /// </summary>
    CharacterController pawn;



    /// <summary>
    /// This function is called when the class is instantiated. 
    /// </summary>
    void Start() {
        playerState = MovmentState.Run;
        
       
        //cam = GetComponentInChildren<Camera>();
        //MouseSensitivityX = LookSensitivity;
        //MouseSensitivityY = LookSensitivity;
    }

    /// <summary>
    /// This function runs the relevent state logic baised on playerState. it is called every frame.
    /// </summary>
    void Update() {

        switch (playerState) {
            case MovmentState.Run:
               // DoRun();
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

    /// <summary>
    /// This function sets the players acceleration baised on the left stick and whether or not the player is boosting. 
    /// </summary>
    



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
