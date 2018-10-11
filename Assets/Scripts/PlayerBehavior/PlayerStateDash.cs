using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDash : PlayerState {

    /// <summary>
    /// the duration of the dash in seconds
    /// </summary>
    float dashTime = .5f;
    /// <summary>
    /// how long we have been dashing for
    /// </summary>
    float dashTimer = 0;
    /// <summary>
    /// the point we want to dash to
    /// </summary>
    Vector3 dashTarget;
    /// <summary>
    /// the distance we want to dash
    /// </summary>
    float dashLength = 20;


    // dashDirection;

    /// <summary>
    /// this function id caled every frame, and runs most of the behaviors contained within this class
    /// </summary>
    /// <returns>The next scene to transition to. Returns null if no transition should take place.</returns>
    public override PlayerState Update() {
        //throw new System.NotImplementedException();
        if (DoDash()) return null;
        return new PlayerStateNormal();
    }

    /// <summary>
    /// this function contains the logic that defines the dash action
    /// </summary>
    /// <returns>returns true if we should continue dashing, returns false if we should transition to the nest state</returns>
    bool DoDash() {
        if (dashTimer < dashTime) {
            dashTimer += Time.deltaTime;
            float percent = dashTimer / dashTime;
            pawn.transform.position = Vector3.Lerp(pawn.transform.position, dashTarget, percent);
            //controller.sword.localEulerAngles = new Vector3(0, Mathf.Lerp(weponRotatoin, targetRotation, percent), 0);
            //controller.transform =
            return true;
        }
       // pawn.transform.position = dashTarget;
        return false;
    }


    /// <summary>
    /// This function is called when this class is instantiated and sets up all of the information nessisary for the dash to opoerate 
    /// </summary>
    /// <param name="controller">The controller that instantiates this class</param>
    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);
        Vector2 dashDirection = ForwardVector();
        if (dashDirection == Vector2.zero) dashDirection = controller.prevFacing;
        dashTarget = Vector3.Normalize(new Vector3(dashDirection.x, 0, dashDirection.y));
        dashTarget *= dashLength;
        dashTarget += pawn.transform.position;
        controller.dashEffect.Play();
    }

    


}
