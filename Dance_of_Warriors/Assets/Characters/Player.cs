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
		if (diagonal)
			movement = movement * 0.7f; //if you're moving diagonally, you need to shorten the movement vector
	}


    protected override void handleJump()
    {
		if (Input.GetAxis("Jump") != 0 && canJump) //if we want to jump and actually can
		{
			//characterRigidbody.useGravity = false;
			characterRigidbody.AddForce(transform.up * jumpForce);
			//jumpLeft = jumpHeight;
			//Debug.Log("jumpLeft = " + jumpLeft);

            //no need to turn off canJump since there's a onCollisionExit function for that

            //while (Input.GetAxis("Jump") != 0 && jumpLeft > 0)
            //{
            //    characterRigidbody.useGravity = false;
            //    jumpLeft--;
            //}
            //characterRigidbody.useGravity = true;
        }

		//jumpLeft = 0;
	}
}
