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
    /// the string text for the right right joystick y input
    /// </summary>
    protected string rightJoystickY;
    /// <summary>
    /// the string text for the left stick click input
    /// </summary>
    protected string leftStickClick;

    protected CharacterController pawn;

    protected void SetInputs(int controllerNum) {
        aButton = controllerNum + "_A_Button";
        bButton = controllerNum + "_B_Button";
        xButton = controllerNum + "_X_Button";
        yButton = controllerNum + "_Y_Button";
        leftJoystickX = controllerNum + "_LeftJoystick_X";
        leftJoystickZ = controllerNum + "_LeftJoystick_Z";
        rightJoystickX = controllerNum + "_RightJoyStick_X";
        rightJoystickY = controllerNum + "_RightJoyStick_Y";
        leftStickClick = controllerNum + "_LeftStickClick";
    }

    protected PlayerController controller;

    abstract public PlayerState Update();

    virtual public void OnEnter(PlayerController controller) {

        this.controller = controller;
        SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }

    abstract public void OnExit();

   

}
