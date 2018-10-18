﻿using System.Collections;
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
    const float SPEED = 2.5f;
    /// <summary>
    /// This const defines the force applied by gravity
    /// </summary>
    const float GRAVITY = 20;
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

    float rightTriggerPrev = 0;

    /// <summary>
    /// this function id caled every frame, and runs most of the behaviors contained within this class
    /// </summary>
    /// <returns>The next scene to transition to. Returns null if no transition should take place.</returns>
    override public PlayerState Update() {
        Vector2 direction = ForwardVector();
        PlayerLook(direction);
        Vector2 aiming = AimVector();

        if (!HandleAim(aiming, direction)) {
            DoRun();
        }

        controller.prevFacing = direction;
        controller.prevAiming = aiming;

        if (Input.GetButtonDown(controller.xButton)) return new PlayerStateMele();
        if (Input.GetButtonDown(controller.bButton)) return new PlayerStateDash();
        return null;
    }

    /// <summary>
    /// this functioin contains most of the behavior that defines simple player movement
    /// </summary>
    void DoRun() {

        Vector2 direction = ForwardVector(true);

        float totalSpeed;

        if (ShouldApplyBoost()) {

            totalSpeed = SPEED + BOOST;

        } else {

            totalSpeed = SPEED;
        }

        // Debug.Log(pawn.transform.position.y);

        acceleration = Vector3.Normalize(new Vector3(direction.x, -GRAVITY * Time.deltaTime, direction.y)) * totalSpeed;

        DoMove();
        

    }

    /// <summary>
    /// This function moves the player using accelaeration, velocity and Move()
    /// </summary>
    void DoMove() {

        velocity += acceleration;

        pawn.Move(velocity * Time.deltaTime);

        velocity *= friction;

        // Debug.Log(velocity);
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

    void AimRetical(Vector2 aiming) {
        float angle = Mathf.Atan2(aiming.x, aiming.y);
        angle *= 180 / Mathf.PI;
        //Debug.Log(angle);
        controller.retical.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    /// <summary>
    /// this function changes the players facing baised on teh direction of the left joystick
    /// </summary>
    /// <param name="target">The left joystick</param>
    public void PlayerLook(Vector2 target) {

        float angle = Mathf.Atan2(target.x, target.y);
        angle *= 180 / Mathf.PI;
        pawn.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    bool HandleAim(Vector2 aiming, Vector2 direction) {

        if (Input.GetAxis(controller.leftTrigger) == 1) {

            HanldeShoot(aiming, controller.reticalSpawn.position);
            controller.prevAiming = aiming;

            if (!controller.retical.activeInHierarchy) {
                controller.retical.SetActive(true);
            }

            AimRetical(aiming);

            return true;

        } else if (AimVector(true) == Vector2.zero) {
            HanldeShoot(direction, controller.bulletSpawn.position);
            

        } else {
            HanldeShoot(aiming, controller.reticalSpawn.position);


        }

        if (controller.retical.activeInHierarchy) {
            controller.retical.SetActive(false);
        }

        Debug.Log(controller.reticalSpawn.position);
        return false;
    }


    void HanldeShoot(Vector2 aiming, Vector3 positionMod) {
        float trigger = Input.GetAxis(controller.rightTrigger);
        if (trigger == 1 && rightTriggerPrev != 1) {
            controller.AddBullet(aiming, positionMod);
        }
        rightTriggerPrev = trigger;
    }






    override public void OnEnter(PlayerController controller) {
        this.controller = controller;
        //SetInputs(controller.controllerNum);
        pawn = controller.GetComponent<CharacterController>();
    }



}
