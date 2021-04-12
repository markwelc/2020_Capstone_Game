using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //only use this class for things that are universal for all characters (the player, bosses, npcs, rats)
    //anything that's related to decision making should be in subclasses
    //for instance, bosses decide when/where to move very differently from the player, and so deciding when/where to move is in a subclass
    // public LayerMask whatIsGround, whatIsPlayer;
    //protected Vector2 move;
    protected Rigidbody characterRigidbody; //the character's rigidbody
    protected Transform characterTransform; //the character's transform
    protected Collider characterCollider; //the character's collider
    protected Animator anim;
    protected bool isJumping = false;
    public PlayerHealthController playerHealthManager;
    protected float health;
    protected float speed;//the default speed of the character
    protected float sprintSpeed;
    public bool isDead;  // To check if dead so player cant continue to move
    protected bool isBlocking;
    protected bool isSprinting;
    protected bool isCrouching;
    //public float staminaMax; //the max amount of stamina the character can have
    //protected float staminaCur; //the current amount of stamina the

    protected enum actionState
    {
        inactive,  //this means that the action is not happening
        telegraph, //this means that the action hasn't started yet, but that will start in a moment and any advanced warning that the action will happen is shown
        active, //this means that the action is happening
        recovery, //this means that the action has finished but the immediate results of the action (e.g. the animations to play after the action) haven't finished
        cooldown //this means that the action and all immediate effects have finished, so it looks like the inactive state, but the action may not be activated again
    }

    protected Vector3 movement;//used to hold the direction that the character should move in
    protected bool diagonal;//whether the player is moving diagonally

    /*[SerializeField]*/
    protected float jumpForce; //this is how strongly the character can jump
    protected bool jumpPossible; //determines if the character can currently jump
    protected actionState jumpActionState; //this may not be needed to restrict jumping, but may be useful in graphics

    [SerializeField] protected WeaponController weaponAccess;
    [SerializeField] protected GameObject gunsParent; //this will be the parent object of all guns while they are using animation rigging
    [SerializeField] protected GameObject gunsParentHand; //this will be the parent of all guns while they are NOT using animation rigging
    [SerializeField] protected GameObject meleeParent;

    protected actionState toolActionState;
    protected int[] toolStates; //length of each phase
    protected int usingTool; //keep track of where we are in an element of toolStates
    protected int toolUsed; //keeps track of which tool is being used (0 means no tool)

    protected actionState dashActionState;
    protected int[] dashLength; //lists the number of frames that each phase should be active for
    protected int dashing; //keeps track of where we are in the dash
    protected float[] dashSpeed; //indicates the speed of the dash
    protected Vector3 dashVector;//the direction of our dash

    [SerializeField] protected string[] availableWeapons;
    protected int equippedWeapon; //which weapon is currently equipped
    [SerializeField] private UnityEngine.Animations.Rigging.Rig rig;
    private IEnumerator coroutine;
    [SerializeField] public Collider[] weaponColliders;
    protected float stamina;
    protected float initStamina;
    //public float characterDamageModifier; // get the current damage modifier from the health manager

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        characterRigidbody = this.GetComponent<Rigidbody>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        characterCollider = this.GetComponent<Collider>(); //get collider

        jumpActionState = actionState.inactive;
        //toolActionState = actionState.inactive;

        // Get the characters health
        // Applies this script to each object using the character class
        // We can then generate a health bar based on this object by giving the name of the desired object
        playerHealthManager = gameObject.AddComponent<PlayerHealthController>();
        health = playerHealthManager.getHealth();
        isDead = false;

        isBlocking = false;
        //characterDamageModifier = playerHealthManager.characterDamageModifier;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // This is just to test to see communicationn between scripts
        // Below is testing to make sure it works, you can check it out if you want
        /*
        playerHealthManager.TakeDamage("playerRightArm", 1f);
        var unusableLimbs = playerHealthManager.getUnusableLimb();
        for(int i = 0; i < unusableLimbs.Count; i++)
        {
            print(unusableLimbs[i]);
        }
        */
        health = playerHealthManager.getHealth();

        checkIsDead();
        if(playerHealthManager.getOneTimeBlock())
        {
            breakBlock();
        }


        handleMovement(); //decide when and how to move
        handleJump(); //decide when to jump
        handleAngle(); //decide where to face
        handleWeapons(); //decide when to use weapons

        moveCharacter(movement);
        /*
        if (isJumping)
        {
            if (characterRigidbody.velocity.y < 0f)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("doneJumping", true);
                isJumping = false;
            }
        }*/

        /** You can use this to check unusable limbs UI
        if (playerHealthManager.rArmUsability == true)
            playerHealthManager.TakeDamage("playerRightArm", 1);
        else if (playerHealthManager.lLegUsability == true)
            playerHealthManager.TakeDamage("playerLeftLeg", 1);
        */
        // New functions to handle sprint and stamina
        // made in character in case we want to apply to enemy later on
        handleSprint();
        handleStamina();

    }


    /*
     * This functions takes a vector indicating in which direction the character should move and how far he should move and moves him that direction and distance.
     * Vertical components of the input vector are replaced with the vertical component of the character's velocity.
     * This makes handling moving while jumping pretty easy, but it means that this function is only useful for controlling horizontal movement.
     */
    protected void moveCharacter(Vector3 direcAndDist) //the input needs to contain both the direction and the distance
    {

        //the raycasting is useful for fast moving objects that the colliders can't deal with
        if (isDead || isBlocking)
            direcAndDist = Vector3.zero;

        direcAndDist.y = characterRigidbody.velocity.y; //Honestly seems like this line shouldn't be here...

        Ray ray = new Ray(characterTransform.position, direcAndDist); //shoot a ray from current position in direction of travel
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, (direcAndDist * Time.deltaTime).magnitude)) //if the ray didn't hit anything within the range that we're moving
            characterRigidbody.MovePosition((Vector3)transform.position + direcAndDist * Time.deltaTime); //go ahead and move

        else
        {
            characterRigidbody.MovePosition(hit.point);//otherwise, move to where the thing we hit was
        }
    }


    //One of the four functions for defining default behavior. They are meant to be overridden.
    protected virtual void handleMovement()
    {
        movement = new Vector3(0, 0, 0); //don't try to move anywhere
    }

    //One of the four functions for defining default behavior. They are meant to be overridden.
    protected virtual void handleJump()
    {
        //do nothing
        //ordinarily, this is the function that decides when to jumpt
        //in the player character's case, this isn't really needed since you can just run the jump function when the hotkey is pressed
    }

    //One of the four functions for defining default behavior. They are meant to be overridden.
    protected virtual void handleAngle()
    {
        characterTransform.eulerAngles = new Vector3(0, 0, 0); //set no particular angle
    }

    //One of the four functions for defining default behavior. They are meant to be overridden.
    protected virtual void handleWeapons() //decides when to use weapons
    {
        //do nothing
    }

    /*
     * This function turns on crouching.
     */
    protected virtual void crouch()
    {
        if(!isCrouching && crouchAllowed()) //if we're aren't already crouching and if we're allowed to crouch
        {
            isCrouching = true;
            anim.SetTrigger("isCrouching");
            // reset trigger helps just like clear the cache basically
            anim.ResetTrigger("endCrouch");
        }
    }

    /*
     * This functions turns off crouching
     */
    protected virtual void endCrouch()
    {
        if(isCrouching)
        {
            isCrouching = false;
            anim.SetTrigger("endCrouch");
            anim.ResetTrigger("isCrouching");

        }
    }

    /**
     * Handles continue checks if the character is sprinting
     * whether he still has the stamina and still meets sprint conditions
     */
    protected void handleSprint()
    {
        // if they are sprinting
        if (isSprinting)
        {
            // reduce stamina
            stamina -= 1f * Time.deltaTime;

            // if they are out of stamina or dont fufill sprint conditions
            if (stamina <= 0f || !continueSprintAllowed())
            {
                // end the sprint action
                endSprint();
            }
        }
    }

    /**
     * Stamina regen.
     * regen stamina if they arent doin an action requiring stamina
     * right now the only action that does is sprint
     * but if more actions were made that used stamina you cann just add them
     * here or make a bool func to check if using stamina required action
     */
    protected void handleStamina()
    {

        if (!isSprinting && stamina < initStamina)
        {
            stamina += 1f * Time.deltaTime;

            // Since working with time we could slightly go over
            if (stamina > initStamina)
                stamina = initStamina;
        }
    }

    /*
     * This function initiates the specified attack.
     * This includes both starting the animations, initializing action states, and makes the weapon able to deal damage.
     * 
     * attackType should be either 1 or 2
     * 1 if using the currently equipped weapon's primary attack
     * 2 if using the currently equipped weapon's secondary attack
     *
     * CharacterDamageModifier keeps track of the current damage scale of the player
     * when the character's arm is debuffed, they deal 15% less damage (per arm)
     */
    protected virtual void useWeapons(int attackType, float characterDamageModifier) //actually goes and uses the weapon
    {

        string animation;
        int[] states;
        weaponAccess.useWeapon(availableWeapons[equippedWeapon], out animation, out states, attackType, characterDamageModifier);
        weaponAccess.canDealDamage(availableWeapons[equippedWeapon], true);

        if (animation != null)
        {
            anim.SetTrigger(animation); //start the animation
        }
        toolStates = states;


        // Get how long the clip is so it only does ddamage while attacking
        float time = -1;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
            if (ac.animationClips[i].name == animation)        //If it has the same name as your clip
            {
                time = ac.animationClips[i].length;
            }
        }
        // make sure we actually got something
        if(time != -1)
        {
            // start courintine when done can no longer deal damage
            StartCoroutine(WaitForAttackComplete(time));
        }


    }

    private IEnumerator WaitForAttackComplete(float waitTime)
    {
        // waittime is the time of that animation
        yield return new WaitForSeconds(waitTime);
        // once done stop attack
        weaponAccess.canDealDamage(availableWeapons[equippedWeapon], false);

    }

    //start using a tool
    //this is in Character.cs because it is similar for every character
    protected void initiateTool(int toolNum)
    {
        if (toolAllowed())
        {
            //since we are initiating use of a tool, we are now moving to the active state
            toolActionState++;

            usingTool = toolStates[(int)toolActionState - 1]; //set usingTool to the value of the first element in toolStates (telegraph length)

            toolUsed = toolNum; //set the tool that we're using depending on how this function was called
        }
    }

    //move through all the tool action states
    //this is in Character.cs because it is similar for every character
    protected void toolUse()
    {
        if (toolActionState == actionState.telegraph && usingTool <= 0) //if we're in the telegraph phase and need to switch
        {
            //telegraph
            toolActionState++; //move to the next state
            usingTool = toolStates[(int)toolActionState - 1]; //set usingTool to the appropriate value

            useWeapons(toolUsed, playerHealthManager.characterDamageModifier);
            char toolType = getCurrentWeaponType();
            if (toolType == 'g' && toolUsed != 1 && this.gameObject.layer == 8)
            {
                toggleAnimRigging(false, true);
                Debug.Log("setting rig.weight to zero cause we've got to wait " + (toolStates[1] + toolStates[2]) + " units of time before we can use it again");
            }
            toolUsed = 0; //we're done with this, set it up for next time.
        }
        else if (toolActionState == actionState.active && usingTool <= 0) //if we're using a tool and need to recover
        {
            //action
            toolActionState++; //move to the next state
            usingTool = toolStates[(int)toolActionState - 1]; //set dashing to the appropriate value
        }
        else if (toolActionState == actionState.recovery && usingTool <= 0) //if we are recovering and need to go to the cool down
        {
            //recovery
            toolActionState++; //move to the next state
            usingTool = toolStates[(int)toolActionState - 1]; //set using tool to the appropriate value

            char toolType = getCurrentWeaponType();
            if (toolType == 'g' && this.gameObject.layer == 8)
            {
                //Debug.Log("turning on anim rigging");
                toggleAnimRigging(true, true);
            }
        }
        else if (toolActionState == actionState.cooldown && usingTool <= 0)
        {
            //cooldown
            toolActionState = actionState.inactive; //move to the inactive state
            usingTool = 0; //set usingTool just to be safe and clean

        }
        else
        {
            usingTool--;
        }
    }

    //changes the currently equipped weapon to the next one in available weapons array
    protected void cycleWeapon()
    {
        //Debug.Log("hello");
        //disable current weapon's gameobject
        getCurrentWeapon().gameObject.SetActive(false);

        equippedWeapon++;
        equippedWeapon = equippedWeapon % availableWeapons.Length;

        getCurrentWeapon().gameObject.SetActive(true);

        //enable the new weapon
        char toolType = getCurrentWeaponType();
        if (toolType == 'g' && this.gameObject.layer == 8)
        {
            Debug.Log("turning on anim rigging");
            toggleAnimRigging(true, false);
        }
        else
        {
            Debug.Log("turning off anim rigging");
            toggleAnimRigging(false, false);
        }
    }

    // these three functions determine whether the character may jump
    // The first two set the jumpPossible variable while the last one checks stuff like whether we're dashing
    // Changed to oncollisionstay
    // Oncollision enter wasn't always accurate and caused issues when on slant
    protected virtual void OnCollisionStay(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            jumpPossible = true;
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            jumpPossible = false;
    }

    //are we allowed to jump?
    protected bool jumpAllowed()
    {
        bool dashingPermits = ((dashActionState == actionState.inactive) || (dashActionState == actionState.cooldown));

        return dashingPermits && jumpPossible && !isBlocking;
    }

    //are we allowed to dash?
    protected bool dashAllowed()
    {
        bool dashingPermits = dashActionState == actionState.inactive;

        return dashingPermits && !isBlocking;
    }

    //are we allowed to use a tool?
    protected bool toolAllowed()
    {
        bool toolPermits = toolActionState == actionState.inactive;

        return toolPermits;
    }

    //are we allowed to use a tool?
    //note that the direction we're trying to sprint in matters (you can't sprint backwards and you have to be trying to move to sprint)
    protected bool sprintAllowed(Vector2 move)
    {
        bool dashingPermits = ((dashActionState == actionState.inactive) || (dashActionState == actionState.cooldown));

        // Checking isjumping because isgrounded causes issues in arena since multiple tiles there could be a single frame
        // which would cause sprintallowed to end since checked every frame while sprinting

        return dashingPermits && !isJumping && !isBlocking && move.y > 0 && (move.x < 0.1 || move.x > 0.1);
    }

    protected virtual bool continueSprintAllowed()
    {
        // Do nothing here
        return false;
    }

    //are we allowed to crouch
    protected bool crouchAllowed()
    {
        bool dashingPermits = ((dashActionState == actionState.inactive) || (dashActionState == actionState.cooldown));
        return dashingPermits && !isJumping && !isSprinting && !isBlocking;
    }

    public float getHealth()
    {
        return health;
    }

    protected virtual void breakBlock()
    {
        // Override
    }

    protected virtual void endSprint()
    {
        // Override
    }

    private void checkIsDead()
    {
        if (health <= 0)
        {
            // player is dead this causes them to drop their weapon
            // since added rigged body and is trigger to false so it stays on the map
            // then there is a possibility you can pick it up again and add to invenetory if need be
            foreach (Collider wCol in weaponColliders)
            {
                wCol.isTrigger = false;
            }
            isDead = true;
        }
    }

    /*
     * determine what type of weapon is currently being used
     * 
     * returns 'm' if the currently equipped weapon is a melee weapon
     * returns 'g' if it's a gun
     * returns '\0' otherwise
     */
    protected char getCurrentWeaponType()
    {
        Transform curWeapon = getCurrentWeapon();
        if (curWeapon.IsChildOf(gunsParent.transform) || curWeapon.IsChildOf(gunsParentHand.transform))
        {
            return 'g';//we're using a melee weapon, so don't use animation rigging
        }
        else if (curWeapon.IsChildOf(meleeParent.transform))
        {
            return 'm';//we're using a gun
        }
        else
        {
            Debug.Log("no weapon with name " + availableWeapons[equippedWeapon] + " found");
            return '\0';
        }
    }

    /*
     * returns the transform of the currently equipped weapon
     */
    protected Transform getCurrentWeapon()
    {
        Transform curWeapon = gunsParent.transform.Find(availableWeapons[equippedWeapon]);
        if(curWeapon == null)
            curWeapon = gunsParentHand.transform.Find(availableWeapons[equippedWeapon]); //we need to search both possible parents for the gun (although we should actually use the gun while waving it around)

        if (curWeapon == null)
            curWeapon = meleeParent.transform.Find(availableWeapons[equippedWeapon]);

        if (curWeapon == null)
            Debug.Log("no weapon with name " + availableWeapons[equippedWeapon] + " found");

        return curWeapon;
    }

    /*
     * this function will turn off/on animation rigging for the currently equipped weapon
     * won't do anything if a weapon that is not a gun is currently equipped
     */
    protected void toggleAnimRigging(bool turnOn, bool changeParent)
    {
        char type = getCurrentWeaponType();
        Transform curGun = getCurrentWeapon();
        if(turnOn)
        {
            rig.weight = 1;
            if(changeParent && type == 'g') curGun.SetParent(gunsParent.transform, false);
        }
        else
        {
            rig.weight = 0;
            if(changeParent && type == 'g') curGun.SetParent(gunsParentHand.transform, false);
        }
    }
}
