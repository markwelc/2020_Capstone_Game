using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected Handgun handgun;
    protected Stick stick;

    protected string animationName; //animation for the primary attack
    protected string animationNameSecondary; //animation for the secondary attack
    protected string animationNameUniqueOne; // and so on..
    protected string animationNameUniqueTwo;
    protected int ammo; //this should be in Guns.cs
    protected int[] weaponStates; //the lengths of action states
    protected int[] weaponStatesSecondary; //the lengths of the action states for the secondary attack
    protected int[] weaponStatesUniqueOne; // and so on...
    protected int[] weaponStatesUniqueTwo;


    //create an array to hold four values
    //in handgun.cs AND stick.cs, set those four values

    protected virtual void Start()
    {
        handgun = this.GetComponentInChildren<Handgun>(true);
        stick = this.GetComponentInChildren<Stick>(true);
        animationName = null;
    }

    /**
     * determines what kind of gun to use and then sets the animation and the action state lengths based on the weapon being used and the attack being used
     * parameters are for the weapon we are using, the name of the animation, the weapons states (time for various actions), 
     * which attack type is being performed (light/heavy), and the amount of damage the weapon does
     */
    public virtual void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)
    {
        states = new int[4];
        switch(weaponName)
        {
            case "handgun":
                handgun.useWeapon((string)null, out animation, out states, attackType, characterDamageModifier); //know th
                break;
            case "stick":
                stick.useWeapon((string)null, out animation, out states, attackType, characterDamageModifier);
                break;
            default:
                animation = null;
                states = null;
                break;
        }
    }

    /**
     * this is just overridden by the Gun and Melee classes
     */
    protected virtual void useWeapon()
    {
        //do nothing
    }

    /**
     * Decide if the weapon can actually deal damage
     * The parameters are for the weapon name and whether it can deal damage at this time
     */
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
