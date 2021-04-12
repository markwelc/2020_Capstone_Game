﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Need for new input system
using UnityEngine.XR.WSA;


public class NewPlayer : Character
{

    PlayerControls controls;    // Standard controls available to the player
    Vector2 move;               // This stores our movement from keyboard or left stick
    protected float mouseSensitivity; //hopefully this can be replaced by something in the input manager
    private Transform cameraMain;

    public bool dash;
    public Inventory inventory; //keeps track of the player's inventory
    public HUD hud;//references the HUD script for opening and closing message panels

    [SerializeField] private Vector3 lookOffset; //this is subtracted from camera position and then the character always looks in the direction from this point to itself
    [SerializeField] private float smoother; //this will slow down the speed at which the player looks toward where the camera's looking (the lower the value, the slower the movement)
    [SerializeField] private bool useFreeRotation; //determines whether moving the camera while the player's character isn't walking around should rotate the character

    [SerializeField] private LayerMask playerLayer;

    private GameObject reticle;
    reticleController retController;

    private bool deathMessageOpened;

    private bool canCheck;

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
        //controls.Gameplay.Fire.performed += ctx => useWeapons();
        controls.Gameplay.Tool.performed += ctx => initiateTool(1); //this is meant for your standard attack (the stick)
        controls.Gameplay.Tool2.performed += ctx => initiateTool(2); //this is meant for your secondary attack (the gun)
        controls.Gameplay.Tool3.performed += ctx => initiateTool(3); //this is meant for offensive items (grenades)
        controls.Gameplay.Tool4.performed += ctx => initiateTool(4); //this is meant for support items (estus stimpaks)
        controls.Gameplay.ChangeViewMode.performed += ctx => changeViewMode();
        controls.Gameplay.CycleWeapon.performed += ctx => cycleWeapon();

        controls.Gameplay.Pickup.performed += ctx => PickupMessage();
        controls.Gameplay.Block.performed += ctx => startBlock();
        controls.Gameplay.Block.canceled += ctx => endBlock();
        controls.Gameplay.Sprint.performed += ctx => Sprint();
        controls.Gameplay.Sprint.canceled += ctx => endSprint();
        controls.Gameplay.Crouch.performed += ctx => crouch();
        controls.Gameplay.Crouch.canceled += ctx => endCrouch();

        reticle = GameObject.Find("/Main Camera/Canvas/Reticle");
        retController = reticle.GetComponent<reticleController>();
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
        base.Start(); //call the regular start function

        cameraMain = Camera.main.transform;  //Get our main camera that is to be followed with the rotation
        //define all variables here
        //this might be dumb, not sure
        dash = false;
        // speed = 10;
        //make speed dependent on the playerHealthController's value
        speed = playerHealthManager.characterSpeed * 10f;
        sprintSpeed = playerHealthManager.characterSpeed * 15f;
        jumpForce = 6;

        // Tool Added stuff
        toolActionState = actionState.inactive;
        usingTool = 0;
        toolStates = new int[4];
        toolStates[0] = 0;  //length of telegraph
        toolStates[1] = 0;  //length of action
        toolStates[2] = 0;  //length of recovery
        toolStates[3] = 0;  //length of tool cooldown
        toolUsed = 0;
        // End tools

        dashVector = Vector3.zero;
        dashActionState = actionState.inactive;
        dashing = 0;

        dashLength = new int[4];
        dashLength[0] = 0; //length of telegraph
        dashLength[1] = 22; //length of action
        dashLength[2] = 0; //length of recovery
        dashLength[3] = 100; //length of dash cool down

        dashSpeed = new float[4];
        dashSpeed[0] = speed; //speed to move while in telegraph
        dashSpeed[1] = speed * 2; //speed to move while dashing
        dashSpeed[2] = speed; //speed to move while in recovery
        dashSpeed[3] = 0; //this should never be used

        mouseSensitivity = 100;

        equippedWeapon = 1; //this is the starting value
        deathMessageOpened = false;

