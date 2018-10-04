using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMele : PlayerState {

    float animTime = 1;
    float animTimer = 0;
    float weponRotatoin = 0;
    float targetRotation = 360;
    float punchLength = 20;
    float targetDist;
    float currentDist;


    public override PlayerState Update() {
        if (!DoMele()) return new PlayerStateNormal();
        return null;
    }



    bool DoMele() {
        if (animTimer < animTime) {
            animTimer += Time.deltaTime;
            //Do Mele
            return true;
        }
        return false;
    }

    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);

    }



    public override void OnExit() {
        //throw new NotImplementedException();
    }


}
