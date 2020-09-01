using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	protected override void handleMovement()
	{

		if (Input.GetAxis("Forward_Backward") != 0 && Input.GetAxis("Left_Right") != 0) //if you're trying to go in two perpendicular directions
			diagonal = true; //you're trying to move diagonally
		else
			diagonal = false;

		movement = new Vector3(Input.GetAxis("Left_Right"), 0, Input.GetAxis("Forward_Backward"));
        movement = movementVectorAdjust(movement);
		if (diagonal)
			movement = movement * 0.7f; //if you're moving diagonally, you need to shorten the movement vector
	}

    private Vector3 movementVectorAdjust(Vector3 original)
    {
        //always convert to unit vectors as soon as possible
        Vector3 leftRight = original.x * characterTransform.right;
        Vector3 forwardBack = original.z * Vector3.Normalize(Vector3.ProjectOnPlane(characterTransform.forward, Vector3.up));
        Vector3 result = leftRight + forwardBack;
        return result;
    }


    protected override void handleJump()
    {
		if (Input.GetAxis("Jump") != 0 && canJump) //if we want to jump and actually can
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
}
