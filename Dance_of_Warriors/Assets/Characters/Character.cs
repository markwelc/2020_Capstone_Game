using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //only use this class for things that are universal for all characters (the player, bosses, npcs, rats)
    //anything that's related to decision making should be in subclasses
    //for instance, bosses decide when/where to move very differently from the player, and so deciding when/where to move is in a subclass

    public float maxHealth; //the amount of health that the character can have
    public float health; //the amount of health that the character has

    public float speed;//the default speed of the character

    protected Rigidbody characterRigidbody; //the character's rigidbody
    protected Transform characterTransform; //the character's transform
    protected Collider characterCollider; //the character's collider

    protected Vector3 movement;//used to hold the direction that the character should move in
    protected bool diagonal;//whether the player is moving diagonally
    [SerializeField] protected float jumpForce; //this is how strongly the character can jump
    protected bool canJump; //determines if the character can currently jump
    //[SerializeField] protected int jumpHeight; //used to store the max height of the jump (uses the jumpLeft variable)
    //[SerializeField] protected int jumpLeft; //how much jumping force the character has in them



    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = this.GetComponent<Rigidbody>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        characterCollider = this.GetComponent<Collider>(); //get collider
        health = maxHealth; //set health
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovement();
        handleJump();
        handleAngle();
        handleWeapons();

        if (health <= 0)
            die();
        else if (maxHealth < health) //if the character has too much health for some reason
            health = maxHealth;

        moveCharacter(movement);
    }


    protected void moveCharacter(Vector3 direction)
    {
        //the raycasting is useful for fast moving objects that the colliders can't deal with

        Ray ray = new Ray(transform.position, direction); //shoot a ray from current position in direction of travel
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, (direction * speed * Time.deltaTime).magnitude)) //if the ray didn't hit anything within the range that we're moving
            characterRigidbody.MovePosition((Vector3)transform.position + direction * speed * Time.deltaTime); //go ahead and move
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
        Destroy(this.gameObject);
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            canJump = true;
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "staticEnvironment")
            canJump = false;
    }
}
