using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    protected float baseDamage; //the base amount of damage this weapon expects to do
    protected int[] phaseTimes; //the lenght of time each phase should last
    public Collider user; //the character using this weapon
   // private Transform weaponTransform;

    public override void useWeapon(string weaponName, out string animation, out gunStates)
    {
        //weaponTransform = this.GetComponent<Transform>();
        animation = animationName;
        gunStates[0] = 0.0f;
        gunStates[1] = 0.0f;
        gunStates[2] = 0.0f;
        gunStates[3] = 0.0f;
        // weaponTransform.localScale += new Vector3(0, 0.5f, 0);
    }
}
