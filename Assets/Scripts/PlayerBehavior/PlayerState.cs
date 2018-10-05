using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

    /// <summary>
    /// the string text for the a button input
    /// </summary>
    protected string aButton;
    /// <summary>
    /// the string text for the b button input
    /// </summary>
    protected string bButton;
    /// <summary>
    /// the string text for the x button input
    /// </summary>
    protected string xButton;
    /// <summary>
    /// the string text for the y button input
    /// </summary>
    protected string yButton;
    /// <summary>
    /// the string text for the left bumper input
    /// </summary>
    protected string leftBumber;
    /// <summary>
    /// the string text for the right bumper input
    /// </summary>
    protected string rightBumper;
    /// <summary>
    /// the string text for the back button input
    /// </summary>
    protected string backButton;
    /// <summary>
    /// the string text for the start button input
    /// </summary>
    protected string startButton;
    /// <summary>
    /// the string text for the left stick click input
    /// </summary>
    protected string leftStickClick;
    /// <summary>
    /// the string text for the right Stick click input
    /// </summary>
    protected string rightStickClick;
    /// <summary>
    /// the string text for the joystick x input
    /// </summary>
    protected string leftJoystickX;
    /// <summary>
    /// the string text for the left joystick z  input
    /// </summary>
    protected string leftJoystickZ;
    /// <summary>
    /// the string text for the right joystick x input
    /// </summary>
    protected string rightJoystickX;
    /// <summary>
    /// the string text for the right joystick y input
    /// </summary>
    protected string rightJoystickY;
    /// <summary>
    /// the string text for the  D-Pad X input
    /// </summary>
    protected string dPadX;
    /// <summary>
    /// the string text for the D-Pad Y input
    /// </summary>
    protected string dPadY;
    /// <summary>
    /// the string text for the triggers input
    /// </summary>
    protected string triggers;
    /// <summary>
    /// the string text for the left trigger input
    /// </summary>
    protected string leftTrigger;
    /// <summary>
    /// the string text for the right trigger input
    /// </summary>
    protected string rightTrigger;



    /// <summary>
    /// A refrence to the Character controller componnent on the player
    /// </summary>
    protected CharacterController pawn;


    /// <summary>
    /// Assighns the string values of each controller input useing controller num as a prefix to differentiate the controlers 
    /// </summary>
    /// <param name="controllerNum">The number assighned to the controller giving input to this class. Corresponds with the joystick number.</param>
    protected void SetInputs(int controllerNum) {
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

    /// <summary>
    /// A refrence to the Player controller component that governs over PlayerState 
    /// </summary>
    protected PlayerController controller;

    /// <summary>
    /// an abstract class that will be used to update the playstate that overrides it.
    /// </summary>
    /// <returns>The next scene to transition to. Returns null if no transition should take place.</returns>
    abstract public PlayerState Update();

    /// <summary>
    /// Should be called when a player state class is instantiated to set up any relevant logic
    /// </summary>
    /// <param name="controller"> the player controller class that instantiatyed this playerstate class </param>
    virtual public void OnEnter(PlayerController controller) {

        this.controller = controller;
        SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Should be called before this class is removed 
    /// </summary>
    abstract public void OnExit();

   

}
