using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject sword;
    //public MeshRenderer blade;
    public ParticleSystem boostEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem bladeEffect;
    /// <summary>
    /// the number of the controller curently giving input to this pawn
    /// </summary>
    public int controllerNum = 1;


    [HideInInspector]
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


   



    private PlayerState playerState;

    // Use this for initialization
    void Start() {
        SetInputs();
        SwitchPlayerState(new PlayerStateNormal());
    }

    // Update is called once per frame
    void Update() {
        //print(playerState);
        if (playerState != null) {
            
            PlayerState newState = playerState.Update();
            SwitchPlayerState(newState);
        }
    }


    private void SwitchPlayerState(PlayerState newState) {
       // print(xButton + " reads: " + Input.GetButton(xButton));
        if (newState != null) {

            if (playerState != null) playerState.OnExit();
            playerState = newState;
            playerState.OnEnter(this);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("DealsDamage")) Debug.Log("touching");
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
