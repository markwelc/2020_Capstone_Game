using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Need for new input system
using UnityEngine.XR.WSA;

public class NewPlayer : Character
{

    PlayerControls controls;    // Standard controls available to the player
    Vector2 move;               // This stores our movement from keyboard or left stick
    /*[SerializeField]*/
    protected float mouseSensitivity; //hopefully this can be replaced by something in the input manager
    /*[SerializeField]*/
    //protected float clampAngle; //the max angle that the character can look up (negate to get the max angle the character can look down)
    //Rigidbody myRigid;
    //private Vector3 inputDirection;
    private Transform cameraMain;
    //Vector3 moveWithCamera;
    //float turnSmoothVelocity;

    [SerializeField] private Vector3 lookOffset; //this is subtracted from camera position and then the character always looks in the direction from this point to itself
    [SerializeField] private float smoother; //this will slow down the speed at which the player looks toward where the camera's looking (the lower the value, the slower the movement)
    [SerializeField] private bool useFreeRotation;

    /*[SerializeField]*/
    protected int dashing; //keeps track of where we are in the dash
    /*[SerializeField]*/
    protected int[] dashLength; //lists the number of frames that each of the three phases should be active for
    /*[SerializeField]*/
    protected float[] dashSpeed; //indicates the speed of the dash
    protected Vector3 dashVector;//the direction of our dash
    /*[SerializeField]*/
    protected actionState dashActionState;
    protected float[] useStates;


    [SerializeField] private LayerMask playerLayer;

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
        controls.Gameplay.Fire.performed += ctx => useWeapons();
        controls.Gameplay.ChangeViewMode.performed += ctx => changeViewMode();
        controls.Gameplay.CycleWeapon.performed += ctx => cycleWeapon();
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

        speed = 10;
        jumpForce = 300;

        dashVector = Vector3.zero;
        dashActionState = actionState.inactive;
        dashing = 0;

        dashLength = new int[4];
        dashLength[0] = 2; //length of telegraph
        dashLength[1] = 15; //length of action
        dashLength[2] = 2; //length of recovery
        dashLength[3] = 200; //length of dash cool down

        dashSpeed = new float[4];
        dashSpeed[0] = speed; //speed to move while in telegraph
        dashSpeed[1] = speed * 3; //speed to move while dashing
        dashSpeed[2] = speed; //speed to move while in recovery
        dashSpeed[3] = 0; //this should never be used

        mouseSensitivity = 100;
        //clampAngle = 60;

        base.Start(); //call the regular start function

        //equippedWeapon = "stick"; //this is given a default value that I want to override

