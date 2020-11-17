using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    protected float baseDamage; //the base amount of damage this weapon expects to do
    protected int[] phaseTimes; //the lenght of time each phase should last
    public Collider user; //the character using this weapon
   // private Transform weaponTransform;

    public override void useWeapon(string weaponName, out string animation)
    {
        //weaponTransform = this.GetComponent<Transform>();
        animation = animationName;
       // weaponTransform.localScale += new Vector3(0, 0.5f, 0);
    }
}
