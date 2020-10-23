using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Need for new input system

public class PlayerTwo : CharacterTwo
{

    PlayerControls controls;    // Standard controls available to the player
    Vector2 move;               // This stores our movement from keyboard or left stick
    /*[SerializeField]*/
    protected float mouseSensitivity; //hopefully this can be replaced by something in the input manager
    /*[SerializeField]*/
    protected float clampAngle; //the max angle that the character can look up (negate to get the max angle the character can look down)
    Rigidbody myRigid;
    private Vector3 inputDirection;
    private Transform cameraMain;
    Vector3 moveWithCamera;
    float turnSmoothVelocity;

    /**
     * On awake we initialize our controls to tell it what to do with each 
     * 
     */
    private void Awake()
    {

        controls = new PlayerControls();    // Initialize our controls object

        // Move is controlled with our left stick or keyboard being a 2d plane for direction so save that vector2 as move
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;              //Not moving anymore so set that vector2 to 0


        controls.Gameplay.Jump.performed += ctx => Jump();      // In jump context call the jump function
        controls.Gameplay.Dash.performed += ctx => initiateDash();       //Similar for dashing
        controls.Gameplay.Fire.performed += ctx => shootWeapons();

    }

    /**
     * Enable Gameplay controls
     */
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    /**
     * Disable Gameplay controls
     */
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {

        cameraMain = Camera.main.transform;  //Get our main camera that is to be followed with the rotation
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
        dashSpeed[0] = 5;
        dashSpeed[1] = 20;
        dashSpeed[2] = 5;

        mouseSensitivity = 100;
        clampAngle = 60;

        base.Start(); //call the regular start function

    }

    /**
     * General movement override, called in fixed update each time from parent
     */
    protected override void handleMovement()
    {
        // Are we dashing atm?
        if (dashActionState == actionState.inactive) //if we're not in the middle of dashing
        {
            standardMovement(); //we can go ahead and move normally
        }
        else
        {
            // Still dashing so continue that telegraph
            dashingMovement();
        }
    }

    /**
     * Handles player movement in respect to the direction and camera
     * gets our current move location and transforms the players position each frame 
     */
    private void standardMovement()
    {
        // Essentially we are going in a current direction with respect to the camera view
        // Since input is a 2d vector, move.y is essentially what we want to use to move in the z axis
        // now aclculate our vector with respect to the camera
        /*moveWithCamera = (cameraMain.forward * move.y + cameraMain.right * move.x);

        moveWithCamera.y = 0f; // set y to there to be sure we dont move up or down
        */

        //Assign the camera vector3
        Vector3 forward = cameraMain.forward;
        forward.y = -0.1f;  // For some reason at 0 it messed with grounded so weird
        Vector3 right = cameraMain.right;
        right.y = -0.1f;

        // Had to do that as negative. 0 made the is grounded function act off

        movement = (forward * move.y + right * move.x);


        
        

        movement *= speed;  //Move with speed

    }

    /**
     * Overriding parent angle method
     * This essentially just rotate the player so they are facing their movement direction
     */
    protected override void handleAngle()
    {
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        // Get the new target angle to rotate to and convert to degrees

        //Is the player moving? That way if still they can rotate and view the player
        if (movement != Vector3.zero)
        {
            // Transform rotation
            // This could using a lerp method to make it smoother I just haven't implemented it yet
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }

    /**
     * Handle player jump.
     * When button is hit this function launches
     * Not overriding from parent as it already has an active listener
     */
    private void Jump()
    {
        
        if(characterController.isGrounded)
        {
            Debug.Log("jumping");
            // we can jump now
            //movement.y = 3f;
            //characterController.Move(movement);
            isJumping = true;


        }
    }

    /**
     * Player attempts to dash, initiate dash telegraph
     */
    private void initiateDash()
    {

        if (dashActionState == actionState.inactive)
        {
            movement.y = 0; // make sure no vertical movement
            dashVector = movement;//save the current movement vector so that we have it next time this function is called

            dashActionState++;
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the value of the first element in dash length (telegraph length)

            movement *= dashSpeed[(int)dashActionState - 1]; //scales the movement vector
        }
    }

    private void dashingMovement()
    {

        movement = dashVector; //set direction and speed to whatever it was in the previous function call
        movement.y = 0; // double check to make sure no vertical movement
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

    private void shootWeapons()
    {
        weaponAccess.useWeapon();
    }



}
