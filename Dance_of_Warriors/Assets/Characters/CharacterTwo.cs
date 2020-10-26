using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTwo : MonoBehaviour
{
    //only use this class for things that are universal for all characters (the player, bosses, npcs, rats)
    //anything that's related to decision making should be in subclasses
    //for instance, bosses decide when/where to move very differently from the player, and so deciding when/where to move is in a subclass

    //protected Rigidbody characterRigidbody; //the character's rigidbody
    protected CharacterController characterController;
    protected Transform characterTransform; //the character's transform
    protected Collider characterCollider; //the character's collider
    protected Animator anim;
    bool wasJumping = false;
    protected float healthMax; //the amount of health that the character can have
    protected float health; //the amount of health that the character has

    protected float speed;//the default speed of the character

    //public float staminaMax; //the max amount of stamina the character can have
    //protected float staminaCur; //the current amount of stamina the 

    protected enum actionState { inactive, telegraph, active, recovery }

    protected Vector3 movement;//used to hold the direction that the character should move in
    protected bool diagonal;//whether the player is moving diagonally

    /*[SerializeField]*/
    protected float jumpForce; //this is how strongly the character can jump
    protected bool jumpPossible; //determines if the character can currently jump
    protected actionState jumpActionState; //this may not be needed to restrict jumping, but may be useful in graphics

    /*[SerializeField]*/
    protected int dashing; //keeps track of where we are in the dash
    /*[SerializeField]*/
    protected int[] dashLength; //lists the number of frames that each of the three phases should be active for
    /*[SerializeField]*/
    protected float[] dashSpeed; //indicates the speed of the dash
    protected Vector3 dashVector;//the direction and speed of our dash
    /*[SerializeField]*/
    protected actionState dashActionState;

    [SerializeField] protected WeaponController weaponAccess;
    protected actionState toolActionState;

    protected bool isJumping = false;
    float jumpTime = 0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterController = this.GetComponent<CharacterController>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        characterCollider = this.GetComponent<Collider>(); //get collider
        health = healthMax; //set health
        dashVector = Vector3.zero;
        anim = GetComponent<Animator>();

        jumpActionState = actionState.inactive;
        dashActionState = actionState.inactive;
        toolActionState = actionState.inactive;
        dashing = 0;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(isJumping)
        {
            jumpTime += Time.deltaTime;
        }
        handleMovement();
        //handleJump();
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
        
        if (!Physics.Raycast(ray, out hit, (direcAndDist * Time.deltaTime).magnitude))
        {

            //if the ray didn't hit anything within the range that we're moving
            //  if(!characterController.isGrounded)
            //  {
            float verticalSpeed = -1;
            if(isJumping)
            {
                verticalSpeed = 40f; // vertical force
                verticalSpeed -= jumpTime  * 100f;
                
                if(jumpTime > 0.25f)
                {
                    
                    jumpTime = 0f;
                    isJumping = false;
                    wasJumping = true;
                }
                
            }
            if (wasJumping)
            {
                if (characterController.isGrounded)
                {
                    anim.SetBool("isJumping", false);
                    anim.SetBool("doneJumping", true);
                    wasJumping = false;
                }
            }
            verticalSpeed -= 9.8f;
            direcAndDist.y = verticalSpeed;
            //direcAndDist.y -= 9.8f;
               
            //}
           
            
            characterController.Move(direcAndDist * Time.deltaTime);
            //characterRigidbody.MovePosition((Vector3)transform.position + direcAndDist * Time.deltaTime); //go ahead and move
        }
        else
        {
            Debug.Log("hmmm");
            characterController.Move(hit.point);
            //characterRigidbody.MovePosition(hit.point);//otherwise, move to where the thing we hit was
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


    protected void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (LayerMask.LayerToName(hit.gameObject.layer) == "staticEnvironment")
        {
            Debug.Log("Colliding with something");
            jumpPossible = true;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            jumpPossible = false;
    }
}
