using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHit : PlayerState {

    /// <summary>
    /// The amount of force applyed when the player is hit
    /// </summary>
    float HitImpulse = 75;
    /// <summary>
    /// the amount of friction applied when the player is hit
    /// </summary>
    float friction = .9f;
    /// <summary>
    /// This const defines the force applied by gravity
    /// </summary>
    const float GRAVITY = 20;
    float accelerationY;
    //Vector3 acceleration;
    /// <summary>
    /// The velocity of the player
    /// </summary>
    Vector3 velocity;
    /// <summary>
    /// the direction that the player should move when hit
    /// </summary>
    Vector3 hitDirection;

    //int running;

    /// <summary>
    /// This feunction is called every frame
    /// </summary>
    /// <returns>The next play state to enter retuns null if we should remain in this state</returns>
    public override PlayerState Update() {
        // throw new NotImplementedException();
        //running++;
        //Debug.Log("You where hit " + running );
        //Debug.Log(velocity);
        if (DoMove()) return null;
        return new PlayerStateNormal();
    }

    /// <summary>
    /// This function moves the player using velocity and simple move
    /// </summary>
    /// <returns>returns true if we are still moving, returns false if we are moving slow enough to transition out of hit</returns>
    bool DoMove() {
        Debug.Log("hit");
        //velocity += acceleration;
        accelerationY += GRAVITY * Time.deltaTime;

        pawn.Move(velocity * Time.deltaTime);

        velocity.y -= accelerationY;///GRAVITY * Time.deltaTime;
        velocity *= friction;

        float p = pawn.transform.position.y - 1.5f;
        if (Mathf.Abs(p) < .1f) return false;

        return true;
    }

    /// <summary>
    /// gets the transform of whatever hit this player, sets the hitDirection and velocity baised on this
    /// </summary>
    /// <param name="actor"> Whatever hit this player </param>
    public override void GetActorTransform(Transform actor) {
        hitDirection =  pawn.transform.position - actor.position;
        velocity = Vector3.Normalize(hitDirection);
        velocity.y = .5f;
        velocity *= HitImpulse;
    }
}
