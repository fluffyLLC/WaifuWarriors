using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHit : PlayerState {

    float HitImpulse = 100;
    float friction = .9f;
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 hitDirection;
    int running;

    public override PlayerState Update() {
        // throw new NotImplementedException();
        running++;
        //Debug.Log("You where hit " + running );
        //Debug.Log(velocity);
        if (DoMove()) return null;
        return new PlayerStateNormal();
    }

    /// <summary>
    /// This function moves the player using accelaeration, velocity and simple move
    /// </summary>
    bool DoMove() {

        //velocity += acceleration;

        pawn.SimpleMove(velocity);

        velocity *= friction;

        if (Mathf.Abs(velocity.x) < 1 && Mathf.Abs(velocity.z) < 1 ) return false;

        return true;
    }

    public override void OnEnter(PlayerController controller) {
        base.OnEnter(controller);
    }

    public override void OnExit() {
       // throw new NotImplementedException();
    }

    public override void GetActorTransform(Transform actor) {
        hitDirection =  pawn.transform.position - actor.position;
        hitDirection.y = 1;
        velocity = Vector3.Normalize(hitDirection) * HitImpulse;
    }
}
