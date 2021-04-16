using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : Guns
{
    //stuff is going to go here eventually
    //this will be where stuff like inaccuracy, telegraph time, and recover time will be saved
    
    protected override void Start()
    {
        animationName = null;
        animationNameSecondary = "quick_left_jab";
        ammo = 7;
        //handgunActive = true;
        //telegraph, action, recovery, cooldown that is passed to the player to determine if/when they can fire
        weaponStates = new int[4];
        weaponStates[0] = 2;
        weaponStates[1] = 5;
        weaponStates[2] = 2;
        weaponStates[3] = 30;

        weaponStatesSecondary = new int[4];
        weaponStatesSecondary[0] = 5;
        weaponStatesSecondary[1] = 50;
        weaponStatesSecondary[2] = 50;
        weaponStatesSecondary[3] = 5;
    }
}
