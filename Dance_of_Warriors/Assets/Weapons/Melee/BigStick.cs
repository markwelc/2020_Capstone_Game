using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStick : Melee
{
    // Start is called before the first frame update
    void Start()
    {
        animationName = "great_sword_jump_attack";
        animationNameSecondary = "great_sword_slash_attack";
        //stickActive = true;
        weaponStates = new int[4];
        weaponStates[0] = 5;
        weaponStates[1] = 5;
        weaponStates[2] = 5;
        weaponStates[3] = 5;

        weaponStatesSecondary = new int[4];
        weaponStatesSecondary[0] = 5;
        weaponStatesSecondary[1] = 5;
        weaponStatesSecondary[2] = 5;
        weaponStatesSecondary[3] = 5;
    }
}
