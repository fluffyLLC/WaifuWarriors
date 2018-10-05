using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNormal : PlayerState {

    /// <summary>
    /// this const determins the length of the players boost
    /// </summary>
    const float BOOST_TIMER = .4f;
    /// <summary>
    /// this const determins the length of the boost cooldown
    /// </summary>
    const float BOOST_COOLDOWN = 1;
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


    override public PlayerState Update() {
        DoRun();


        if (CheckForMele()) return new PlayerStateMele();
        return null;
    }
    
    void DoRun() {
        //Debug.

        float x = Input.GetAxis(leftJoystickX) * (invertX ? -1 : 1);
        float z = Input.GetAxis(leftJoystickZ) * (invertZ ? -1 : 1);

        //print(x);
        Debug.Log(x + "," + z);
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
               // controller.boostEffect.Stop();
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
            controller.boostEffect.Play();
        }
        return false;
    }

    private bool CheckForMele() {
        return Input.GetButtonDown(xButton);
    }

    override public void OnEnter(PlayerController controller) {
        this.controller = controller;
        SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }

    override public void OnExit() { }

    
}
