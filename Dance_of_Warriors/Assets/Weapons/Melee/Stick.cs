using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : Melee
{
    protected override void Start()
    {
        animationName = "standard_stick_attack";
        //stickActive = true;
        weaponStates = new int[4];
        weaponStates[0] = 0;
        weaponStates[1] = 0;
        weaponStates[2] = 0;
        weaponStates[3] = 0;
    }
}
