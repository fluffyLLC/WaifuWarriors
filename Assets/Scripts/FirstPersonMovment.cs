using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovment : MonoBehaviour {

    public float speed = 5;
    public bool invertLookX = false;
    public bool invertLookY = true;

    public float lookSensitivity = 5;

    // float MouseSensitivityX;
    //float MouseSensitivityY;

    CharacterController pawn;
    Camera cam;


    // Use this for initialization
    void Start() {
        pawn = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        // MouseSensitivityX = LookSensitivity;
        //  MouseSensitivityY = LookSensitivity;
    }

    // Update is called once per frame
    void Update() {
        DoLook();
        DoMove();
    }

    private void DoMove() {
        Vector3 velocity = Vector3.zero;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        velocity += transform.forward * v * speed;
        velocity += transform.right * h * speed;
        
        pawn.SimpleMove(velocity);

        
           //print(cam.transform.localEulerAngles.y);

            /*
            velocity.z = v;
            velocity = transform.TransformDirection(velocity);
            */
    }

    private void DoLook() {
        float lookX = Input.GetAxis("Mouse X") * (invertLookX ? 1 : -1) * lookSensitivity;
        transform.Rotate(0, lookX, 0);


        float lookY = Input.GetAxis("Mouse Y") * (invertLookX ? 1 : -1) * lookSensitivity;
        float newAngle = cam.transform.localEulerAngles.x + lookY;
        if (newAngle > 80 && newAngle < 280) return;
        cam.transform.Rotate(lookY, 0, 0);

        //print(cam.transform.rotation.y);
    }
}
