using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //only use this class for things that are universal for all characters (the player, bosses, npcs, rats)
    //anything that's related to decision making should be in subclasses
    //for instance, bosses decide when/where to move very differently from the player, and so deciding when/where to move is in a subclass

    protected Rigidbody characterRigidbody; //the character's rigidbody
    protected Transform characterTransform; //the character's transform
    protected Collider characterCollider; //the character's collider

    protected float healthMax; //the amount of health that the character can have
    protected float health; //the amount of health that the character has

    protected float speed;//the default speed of the character

    //public float staminaMax; //the max amount of stamina the character can have
    //protected float staminaCur; //the current amount of stamina the 

    protected enum actionState { inactive, telegraph, active, recovery }

    protected Vector3 movement;//used to hold the direction that the character should move in
    protected bool diagonal;//whether the player is moving diagonally

    /*[SerializeField]*/ protected float jumpForce; //this is how strongly the character can jump
    protected bool jumpPossible; //determines if the character can currently jump
    protected actionState jumpActionState; //this may not be needed to restrict jumping, but may be useful in graphics

    /*[SerializeField]*/ protected int dashing; //keeps track of where we are in the dash
    /*[SerializeField]*/ protected int[] dashLength; //lists the number of frames that each of the three phases should be active for
    /*[SerializeField]*/ protected float[] dashSpeed; //indicates the speed of the dash
    protected Vector3 dashVector;//the direction and speed of our dash
    /*[SerializeField]*/ protected actionState dashActionState;

    [SerializeField] protected WeaponController weaponAccess;
    protected actionState toolActionState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterRigidbody = this.GetComponent<Rigidbody>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        characterCollider = this.GetComponent<Collider>(); //get collider
        health = healthMax; //set health
        dashVector = Vector3.zero;

        jumpActionState = actionState.inactive;
        dashActionState = actionState.inactive;
        toolActionState = actionState.inactive;
        dashing = 0;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovement();
        handleJump();
        handleAngle(); //Commented out as it overriding my angle
        handleWeapons();

        if (health <= 0)
            die();
        else if (healthMax < health) //if the character has too much health for some reason
            health = healthMax; //reduce their health to the max possible

        moveCharacter(movement);
    }


    protected void moveCharacter(Vector3 direcAndDist) //the input needs to contain both the direction and the distance
    {
        //the raycasting is useful for fast moving objects that the colliders can't deal with

        Ray ray = new Ray(transform.position, direcAndDist); //shoot a ray from current position in direction of travel
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
    }

    protected virtual void handleAngle()
    {
        characterTransform.eulerAngles = new Vector3(0, 0, 0); //set no particular angle
    }

    protected virtual void handleWeapons()
    {
        //do nothing
    }


    protected virtual void die()
    {
        //this will get a good deal more complicated eventually, but right now it's simple
        //Destroy(this.gameObject);
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            jumpPossible = true;
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            jumpPossible = false;
    }
}
