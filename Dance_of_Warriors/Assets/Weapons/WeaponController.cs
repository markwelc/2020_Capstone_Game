using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /*[SerializeField] private*/ public Handgun handgun;

    public virtual void useWeapon()
    {
        handgun.useWeapon();
    }
}
