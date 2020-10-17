using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Need for new input system

public class NewPlayer : Character
{

    PlayerControls controls;
    Vector2 move;
    /*[SerializeField]*/
    protected float mouseSensitivity; //hopefully this can be replaced by something in the input manager
    /*[SerializeField]*/
    protected float clampAngle; //the max angle that the character can look up (negate to get the max angle the character can look down)
    private Quaternion rotation;

    private Vector3 inputDirection;
    private Transform cameraMain;
    
    float turnSmoothVelocity;
    private void Awake()
    {

        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Dash.performed += ctx => dashingMovement();

        //controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        cameraMain = Camera.main.transform;
        //define all variables here
        //this might be dumb, not sure
        healthMax = 10;
        speed = 10;
        jumpForce = 175;

        dashLength = new int[3];
        dashLength[0] = 2;
        dashLength[1] = 2;
        dashLength[2] = 2;

        dashSpeed = new float[3];
        dashSpeed[0] = 0.01f;
        dashSpeed[1] = 0.01f;
        dashSpeed[2] = 0.01f;

        mouseSensitivity = 100;
        clampAngle = 60;

        base.Start(); //call the regular start function

    }

    // Update is called once per frame
   /* void Update()
    {
        
         standardMovement();
        
        
        //RotateCamera();

    }*/

    protected override void handleMovement()
    {
        if (dashActionState == actionState.inactive) //if we're not in the middle of dashing
        {
            standardMovement(); //we can go ahead and move normally
        }
        else
        {
            dashingMovement();
        }
    }

    private void standardMovement()
    {
        //Vector3 m = new Vector3(move.x, 0, move.y);
        //transform.position += m * speed * Time.deltaTime;

        Vector3 moveWithCamera = (cameraMain.forward * move.y + cameraMain.right * move.x);
        //moveWithCamera.y = 0;
        // Now rotate the player
        moveWithCamera.y = 0f;
        float targetAngle = Mathf.Atan2(moveWithCamera.x, moveWithCamera.z) * Mathf.Rad2Deg;


        if (moveWithCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.position += moveWithCamera * speed * Time.deltaTime;

        }

    }

    private void Jump()
    {
        characterRigidbody.AddForce(transform.up * jumpForce);
    }
    
    private void dashingMovement()
    {
        Vector3 moveWithCamera = (cameraMain.forward * move.y + cameraMain.right * move.x);
        //moveWithCamera.y = 0;
        // Now rotate the player
        moveWithCamera.y = 0f;
        float targetAngle = Mathf.Atan2(moveWithCamera.x, moveWithCamera.z) * Mathf.Rad2Deg;


        if (moveWithCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.position += moveWithCamera * (speed * 10) * Time.deltaTime;

        }

        // Start dash
        if(dashActionState == actionState.inactive)
        {
            // Okay so we know we can start
            dashVector = moveWithCamera;
            dashActionState++;
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the value of the first element in dash length (telegraph length)

            movement *= dashSpeed[(int)dashActionState - 1]; //scales the movement vector

        }

        movement = dashVector; //set direction and speed to whatever it was in the previous function call

        if (dashActionState == actionState.telegraph && dashing <= 0) //if we're in the telegraph phase and need to switch
        {
            dashActionState++; //move to the next state
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the appropriate value

            movement *= dashSpeed[(int)dashActionState - 1]; //scale movement
        }
        else if (dashActionState == actionState.active && dashing <= 0) //if we're dashing and need to recover
        {
            dashActionState++; //move to the next state
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the appropriate value

            movement *= dashSpeed[(int)dashActionState - 1]; //scale movement
        }
        else if (dashActionState == actionState.recovery && dashing <= 0) //if we are done recovering
        {
            dashActionState = actionState.inactive; //move to the next state
            dashing = 0; //set dashing to the appropriate value
        }
        else
        {
            dashing--;
            movement *= dashSpeed[(int)dashActionState - 1]; //scale movement
        }

    }

}
