using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEventReceiver : MonoBehaviour
{
    /**
     * Called by animation event trigger
     * Allows it to be called at the selected animation frame
     * Sends message to character allowing weapon damage
     */
    public void meleeCanDamage()
    {
        // Send message upwards to be called in character
        SendMessageUpwards("weaponDamageAllowed");
    }

    /**
     * Called by animation event trigger
     * Allows it to be called at the selected animation frame
     * Sends message to character allowing weapon damage
     */
    public void meleeNoDamage()
    {
        // Send message upwards to be called in character
        SendMessageUpwards("weaponNoDamageAllowed");
    }
}
