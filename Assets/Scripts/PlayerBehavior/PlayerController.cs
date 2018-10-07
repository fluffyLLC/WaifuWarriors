using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// the number of the controller curently giving input to this pawn
    /// </summary>
    public int controllerNum = 1;

    public Transform sword;
    public MeshRenderer blade;
    public ParticleSystem boostEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem bladeEffect;
    public Vector2 prevFacing;



    private PlayerState playerState;

    // Use this for initialization
    void Start() {

        SwitchPlayerState(new PlayerStateNormal());
    }

    // Update is called once per frame
    void Update() {
        //print(playerState);
        if (playerState != null) {
            
            PlayerState newState = playerState.Update();
            SwitchPlayerState(newState);
        }
    }


    private void SwitchPlayerState(PlayerState newState) {

        if (newState != null) {

            if (playerState != null) playerState.OnExit();
            playerState = newState;
            playerState.OnEnter(this);
        }
    }

   
}
