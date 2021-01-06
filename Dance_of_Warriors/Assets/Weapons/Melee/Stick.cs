using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : Melee
{
    protected override void Start()
    {
        animationName = "standard_stick_attack";
        //stickActive = true;
        weaponStates = new float[4];
        weaponStates[0] = 0.0f;
        weaponStates[1] = 0.0f;
        weaponStates[2] = 0.0f;
        weaponStates[3] = 0.0f;
    }
}
