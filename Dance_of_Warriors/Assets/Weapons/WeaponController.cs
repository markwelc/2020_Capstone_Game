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
                handgun.useWeapon((string)null, out animation, out states, attackType); //know th
                break;
            case "stick":
                stick.useWeapon((string)null, out animation, out states, attackType);
                Debug.LogWarning("da states: " + states);
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
