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

    protected string animationName; //primary attack animation
    protected string animationNameSecondary; //secondary attack animation
    protected int ammo;
    protected int[] weaponStates;
    
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

    public virtual void useWeapon(string weaponName, out string animation, out int[] states, int attackType)
    {
        states = new int[4];
        switch(weaponName)
        {
            case "handgun":
                handgun.useWeapon((string)null, out animation, out states, attackType); //know th
                //animation = handgun.animationName;
                //states = handgun.weaponStates;
                break;
            case "stick":
                stick.useWeapon((string)null, out animation, out states, attackType);
                //animation = stick.animationName;
                //states = stick.weaponStates;
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
