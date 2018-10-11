using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMele : PlayerState {

    float animTime = .15f;
    float animTimer = 0;
    float weponRotatoin = 0;
    float targetRotation = -180;
    float punchLength = 3;
    Vector3 targetPosit;
    //float currentDist;
    Vector3 rotatation = Vector3.zero;


    public override PlayerState Update() {
        if (!DoMele()) return new PlayerStateNormal();
        return null;
    }



    bool DoMele() {
        if (animTimer < animTime) {
            animTimer += Time.deltaTime;
            float percent = animTimer / animTime;
            rotatation.y = Mathf.Lerp(weponRotatoin, targetRotation, percent);
            controller.sword.transform.localEulerAngles = rotatation;
            pawn.transform.position = Vector3.Lerp(pawn.transform.position, targetPosit, percent);
            return true;
        }
        return false;
    }

    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);
        controller.sword.SetActive(true); // = true;
        controller.bladeEffect.Play();
        Vector2 punchDirection = ForwardVector();
        if (punchDirection == Vector2.zero) punchDirection = controller.prevFacing;
        targetPosit = Vector3.Normalize(new Vector3(punchDirection.x, 0, punchDirection.y));
        targetPosit *= punchLength;
        targetPosit += pawn.transform.position;
        //animTimer
    }



    public override void OnExit() {

       // controller.blade.enabled = false;
        controller.sword.SetActive(false); //localEulerAngles = Vector3.zero;
        controller.sword.transform.localEulerAngles = Vector3.zero;

        //throw new NotImplementedException();
    }



}