        //trying to access the numbers in the states array.
        //useStates = new float[4];
        //useStates[0] = ?
    }

    /**
     * General movement override, called in fixed update each time from parent
     * NOTE: this function doesn't start doing a certain type of movement.
     *      This means that if, for instance, dashActionState is actionState.cooldown, we can
     *          set it to actionState.inactive
     *          call this function
     *          set it back to actionState.cooldown
     *      Thanks to this ability, we can call handleMovement from dashingMovement and know that handleMovement won't immediately turn around and call dashingMovement
     *      In order for this to work, handleMovement can only ever see what kind of movement is happening right now, and do that
     */
    protected override void handleMovement()
    {
        if(dashActionState != actionState.inactive)
        {
            dashingMovement();
        }
        //else if(sprintActionState != actionState.inactive)
        //{
        //    sprintingMovement();
        //}
        else //no special kind of movement
        {
            standardMovement();
        }
    }

    /**
     * Handles player movement in respect to the direction and camera
     * gets our current move location and transforms the players position each frame
     *
     * while this could override something from the Character class, it doesn't really need to because its purpose is to make handleMovement cleaner
     * which, as far as the Character class is concerned, isn't needed
     */
    private void standardMovement()
    {
        // Essentially we are going in a current direction with respect to the camera view
        // Since input is a 2d vector, move.y is essentially what we want to use to move in the z axis
        // now calculate our vector with respect to the camera
        movement = Vector3.ProjectOnPlane(cameraMain.forward, Vector3.up) * move.y + cameraMain.right * move.x;
            //if the camera is looking down, cameraMain.forward is looking down. We need to project it onto a horizontal plane, normalize the result, then use that instead
        movement.y = 0f; // set y to there to be sure we dont move up or down
        movement = Vector3.Normalize(movement); //be sure movement is a normal vector

        movement *= speed;  //Move with speed
        anim.SetFloat("speed", move.y, 1f, Time.deltaTime * 10f);
        anim.SetFloat("turn", move.x, 1f, Time.deltaTime * 10f);

    }

    /**
     * Overriding parent angle method
     * This essentially just rotate the player so they are facing their movement direction
     */
    protected override void handleAngle()
    {
        //if (!useFreeRotation || movement != Vector3.zero)
        //{
        //    characterRigidbody.constraints = RigidbodyConstraints.None; //unfreeze rotation
        //    characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationX; //we always want rotation around x to be frozen

        //    //we want to rotate the player to match the direction the camera is facing
        //    //at least for now
        //    Vector3 directionVector = characterTransform.position - (cameraMain.position - lookOffset); //get a vector pointing from just below the camera's position to the player's position
        //    var lookAt = Quaternion.LookRotation(directionVector, Vector3.up); //save that in a format that we can use to change characterTransform.rotation
        //    characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, lookAt, Time.deltaTime * smoother); //change characterTransform.rotation, but do it slowly and steadily
        //}
        //else
        //{
        //    characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ; //freeze rotation
        //    characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        //}


        float targetAngleY = cameraMain.transform.rotation.eulerAngles.y;
        float targetAngleX = cameraMain.transform.rotation.eulerAngles.x;

        if ((!useFreeRotation || movement != Vector3.zero) && !isDead)
        {
            // Removed these constraints for now because they were messing us up
            //characterRigidbody.constraints = RigidbodyConstraints.None; //unfreeze rotation
            //characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationX; //we always want rotation around x to be frozen
            //characterRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ; //rotation around the z axis should always be frozen

            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.Euler(0, targetAngleY, 0), 15 * Time.fixedDeltaTime); //rotate the whole character to look left and right

            //shoot a raycast from the camera straight outwards
            Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000f, ~playerLayer)) //the ray hit something, so we aren't looking at empty space
            {
                Vector3 dirVector = hit.point - gunsPrefab.transform.position; //figure out which direction we should aim in (difference of two vectors)
                gunsPrefab.transform.rotation = Quaternion.Slerp(gunsPrefab.transform.rotation, Quaternion.LookRotation(dirVector), 15 * Time.fixedDeltaTime);
                    //aim in that direction
            }
            else //the ray didn't hit anything, so we're looking at empty space
            {
                gunsPrefab.transform.rotation = Quaternion.Slerp(gunsPrefab.transform.rotation, Quaternion.LookRotation(cameraMain.transform.forward), 15 * Time.fixedDeltaTime);
                //just be parallel to the camera
            }
        }
        else
        {
            characterRigidbody.constraints = RigidbodyConstraints.FreezeRotation; //freeze rotation
        }

    }

    /**
     * Handle player jump.
     * When button is hit this function launches
     * Not overriding from parent as it already has an active listener
     */
    private void Jump()
    {
        if (jumpAllowed())
        {
            characterRigidbody.AddForce(Vector3.up * jumpForce);
            anim.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    /**
     * Player attempts to dash, initiate dash telegraph
     */
    private void initiateDash()
    {
        if (dashAllowed())
        {
            if (move.y > 0.01 || move.x > 0.01)
                anim.SetTrigger("isDashing");

            movement = Vector3.ProjectOnPlane(cameraMain.forward, Vector3.up) * move.y + cameraMain.right * move.x; //figure out which direction to dash in
            movement.y = 0; // make sure no vertical movement
            movement = Vector3.Normalize(movement); //be sure movement is a normal vector

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

            movement *= dashSpeed[(int)dashActionState - 1]; //scale movement (this is recovery speed)
        }
        else if (dashActionState == actionState.recovery && dashing <= 0) //if we are recovering and need to go to the cool down
        {
            dashActionState++; //move to the next state
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the appropriate value

            anim.SetTrigger("doneDashing");
            anim.SetBool("isDashing", false);

            dashActionState = actionState.inactive;
            handleMovement(); //at this point we just want to move normally
                              //this lets us move in the frame that we switch to using regular movement
            dashActionState = actionState.cooldown;
                //this swapping of the value of dashActionState is explained in the else
        }
        else if(dashActionState == actionState.cooldown && dashing <= 0)
        {
            dashActionState = actionState.inactive; //move to the inactive state
            dashing = 0; //set dashing just to be safe and clean

            handleMovement(); //call this again just so that we can move in this frame
        }
        else
        {
            dashing--;
            if (dashActionState == actionState.cooldown)
            {
                dashActionState = actionState.inactive;
                handleMovement(); //we're in cooldown, so just move normally
                dashActionState = actionState.cooldown;
                    //this swapping of dashActionState lets us call handleMovement without having to worry about it calling dashingMovement
                    //handleMovement doesn't care that we're lying about what dashActionState should be
            }
            else
                movement *= dashSpeed[(int)dashActionState - 1]; //scale movement
        }
    }



    private void changeViewMode()
    {
        useFreeRotation = !useFreeRotation;
    }


    protected bool dashAllowed()
    {
        bool dashingPermits = dashActionState == actionState.inactive;
        if (dashingPermits)
        {
            return true;
        }

        return false;
    }


    /**
     * we need to override this cause we care about the value of dashActionState
     */
    protected override bool jumpAllowed()
    {
        bool dashingPermits = ((dashActionState == actionState.inactive) || (dashActionState == actionState.cooldown));
        if (dashingPermits && jumpPossible)
        {
            return true;
        }

        return false;
    }
}
