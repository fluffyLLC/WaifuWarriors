using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDash : PlayerState {

    float dashTime = .5f;
    float dashTimer = 0;
    //float weponRotatoin = 0;
    //float targetRotation = -360;
    Vector3 dashTarget;
    float dashLength = 20;
    float targetDist;
    float currentDist;

    // dashDirection;

    public override PlayerState Update() {
        //throw new System.NotImplementedException();
        if (DoDash()) return null;
        return new PlayerStateNormal();
    }

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



    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);
        Vector2 dashDirection = ForwardVector();
        if (dashDirection == Vector2.zero) dashDirection = controller.prevFacing;
        dashTarget = Vector3.Normalize(new Vector3(dashDirection.x, 0, dashDirection.y));
        dashTarget *= dashLength;
        dashTarget += pawn.transform.position;
        controller.dashEffect.Play();
    }
    public override void OnExit() {
        // throw new System.NotImplementedException();
        //controller.dashEffect.Stop();
    }


}
