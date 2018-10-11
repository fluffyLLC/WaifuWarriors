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
    const float SPEED = 3f;
    /// <summary>
    /// this float determins the rate of friction on velocity
    /// </summary>
    float friction = .9f;
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
    /// this function id caled every frame, and runs most of the behaviors contained within this class
    /// </summary>
    /// <returns>The next scene to transition to. Returns null if no transition should take place.</returns>
    override public PlayerState Update() {
        DoRun();

        if (Input.GetButtonDown(controller.xButton)) return new PlayerStateMele();
        if (Input.GetButtonDown(controller.bButton)) return new PlayerStateDash();
        return null;
    }

    /// <summary>
    /// this functioin contains most of the behavior that defines simple player movement
    /// </summary>
    void DoRun() {

        Vector2 direction = ForwardVector();

        if (direction != Vector2.zero) {
            PlayerLook(direction);
            controller.prevFacing = direction;
        }
        
        float totalSpeed;

        if (ShouldApplyBoost()) {

            totalSpeed = SPEED + BOOST;

        } else {

            totalSpeed = SPEED;
        }

        acceleration = Vector3.Normalize(new Vector3(direction.x, 0, direction.y)) * totalSpeed;

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
        } else if (Input.GetButtonDown(controller.leftStickClick)) {

            boostTimer = BOOST_TIMER;
            applyingBoost = true;
            controller.boostEffect.Play();
        }

        return false;
    }

 

    override public void OnEnter(PlayerController controller) {
        this.controller = controller;
        //SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }

   
    
}
