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
        //handgunActive = true;
        //telegraph, action, recovery, cooldown that is passed to the player to determine if/when they can fire
        gunStates = new float[4];
        gunStates[0] = 3.0f;
        gunStates[1] = 0.0f;
        gunStates[2] = 0.0f;
        gunStates[3] = 0.0f;
    }
}
