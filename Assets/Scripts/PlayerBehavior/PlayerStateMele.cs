using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMele : PlayerState {

    /// <summary>
    /// the duration of the mele atack in seconds
    /// </summary>
    float animTime = .15f;
    /// <summary>
    /// how long we have been meleing for
    /// </summary>
    float animTimer = 0;
    /// <summary>
    /// the current rotation of the mele object
    /// </summary>
    float weponRotatoin = 0;
    /// <summary>
    /// the target rotation of the mele object
    /// </summary>
    float targetRotation = -180;
    /// <summary>
    /// the distance we should travel while punching
    /// </summary>
    float punchLength = 1;
    /// <summary>
    /// the target position for the punch
    /// </summary>
    Vector3 targetPosit;
    //float currentDist;

    /// <summary>
    /// the vector used to set the rotation
    /// </summary>
    Vector3 rotatation = Vector3.zero;

    int running;

    /// <summary>
    /// this function id caled every frame, and runs most of the behaviors contained within this class
    /// </summary>
    /// <returns>The next scene to transition to. Returns null if no transition should take place.</returns>
    public override PlayerState Update() {
        if (DoMele()) return null;
        return new PlayerStateNormal();

    }


    /// <summary>
    /// this fcnction contains most of the logic that defines a mele action 
    /// </summary>
    /// <returns>returns true if we should continue in the mele state, retuns false if we should transition to the next state</returns>
    bool DoMele() {

        //if (Time.timeScale < 1) { Time.timeScale += 0.01f; } else if (Time.timeScale != 1) { Time.timeScale = 1; }

        if (animTimer < animTime) {
            animTimer += Time.deltaTime;
            running++;
            Debug.Log("Frame: " + running + ", animTimer: " + animTimer + ", animTime: " + animTime);
            float percent = animTimer / animTime;

            rotatation.y = Mathf.Lerp(weponRotatoin, targetRotation, percent);
            controller.sword.transform.localEulerAngles = rotatation;
            pawn.transform.position = Vector3.Lerp(pawn.transform.position, targetPosit, percent);
            return true;
        }

        return false;
    }

    /// <summary>
    /// This function is called when this class is instantiated and sets up all of the information nessisary for the mele to opoerate 
    /// </summary>
    /// <param name="controller">The controller that instantiates this class</param>
    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);
        //Time.timeScale = 0;

        controller.sword.SetActive(true); // = true;

        controller.meleColisionVolume.SetActive(true);

        controller.bladeEffect.Play();

        Vector2 punchDirection = ForwardVector();
        if (punchDirection == Vector2.zero) punchDirection = controller.prevFacing;
        targetPosit = Vector3.Normalize(new Vector3(punchDirection.x, 0, punchDirection.y));
        targetPosit *= punchLength;
        targetPosit += pawn.transform.position;
        //animTimer
    }


    /// <summary>
    /// this function is calles before this clas is removed, it resets all assets associated with the mele action 
    /// </summary>
    public override void OnExit() {

        // controller.blade.enabled = false;
        controller.sword.SetActive(false); //localEulerAngles = Vector3.zero;
        controller.meleColisionVolume.SetActive(false);
        controller.sword.transform.localEulerAngles = Vector3.zero;

        //throw new NotImplementedException();
    }



}
