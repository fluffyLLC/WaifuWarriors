using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// this is a refrence to the sword game object
    /// </summary>
    public GameObject sword;
    /// <summary>
    /// This is a refrence to teh players mele colision volume
    /// </summary>
    public GameObject meleColisionVolume;
    /// <summary>
    /// this is a refrence to the player's boost particle effect
    /// </summary>
    public ParticleSystem boostEffect;
    /// <summary>
    /// this is a refrence to the player's dash particle effect
    /// </summary>
    public ParticleSystem dashEffect;
    /// <summary>
    /// this is a refrence to the player's blade particle effect
    /// </summary>
    public ParticleSystem bladeEffect;
    /// <summary>
    /// the number of the controller curently giving input to this pawn
    /// </summary>
    public int controllerNum = 1;

    
    [HideInInspector]
    /// <summary>
    /// this vec2 contains the last non 0 direction of the left joystick
    /// </summary>
    public Vector2 prevFacing;

    [HideInInspector]
    /// <summary>
    /// The string values recognides as button presses on the joystick, set in SetInputs()
    /// </summary>
    public string aButton, bButton, xButton, yButton, leftBumber, rightBumper, backButton, startButton, leftStickClick, rightStickClick;

    [HideInInspector]
    /// <summary>
    /// The string values recognides as axeses on the joystick, set in SetInputs()
    /// </summary>
    public string leftJoystickX, leftJoystickZ, rightJoystickX, rightJoystickY, dPadX, dPadY, triggers, leftTrigger, rightTrigger;


   


    /// <summary>
    /// this contains the players current state
    /// </summary>
    private PlayerState playerState;


    // Use this for initialization
    /// <summary>
    /// this finction initalises this class
    /// </summary>
    void Start() {
        SetInputs();
        SwitchPlayerState(new PlayerStateNormal());
    }

    // Update is called once per frame
    /// <summary>
    /// this function is called every frame and runs the player state logic
    /// </summary>
    void Update() {
        //print(playerState);
        if (playerState != null) {
            
            PlayerState newState = playerState.Update();
            SwitchPlayerState(newState);
        }
    }

    /// <summary>
    /// This function switches to a new state and runs the on exit and on erner functions contatined within the finctions
    /// </summary>
    /// <param name="newState"> The state to transition to </param>
    private void SwitchPlayerState(PlayerState newState) {
       // print(xButton + " reads: " + Input.GetButton(xButton));
        if (newState != null) {

            if (playerState != null) playerState.OnExit();
            playerState = newState;
            playerState.OnEnter(this);
        }
    }

    /// <summary>
    /// this function is called when the player is hit with a trigger volume
    /// </summary>
    /// <param name="hit">the collider that hits this player</param>
    void OnTriggerEnter(Collider hit) {
        //print();

       // if (hit.gameObject) { print("why are you hiting yourself?"); }

        if (hit.gameObject.CompareTag("MeleStrike")) {
            print(playerState.ToString());
            if (playerState.ToString() != "PlayerStateDash" || playerState.ToString() != "PlayerStateDash") {
                SwitchPlayerState(new PlayerStateHit());
                Transform StrikerLocation = hit.GetComponentInParent<Transform>();
                playerState.GetActorTransform(StrikerLocation);
            }

        } //Debug.Log("touching");
    }

    /// <summary>
    /// Assighns the string values of each controller input useing controller num as a prefix to differentiate the controlers 
    /// </summary>
    protected void SetInputs() {
        //Debug.Log("seeting up");
        aButton = controllerNum + "_A_Button";
        bButton = controllerNum + "_B_Button";
        xButton = controllerNum + "_X_Button";
        yButton = controllerNum + "_Y_Button";
        leftBumber = controllerNum + "_LeftBumper";
        rightBumper = controllerNum + "_RightBumper";
        backButton = controllerNum + "_BackButton";
        startButton = controllerNum + "_StartButton";
        rightStickClick = controllerNum + "_RightStickClick";
        leftStickClick = controllerNum + "_LeftStickClick";
        leftJoystickX = controllerNum + "_LeftJoystick_X";
        leftJoystickZ = controllerNum + "_LeftJoystick_Z";
        rightJoystickX = controllerNum + "_RightJoyStick_X";
        rightJoystickY = controllerNum + "_RightJoyStick_Y";
        dPadX = controllerNum + "_D-Pad_X";
        dPadY = controllerNum + "_D-Pad_Y";
        triggers = controllerNum + "_Triggers";
        leftTrigger = controllerNum + "_LeftTrigger";
        rightTrigger = controllerNum + "_RightTrigger";

    }


}
