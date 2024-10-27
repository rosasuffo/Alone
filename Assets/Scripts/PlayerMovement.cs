using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Subject
{
    ////////////////////////////////////////
    ///////////////VARIABLES////////////////
    ////////////////////////////////////////

    //MOVEMENT
    [SerializeField] private float P_speed = 0;
    [SerializeField] private float P_rotationSpeed = 0;
    [SerializeField] private float P_jumpForce = 0;
    private Vector3 vec_move;

    //CAMERA
    public Transform cameraTransform;

    //RIGIDBODY
    private Rigidbody P_rb;

    //RAYCAST
    public bool raycast;
    private RaycastHit hit;
    private Ray ray;

    ////////////////////////////////////////
    ////////////////METHODS/////////////////
    ////////////////////////////////////////

    void Start()
    {
        raycast = false;
        P_rb = GetComponent<Rigidbody>();
    }

    // cursor inside screen
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    ////////////////////////////////////////
    /////////////// MOVEMENT ///////////////
    ////////////////////////////////////////
    //player rotation in its own axis
    private void playerRotate()
    {
        /*
        // - to rotate player as the same time as the camera
        Quaternion toRotate = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, P_rotationSpeed * Time.deltaTime);

        // - to rotate player when moving
        /*if (vec_move != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(vec_move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, P_rotationSpeed * Time.deltaTime);
        }*/
    }
    private void playerWalk()
    {
        P_rb.velocity = vec_move.normalized * P_speed + new Vector3(0.0f, P_rb.velocity.y, 0.0f);
    }
    private void playerRun()
    {
        P_rb.velocity = vec_move.normalized * (P_speed * 2f) + new Vector3(0.0f, P_rb.velocity.y, 0.0f);
    }
    private void playerJump(int mult)
    {
        P_rb.velocity = new Vector3(P_rb.velocity.x * (Mathf.Abs(vec_move.x) * mult), P_jumpForce, P_rb.velocity.z * mult * (Mathf.Abs(vec_move.z) * mult));
    }

    ////////////////////////////////////////
    /////////////// RAYCAST ////////////////
    ////////////////////////////////////////    
    private void playerRaycast()
    {
        ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 50, Color.red);
        Physics.Raycast(ray, out hit, 50);

        if (hit.collider != null)
        {
            NotifyObservers(PlayerActions.CantInteract);
            switch (hit.transform.gameObject.tag)
            {
                case "npc1":
                    NotifyObservers(PlayerActions.Npc1Seen);
                    raycast = false;
                    return;
                case "npc2":
                    NotifyObservers(PlayerActions.Npc2Seen);
                    raycast = false;
                    return;
            }
        }
    }

    void Update()
    {
        // input 
        vec_move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        vec_move = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * vec_move;

        // update x,z velocity
        //playerRotate();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerWalk();
        }
        else
        {
            playerRun();
        }

        // update y velocity
        /*if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && P_rb.velocity.y == 0)
        {
            playerJump(2);
        }*/

        if (raycast) playerRaycast();


        //Debug
        // Debug.Log(Input.GetAxis("Mando Y"));
        // Debug.Log("P_rb.velocity: " + P_rb.velocity);
        // Debug.Log("vec_move: " + vec_move);
        // Debug.Log("P_rb.velocity.magnitude: " + P_rb.velocity.magnitude);
        // Debug.Log("vec_move.magnitude: " + vec_move.magnitude);
        // Debug.Log(jumpsLeft + " jumps left");
    }
}
