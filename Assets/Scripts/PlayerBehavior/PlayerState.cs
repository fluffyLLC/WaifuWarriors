﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

    
   
    /// <summary>
    /// this inverts the z axis of movment
    /// </summary>
    public bool invertX = false;
    /// <summary>
    /// this inverts the z axis of movment
    /// </summary>
    public bool invertZ = true;



    /// <summary>
    /// A refrence to the Character controller componnent on the player
    /// </summary>
    protected CharacterController pawn;


   

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
        //SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Should be called before this class is removed 
    /// </summary>
    abstract public void OnExit();

    /// <summary>
    /// This function returns a Vector2 determined by the x and y axis of the controller's left stick
    /// </summary>
    /// <returns></returns>
    public Vector2 ForwardVector() {
        return new Vector2(Input.GetAxis(controller.leftJoystickX) * (invertX ? -1 : 1), Input.GetAxis(controller.leftJoystickZ) * (invertZ ? -1 : 1));
    }

    public void PlayerLook(Vector2 target) {
        //Quaternion rotation =
        float angle = Mathf.Atan2(target.x, target.y);
        angle *= 180/Mathf.PI;
        //Debug.Log(angle);
        pawn.transform.eulerAngles = new Vector3(0, angle, 0);
        
    }

   

}
