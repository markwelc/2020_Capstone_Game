using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Handgun handgun;
    //public bool handgunActive;

    public Stick stick;
   // public bool stickActive;

    protected string animationName;
    protected float[] weaponStates;
    //create an array to hold four values
    //in handgun.cs AND stick.cs, set those four values

    protected virtual void Start()
    {
        animationName = null;
        //handgunActive = false;
        //stickActive = false;
    }

    public virtual void useWeapon(string weaponName, out string animation, out float[] states)
    {
        states = new float[4];
        switch(weaponName)
        {
            case "handgun":
                handgun.useWeapon(); //call the useWeapon function that doesn't care about most of the parameters
                break;
            case "stick":
                stick.useWeapon();
                break;
            default:
                animation = null;
                break;
        }

        animation = animationName; //these both happen in every function
        states = weaponStates;
    }


    protected virtual void useWeapon()
    {
        //do nothing
    }
}
