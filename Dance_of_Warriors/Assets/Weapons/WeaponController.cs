using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected Handgun handgun;
    protected Stick stick;

    protected string animationName;
    protected string animationNameSecondary; //the secondary attack
    protected int ammo;
    protected int[] weaponStates;
    protected int[] weaponStatesSecondary;

    //create an array to hold four values
    //in handgun.cs AND stick.cs, set those four values

    protected virtual void Start()
    {
        handgun = this.GetComponentInChildren<Handgun>(true);
        stick = this.GetComponentInChildren<Stick>(true);
        animationName = null;
        //handgunActive = false;
        //stickActive = false;
    }

    public virtual void useWeapon(string weaponName, out string animation, out int[] states, int attackType)
    {
        states = new int[4];
        switch(weaponName)
        {
            case "handgun":
                handgun.useWeapon("handgun", out animation, out states, attackType);
                break;
            case "stick":
                stick.useWeapon((string)null, out animation, out states, attackType);
                break;
            default:
                animation = null;
                states = null;
                break;
        }
    }


    protected virtual void useWeapon()
    {
        //do nothing
    }

    public virtual void canDealDamage(string weaponName, bool canDamage)
    {
        //which weapon can/cant do damage?

        switch (weaponName)
        {
            case "stick":
                stick.canDealDamage(weaponName, canDamage);
                break;
            default:
                // Nothing to be done here could be a gun where bullet always uses damage
                break;
        }
        
    }
}
