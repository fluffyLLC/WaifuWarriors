using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNormal : PlayerState {



    override public PlayerState Update() {
        //put dat behavior in ther


        //transitions as well

        // if (transtion) return new state

        return null;
    }

    override public void OnEnter(PlayerController controller) {
        this.controller = controller;
    }

    override public void OnExit() { }

}
