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
    /// this const determins the length of the players boost
    /// </summary>
    const float BOOST_TIMER = .3f;
    /// <summary>
    /// this const determins the length of the boost cooldown
    /// </summary>
    const float BOOST_COOLDOWN = 2;
    /// <summary>
    /// this const sets the players additional boost speed
    /// </summary>
    const float BOOST = 2;
    /// <summary>
    /// this const sets the players standard speed
    /// </summary>
    const float SPEED = 3.5f;
    /// <summary>
    /// this float determins the rate of friction on velocity
    /// </summary>
    float friction = .9f;
    /// <summary>
    /// this inverts the z axis of movment
    /// </summary>
    public bool invertX = false;
    /// <summary>
    /// this inverts the z axis of movment
    /// </summary>
    public bool invertZ = true;
    /// <summary>
    /// this is true when the player is boosting
    /// </summary>
    bool applyingBoost = false;
    /// <summary>
    /// this times th elength of the players boost in seconds
    /// </summary>
    float boostTimer;
    /// <summary>
    /// this times the boost cooldown in seconds
    /// </summary>
    float boostCooldown;
    /// <summary>
    /// this contains the players velocity
    /// </summary>
    Vector3 velocity = Vector3.zero;
    /// <summary>
    /// this vector contains the players acceleration
    /// </summary>
    Vector3 acceleration = Vector3.zero;

    /// <summary>
    /// the number of the controller curently giving input to this pawn
    /// </summary>
    public int controllerNum = 1;

    /// <summary>
    /// the string text for the a button input
    /// </summary>
    string aButton;
    /// <summary>
    /// the string text for the b button input
    /// </summary>
    string bButton;
    /// <summary>
    /// the string text for the x button input
    /// </summary>
    string xButton;
    /// <summary>
    /// the string text for the y button input
    /// </summary>
    string yButton;
    /// <summary>
    /// the string text for the joystick x input
    /// </summary>
    string leftJoystickX;
    /// <summary>
    /// the string text for the left joystick z  input
    /// </summary>
    string leftJoystickZ;
    /// <summary>
    /// the string text for the right joystick x input
    /// </summary>
    string rightJoystickX;
    /// <summary>
    /// the string text for the right right joystick y input
    /// </summary>
    string rightJoystickY;
    /// <summary>
    /// the string text for the left stick click input
    /// </summary>
    string leftStickClick;



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
        pawn = GetComponent<CharacterController>();
        aButton = controllerNum + "_A_Button";
        bButton = controllerNum + "_B_Button";
        xButton = controllerNum + "_X_Button";
        yButton = controllerNum + "_Y_Button";
        leftJoystickX = controllerNum + "_LeftJoystick_X";
        leftJoystickZ = controllerNum + "_LeftJoystick_Z";
        rightJoystickX = controllerNum + "_RightJoyStick_X";
        rightJoystickY = controllerNum + "_RightJoyStick_Y";
        leftStickClick = controllerNum + "_LeftStickClick";
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

    /// <summary>
    /// This function sets the players acceleration baised on the left stick and whether or not the player is boosting. 
    /// </summary>
    void DoRun() {
        float x = Input.GetAxis(leftJoystickX) * (invertX ? -1 : 1);
        float z = Input.GetAxis(leftJoystickZ) * (invertZ ? -1 : 1);

        //print(x);
        print(x + "," + z);
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

    /// <summary>
    /// This function moves the player using accelaeration, velocity and simple move
    /// </summary>
    void DoMove() {

        velocity += acceleration;

        pawn.SimpleMove(velocity);

        velocity *= friction;
    }

    /// <summary>
    /// This function returnes true if the player should be boosting.
    /// </summary>
    /// <returns> Returns true if the boost cooldown is 0, and boost timer is above 0 </returns>
    bool ShouldApplyBoost() {
        if (boostTimer > 0) {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0) {
                applyingBoost = false;
                boostTimer = 0;
                boostCooldown = BOOST_COOLDOWN;
            }
            return true;
        } else if (boostCooldown > 0) {
            boostCooldown -= Time.deltaTime;
            if (boostCooldown <= 0) {
                boostCooldown = 0;
            }
        } else if (Input.GetButtonDown(leftStickClick)) {
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
