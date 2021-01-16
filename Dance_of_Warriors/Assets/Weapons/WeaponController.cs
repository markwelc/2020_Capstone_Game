using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected Handgun handgun;
    //public bool handgunActive;
    //public CameraLook camera;
    protected Stick stick;
    // public bool stickActive;

    protected string animationName;
    protected int ammo;
    protected float[] weaponStates;
    protected int[] gunStates;
    //create an array to hold four values
    //in handgun.cs AND stick.cs, set those four values

    protected virtual void Start()
    {
        handgun = this.GetComponentInChildren<Handgun>();
        stick = this.GetComponentInChildren<Stick>();
        animationName = null;
        //handgunActive = false;
        //stickActive = false;
    }

    public virtual void useWeapon(string weaponName, out string animation, out int[] states)
    {
        states = new int[4];
        switch(weaponName)
        {
            case "handgun":
                handgun.useWeapon(); //call the useWeapon function that doesn't care about most of the parameters
                animation = handgun.animationName;
                states = handgun.gunStates;
                break;
            case "stick":
                stick.useWeapon();
                animation = stick.animationName;
                states = stick.gunStates;
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
}
