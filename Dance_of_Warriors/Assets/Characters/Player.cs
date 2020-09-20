using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	protected override void handleMovement()
	{
        if (dashing <= 0) //if we're not in the middle of dashing
        {
            movement = new Vector3(Input.GetAxis("Left_Right"), 0, Input.GetAxis("Forward_Backward"));
            movement = movementVectorAdjust(movement); //adjust the vector so that it is horizontal and relative to the player

            //stuff up to here seems to work since moving normally works

            if (Input.GetAxis("Dash") != 0 && dashing == 0) //if we wanna start dashing
                //there's an issue here where you can start dashing immediately after having finished the previous dash
                //fixing this involves a lot of stuff that will be added in later
            {
                movement *= dashSpeed; //scales the movement vector
                dashing = dashLength; //sets dashing to its max possible value
                dashVector = movement;//save the current movement vector so that we have it next time this function is called
                    //movement has already been properly scaled
            }
            else
            {
                movement *= speed; //scales the movement vector
            }
        }
        else
        {
            dashing--; //decrement dashing
            movement = dashVector; //set direction and speed to whatever it was in the previous function call
        }
	}

    private Vector3 movementVectorAdjust(Vector3 original)
        //This function ensures that the movement vector is in the right direction and is a unit vector
    {
        Vector3 leftRight = original.x * characterTransform.right;//original.x is sort of a bool determining whether the player wants to move right
            //we may eventually want to normalize leftRight because at the moment it's scaled according to how recently the player hit the button, but forwardBack isn't (and can't be)
        Vector3 forwardBack = original.z * Vector3.Normalize(Vector3.ProjectOnPlane(characterTransform.forward, Vector3.up));
            //we have to be sure that when the player tries to move forward, they move horizontally instead of the way that they are facing (in case they are facing upwards)
        Vector3 result = leftRight + forwardBack; //just add the vectors to get a resultant movement vector
        return Vector3.Normalize(result);//normalize it before returning
    }


    protected override void handleJump()
    {
		if (Input.GetAxis("Jump") != 0 && jumpPossible) //if we want to jump and actually can
		{
			//characterRigidbody.useGravity = false;
			characterRigidbody.AddForce(transform.up * jumpForce);
			//jumpLeft = jumpHeight;
			//Debug.Log("jumpLeft = " + jumpLeft);

            //no need to turn off canJump since there's an onCollisionExit function for that

            //while (Input.GetAxis("Jump") != 0 && jumpLeft > 0)
            //{
            //    characterRigidbody.useGravity = false;
            //    jumpLeft--;
            //}
            //characterRigidbody.useGravity = true;
        }

		//jumpLeft = 0;
	}

	[SerializeField] protected float mouseSensitivity; //hopefully this can be replaced by something in the input manager
	[SerializeField] protected float clampAngle; //the max angle that the character can look up (negate to get the max angle the character can look down)
    protected override void handleAngle()
    {
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = Input.GetAxis("Mouse X");//I don't know why these are switched

        characterTransform.Rotate(mouseX * mouseSensitivity * Time.deltaTime, 0.0f, 0.0f, Space.Self); //I always want to look up and down relative to myself
        characterTransform.Rotate(0.0f, mouseY * mouseSensitivity * Time.deltaTime, 0.0f, Space.World); //but I always want to look left and right relative to the world
    }


    protected override void handleWeapons()
    {
        if (Input.GetAxis("Fire1") != 0)
            weaponAccess.useWeapon();
    }
}
