using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Handgun handgun;
    //public bool handgunActive;
    //public CameraLook camera;
    public Stick stick;
    // public bool stickActive;
    
    protected string animationName;
    protected int ammo;
    //create an array to hold four values
    //in handgun.cs AND stick.cs, set those four values
    protected int[] gunStates;

    protected virtual void Start()
    {
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
                handgun.useWeapon((string)null, out animation, out states); //know the name of the gun when the function is running, so no need to pass it
                break;
            case "stick":
                stick.useWeapon((string)null, out animation, out states);
                break;
            default:
                animation = null;
                break;
        }
    }
}
