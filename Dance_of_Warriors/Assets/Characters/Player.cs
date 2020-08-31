using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	protected override void handleMovement()
	{

		if (Input.GetAxis("Forward_Backward") != 0 && Input.GetAxis("Left_Right") != 0)
			diagonal = true;
		else
			diagonal = false;

		movement = new Vector3(Input.GetAxis("Left_Right"), 0, Input.GetAxis("Forward_Backward"));
		if (diagonal)
			movement = movement * 0.7f;
	}
}
