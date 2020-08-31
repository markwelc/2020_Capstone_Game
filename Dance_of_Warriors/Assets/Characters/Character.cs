using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float maxHealth; //the amount of health that the character can have
    public float health; //the amount of health that the character has

    public float speed;//the default speed of the character

    protected Rigidbody rb; //the character's rigidbody
    protected Transform characterTransform; //the character's transform

    protected Vector3 movement;//used to hold the direction that the character should move in
    protected bool diagonal;//whether the player is moving diagonally


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>(); //get rigidbody
        characterTransform = this.GetComponent<Transform>(); //get transform
        health = maxHealth; //set health
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovement();
        handleAngle();
        handleWeapons();

        if (health <= 0)
            die();
        else if (maxHealth < health) //if there character has too much health for some reason
            health = maxHealth;

        moveCharacter(movement);
    }


    protected void moveCharacter(Vector3 direction)
    {
        //the raycasting is useful for fast moving objects that the colliders can't deal with

        Ray ray = new Ray(transform.position, direction); //shoot a ray from current position in direction of travel
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, (direction * speed * Time.deltaTime).magnitude)) //if the ray didn't hit anything within the range that we're moving
            rb.MovePosition((Vector3)transform.position + direction * speed * Time.deltaTime); //go ahead and move
        else
        {
            rb.MovePosition(hit.point);//otherwise, move to where the thing we hit was
        }
    }


    //these four are only defining default behavior.They are meant to be overridden
    protected virtual void handleMovement()
    {
        movement = new Vector3(0, 0, 0); //don't try to move anywhere
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
}
