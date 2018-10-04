using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerState playerState;

    // Use this for initialization
    void Start() {
        SwitchPlayerState(new PlayerStateNormal());

    }

    // Update is called once per frame
    void Update() {

        PlayerState newState = playerState.Update();
        SwitchPlayerState(newState);
    }

    private void SwitchPlayerState(PlayerState newState) {
        if (playerState != null) {

            if (playerState != null) playerState.OnExit();

            playerState = newState;

            playerState.OnEnter(this);

        }
    }
}
