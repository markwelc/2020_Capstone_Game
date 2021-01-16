using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //only use this class for things that are universal for all characters (the player, bosses, npcs, rats)
    //anything that's related to decision making should be in subclasses
    //for instance, bosses decide when/where to move very differently from the player, and so deciding when/where to move is in a subclass
    // public LayerMask whatIsGround, whatIsPlayer;

    protected Rigidbody characterRigidbody; //the character's rigidbody
    protected Transform characterTransform; //the character's transform
    protected Collider characterCollider; //the character's collider
    protected Animator anim;
    protected bool isJumping = false;
    public PlayerHealthController playerHealthManager;
    protected float health;
    protected float speed;//the default speed of the character

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

    /*[SerializeField]*/ protected float jumpForce; //this is how strongly the character can jump
    protected bool jumpPossible; //determines if the character can currently jump
    protected actionState jumpActionState; //this may not be needed to restrict jumping, but may be useful in graphics

    [SerializeField] protected WeaponController weaponAccess;
    [SerializeField] protected GameObject weaponsPrefab;
    protected GameObject gunsPrefab; //this is so that we can access only the guns
    [SerializeField] protected GameObject gunsParent; //this will be the parent object of the gunsPrefab
    protected GameObject meleePrefab;
    [SerializeField] protected GameObject meleeParent;
    //[SerializeField] protected GameObject weaponGrip; //stores transform of where we want the weapon to be
    protected actionState toolActionState;
    protected int[] toolStates;
    protected bool isDead;  // To check if dead so player cant continue to move
    [SerializeField] protected string[] availableWeapons;
    protected int equippedWeapon; //which weapon is currently equipped

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        characterRigidbody = this.GetComponent<Rigidbody>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        characterCollider = this.GetComponent<Collider>(); //get collider

        jumpActionState = actionState.inactive;
        toolActionState = actionState.inactive;

        // Get the characters health
        // Applies this script to each object using the character class
        // We can then generate a health bar based on this object by giving the name of the desired object
        playerHealthManager = gameObject.AddComponent<PlayerHealthController>();
        health = playerHealthManager.getHealth();
        isDead = false;
        setWeaponParents();
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

        if(health <= 0)
        {
            isDead = true;
        }

        handleMovement();
        handleJump();
        handleAngle(); //Commented out as it overriding my angle
        handleWeapons();


        moveCharacter(movement);
        if(isJumping)
        {
            if(characterRigidbody.velocity.y < 0f)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("doneJumping", true);
                isJumping = false;
            }
        }

    }


    protected void moveCharacter(Vector3 direcAndDist) //the input needs to contain both the direction and the distance
    {

        //the raycasting is useful for fast moving objects that the colliders can't deal with
        if (isDead)
            direcAndDist = Vector3.zero;

        direcAndDist.y = characterRigidbody.velocity.y;
        Ray ray = new Ray(characterTransform.position, direcAndDist); //shoot a ray from current position in direction of travel
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, (direcAndDist * Time.deltaTime).magnitude)) //if the ray didn't hit anything within the range that we're moving
            characterRigidbody.MovePosition((Vector3)transform.position + direcAndDist * Time.deltaTime); //go ahead and move

        else
        {
            characterRigidbody.MovePosition(hit.point);//otherwise, move to where the thing we hit was
        }
    }


    //these four are only defining default behavior.They are meant to be overridden
    protected virtual void handleMovement()
    {
        movement = new Vector3(0, 0, 0); //don't try to move anywhere
    }

    protected virtual void handleJump()
    {
        //do nothing
        //ordinarily, this is the function that decides when to jumpt
        //in the player character's case, this isn't really needed since you can just run the jump function when the hotkey is pressed
    }

    protected virtual void handleAngle()
    {
        characterTransform.eulerAngles = new Vector3(0, 0, 0); //set no particular angle
    }

    protected virtual void handleWeapons() //decides when to use weapons
    {
        //do nothing
    }

    protected virtual void useWeapons() //actually goes and uses the weapon
    {
        string animation;
        int[] states;
        weaponAccess.useWeapon(availableWeapons[equippedWeapon], out animation, out states); //the first argument will probably be replaced with a default weapon that doesn't exist yet

        if(animation != null)
            anim.SetTrigger(animation); //start the animation
        toolStates = states;
    }

    /*
     * changes the equipped weapon to the next item in the available weapons array
     */
    protected virtual void cycleWeapon()
    {
        equippedWeapon++;
        equippedWeapon = equippedWeapon % availableWeapons.Length;
    }

    //these three functions determine whether the character may jump
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

    protected virtual bool jumpAllowed()
    {
        return jumpPossible;
    }

    public float getHealth()
    {
        return health;
    }

    /*
     * move the Guns and Melee gameObjects that are children of the Weapons gameObject into different places
     */
    void setWeaponParents()
    {
        gunsPrefab = weaponsPrefab.transform.Find("Guns").gameObject; //get the two prefab elements
        meleePrefab = weaponsPrefab.transform.Find("Melee").gameObject;

        gunsPrefab.transform.parent = gunsParent.transform; //make it a child of the correct thing
        Debug.Log("gunsPrefab.transform.parent = " + gunsPrefab.transform.parent);
        gunsPrefab.transform.position = gunsParent.transform.position;//set the position and the rotation
        gunsPrefab.transform.rotation = gunsParent.transform.rotation;

        meleePrefab.transform.parent = meleeParent.transform;
        Debug.Log("meleePrefab.transform.parent = " + meleePrefab.transform.parent);
        meleePrefab.transform.position = meleeParent.transform.position;
        meleePrefab.transform.rotation = meleeParent.transform.rotation;
    }
}
