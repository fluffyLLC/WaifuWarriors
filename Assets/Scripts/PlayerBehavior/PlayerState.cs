using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

    protected PlayerController controller;

    abstract public PlayerState Update();
    virtual public void OnEnter(PlayerController controller) {
        this.controller = controller;
    }
    abstract public void OnExit();

}
