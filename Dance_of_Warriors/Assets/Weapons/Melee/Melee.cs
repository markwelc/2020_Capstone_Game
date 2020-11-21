using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    protected float baseDamage; //the base amount of damage this weapon expects to do
    protected int[] phaseTimes; //the lenght of time each phase should last
    public Collider user; //the character using this weapon
   // private Transform weaponTransform;

    public override void useWeapon(string weaponName, out string animation, out float states)
    {
        //weaponTransform = this.GetComponent<Transform>();
        animation = animationName;
        states = gunStates[0];
        // weaponTransform.localScale += new Vector3(0, 0.5f, 0);
    }
}