        // initial stamina and active stamina
        // setting to 5 for now essentially means
        // doing an action that requires stamina for 5 seconds
        initStamina = 5f;
        stamina = initStamina;
    }

    void Update()
    {
        speed = playerHealthManager.characterSpeed * 10f;
        sprintSpeed = playerHealthManager.characterSpeed * 15f;

        if (isDead && !deathMessageOpened)
        {
            deathMessageOpened = true;
            hud.OpenDeathMessagePanel();
        }

        // Are they jump and can you check aka has courontine finished?
        if (isJumping && canCheck)
        {
            // Okay so we know they are jumping and the courintine is finished
            // check for ground so we can start jump animation
            if (GroundCheck())
            {
                // reset jump operations for next time and play animation
                isJumping = false;
                anim.SetTrigger("landJump");
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "landing");
                canCheck = false;
            }

        }
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

        if (dashActionState != actionState.inactive)
        {
            dashingMovement();
        }
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
        if (move != Vector2.zero)
            retController.setMoving(true);
        else
            retController.setMoving(false);
        // Essentially we are going in a current direction with respect to the camera view
        // Since input is a 2d vector, move.y is essentially what we want to use to move in the z axis
        // now calculate our vector with respect to the camera
        movement = Vector3.ProjectOnPlane(cameraMain.forward, Vector3.up) * move.y + cameraMain.right * move.x;
        //if the camera is looking down, cameraMain.forward is looking down. We need to project it onto a horizontal plane, normalize the result, then use that instead
        movement.y = 0f; // set y to there to be sure we dont move up or down
        movement = Vector3.Normalize(movement); //be sure movement is a normal vector

        // if they are sprinting increase movemeent speed to sprint speed
        if (isSprinting)
            movement *= sprintSpeed;  //Move with speed
        else if (isCrouching)
            movement *= speed / 2;
        else
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

        if (!dash)
        {
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
                    Vector3 dirVector = hit.point - gunsParent.transform.position; //figure out which direction we should aim in (difference of two vectors)
                    gunsParent.transform.rotation = Quaternion.Slerp(gunsParent.transform.rotation, Quaternion.LookRotation(dirVector), 15 * Time.fixedDeltaTime);
                    //aim in that direction
                }
                else //the ray didn't hit anything, so we're looking at empty space
                {
                    gunsParent.transform.rotation = Quaternion.Slerp(gunsParent.transform.rotation, Quaternion.LookRotation(cameraMain.transform.forward), 15 * Time.fixedDeltaTime);
                    //just be parallel to the camera
                }
            }
            else
            {
                characterRigidbody.constraints = RigidbodyConstraints.FreezeRotation; //freeze rotation
            }
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
            // reset the triggers just in case
            anim.ResetTrigger("isJumping");
            anim.ResetTrigger("landJump");

            anim.SetTrigger("isJumping");
            isJumping = true;

            StartCoroutine("Jumping");
            StartCoroutine("Landing");
        }
    }

    private IEnumerator Landing()
    {
        //try to delay for 2 seconds
        yield return new WaitForSeconds(1.0f);
        //anim.SetTrigger("midAir");
        // Just setting can check so we know we can check during our update func
        canCheck = true;
    }
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "launch");
        //this delays the addition of the velocity until the player has prepared to jump
        yield return new WaitForSeconds(0.2f);
        characterRigidbody.velocity = Vector3.up * jumpForce;
    }

    private bool GroundCheck()
    {
        // Just made it return since its either true or false
        // 1.75f is pretty much just arbitary, i thought it looked good though
        // only if ground layer
        return Physics.Raycast(this.transform.position, -Vector3.up, 1.75f, 1 << LayerMask.NameToLayer("staticEnvironment"));
    }

    /**
     * Player attempts to dash, initiate dash telegraph
     */
    private void initiateDash()
    {
        float myTargetAngle = 0;
        if (dashAllowed())
        {
            StartCoroutine("DashSound");
            //FindObjectOfType<AudioManager>().Play("dash");
            if (move.y != 0.00 || move.x != 0.00)
            {
                anim.SetTrigger("isDashing");
                dash = true;
                playerHealthManager.setInvincible(true);
                //current rotation
                //Debug.Log("transform is: " + characterTransform.rotation);
                //current inputs
                //Debug.Log("y: " + move.y + "x: " + move.x);
            }

            //rotate the player for the diagonal forward dashes (if there is appropriate input)
            if (move.y > 0.1 && move.x < -0.1 || move.y < -0.1 && move.x > 0.1)
            {
                //the player is moving diagonal, so rotate the player for the dash (left diagonal)
                //characterTransform.rotation = Quaternion.Euler(0, -45, 0);
                myTargetAngle = characterTransform.rotation.eulerAngles.y - 70;
                if (myTargetAngle < 0)
                {
                    myTargetAngle = myTargetAngle + 360;
                }
                //Debug.Log("Angle: " + myTargetAngle);
                characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.Euler(0, myTargetAngle, 0), 15 * Time.fixedDeltaTime);
                //Debug.Log("transform is: " + characterTransform.rotation);
                //Debug.Log("y: " + move.y + "x: " + move.x);
            }
            else if (move.y > 0.1 && move.x > 0.1 || move.y < -0.1 && move.x < -0.1)
            {
                //the player is moving diagonal, so rotate the player for the dash (right diagonal)
                myTargetAngle = characterTransform.rotation.eulerAngles.y + 70;
                myTargetAngle = myTargetAngle % 360;
                //Debug.Log("Angle: " + myTargetAngle);
                characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.Euler(0, myTargetAngle, 0), 15 * Time.fixedDeltaTime);
                //Debug.Log("transform is: " + characterTransform.rotation);
                //Debug.Log("y: " + move.y + "x: " + move.x);
            }


            movement = Vector3.ProjectOnPlane(cameraMain.forward, Vector3.up) * move.y + cameraMain.right * move.x; //figure out which direction to dash in
            movement.y = 0; // make sure no vertical movement
            movement = Vector3.Normalize(movement); //be sure movement is a normal vector

            dashVector = movement;//save the current movement vector so that we have it next time this function is called

            dashActionState++;
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the value of the first element in dash length (telegraph length)

            movement *= dashSpeed[(int)dashActionState - 1]; //scales the movement vector

            if (getCurrentWeaponType() == 'g' && this.gameObject.layer == 8) //if we're holding a gun, turn off animations
                toggleAnimRigging(false, true);
        }
    }

    private IEnumerator DashSound()
    {
        //try to delay for 2 seconds
        yield return new WaitForSeconds(0.0f);
        FindObjectOfType<AudioManager>().Play(GameObject.Find("Player"), "dash");
    }

    /**
     * move through all the dash action states, toggling certain animations and changing speeds accordingly
     */
    private void dashingMovement()
    {
        movement = dashVector; //set direction and speed to whatever it was in the previous function call
        movement.y = 0; // double check to make sure no vertical movement
        if (dashActionState == actionState.telegraph && dashing <= 0) //if we're in the telegraph phase and need to switch
        {
            dashActionState++; //move to the next state
            dashing = dashLength[(int)dashActionState - 1]; //set dashing to the appropriate value

            movement *= dashSpeed[(int)dashActionState - 1]; //scale movement
            anim.SetTrigger("doneDashing");
            anim.SetBool("isDashing", false);

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

            //anim.SetTrigger("doneDashing");
            //anim.SetBool("isDashing", false);
            playerHealthManager.setInvincible(false);
            dashActionState = actionState.inactive;
            dash = false;
            handleMovement(); //at this point we just want to move normally
                              //this lets us move in the frame that we switch to using regular movement
            dashActionState = actionState.cooldown;
            //this swapping of the value of dashActionState is explained in the else

            if (getCurrentWeaponType() == 'g' && this.gameObject.layer == 8)
                toggleAnimRigging(true, true);
        }
        else if (dashActionState == actionState.cooldown && dashing <= 0)
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

    /**
     * I'm honestly not sure what's going on with this function.
     * It goes against what the handleX functions usually do and seems to try to just start moving through the actions states if we're already using a tool
     */
    protected override void handleWeapons()
    {
        if (toolActionState != actionState.inactive)
        {
            toolUse();
        }
    }

    /**
     * There are two camera modes.
     * In the first mode, while standing still, looking around will not change which way the player's avatar is facing, allowing the camera to look into the character's face.
     * In the other, while standing still, looking around will still cause the player's avatar to rotate.
     * This function switches between the two
     */
    private void changeViewMode()
    {
        useFreeRotation = !useFreeRotation;
    }

    private IInventoryItem mItemToPickup = null; //reference to inventory item that player is colliding with

    //called when player connects with item's box collider
    private void OnTriggerEnter(Collider other)
    {
        //reference to item within box collider
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        //if the item is not null
        if (item != null)
        {
            
            mItemToPickup = item; //get reference to that item
            hud.OpenPickupMessagePanel(""); //open message panel to pick up item
        }
    }

    //called when player is no longer connecting to an item's box collider
    private void OnTriggerExit(Collider other)
    {
        //reference to item within box collider
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null) //if item is not null
        {
            hud.ClosePickupMessagePanel(); //close the message panel
            mItemToPickup = null; //set item reference to null
        }
    }

    //called when an item is picked up
    void PickupMessage()
    {
        if (mItemToPickup != null)
        {
            inventory.AddItem(mItemToPickup); //add item to player's inventory
            mItemToPickup.OnPickup(); //call item's OnPickup() method
            hud.ClosePickupMessagePanel(); //close the message panel
        }
    }


    /**
     * Initiate block
     */
    void startBlock()
    {
        // No need to reinvent the wheel
        // blocking has same restrictions as jump
        // player mu be grounded and not in dash state
        if (jumpAllowed())
        {
          //  Debug.Log("Blocking");
            anim.SetTrigger("isBlocking");

            // Trigger wern't restting for some reason before reset now
            anim.ResetTrigger("doneBlocking");
            anim.ResetTrigger("breakBlock");
            // They are invincible at start
            isBlocking = true;
            playerHealthManager.setInvincible(true);
        }
    }

    /**
     * They stopped blocking but was never broken
     */
    void endBlock()
    {
        //Debug.Log("End Block");
        // They released so end the block no longer invincible
        anim.SetTrigger("doneBlocking");
        isBlocking = false;
        playerHealthManager.setInvincible(false);
    }

    /**
     * If the block has been broken
     */
    protected override void breakBlock()
    {
        if (isBlocking)
        {
            // only blocks one time reset back
            playerHealthManager.setOneTimeBlock(false);
            //   Debug.Log("Got Hit break block");
            anim.SetTrigger("breakBlock"); // break block animation then transition back to standard

            // no longer invincible or blocking
            isBlocking = false;
            playerHealthManager.setInvincible(false);
        }
    }

    public void menuEnabled(bool siPapi)
    {
        if (siPapi)
            controls.Gameplay.Disable();
        else
            controls.Gameplay.Enable();
       
    }

    void Sprint()
    {
        // enable sprint
        // issprinting goes to blend tree which is updated with movement
        if (sprintAllowed(move))
        {

            isSprinting = true;
            anim.SetTrigger("isSprinting");
            anim.ResetTrigger("endSprint");
        }
        
    }

    protected override bool continueSprintAllowed()
    {
        // return to character to ensure conditions are still met
        return sprintAllowed(move);
    }

    protected override void endSprint()
    {
        if (isSprinting)
        {
            anim.SetTrigger("endSprint");
            isSprinting = false;
        }
    }

    protected override void crouch()
    {
        base.crouch();
    }

    protected override void endCrouch()
    {
        base.endCrouch();
    }
}
