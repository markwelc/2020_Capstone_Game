using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : Melee
{
    /**
     * Initializes the action state lengths and the animation names
     */
    protected override void Start()
    {
        animationName = "standard_stick_attack";
        animationNameSecondary = "heavy_stick_attack";
        animationNameUniqueOne = "slam_attack";
        animationNameUniqueTwo = "low_slash_attack";
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

        weaponStatesUniqueOne = new int[4];
        weaponStatesUniqueOne[0] = 5;
        weaponStatesUniqueOne[1] = 5;
        weaponStatesUniqueOne[2] = 5;
        weaponStatesUniqueOne[3] = 5;

        weaponStatesUniqueTwo = new int[4];
        weaponStatesUniqueTwo[0] = 5;
        weaponStatesUniqueTwo[1] = 5;
        weaponStatesUniqueTwo[2] = 5;
        weaponStatesUniqueTwo[3] = 5;

    }
}
