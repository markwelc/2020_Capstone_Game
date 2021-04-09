using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbUIManager : MonoBehaviour
{
    // Can create instance since only working with player
    public static LimbUIManager limbUIInstance;

    // allows inspector view
    [System.Serializable]
    public struct limbUIElement
    {
        // Dont want to be able to view name attribute
        [System.NonSerialized]
        public string name;
        public GameObject limbContainer;
        public Text limbText;
    };
    private string lLegUI;
    private string rLegUI;
    private string lArmUI;
    private string rArmUI;
    private string bodyUI;
    private string headUI;

    //Inspector to assign the container for that element
    // and the text area for it
    [Header("Available UI Containers(Ordered)")]
    public limbUIElement[] limbUIElements;

    // Setup instance
    private void Awake()
    {
        limbUIInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Strings for different limbs
        // you can change if you want just be sure it still fits in element box
        lLegUI = "Left Leg injured! Speed reduced";
        rLegUI = "Right Leg injured! Speed reduced";
        lArmUI = "Left Arm injured! Damage reduced";
        rArmUI = "Right Arm injured! Damage reduced";
        bodyUI = "Body injured! Defenses reduced";
        headUI = "Head injured! Defenses reduced";
    }

    /**
     * Called from player health controller
     * Only called when a limb is set unusable and it is the player
     * Takes in limb name in order to select UI 
     */
    public void setUIMember(string badLimb)
    {
        Debug.Log("Setting UI element for the limb: " + badLimb);

        // Holds our selected limb UI message based on badLimn
        string selectedUI = "null";
        switch (badLimb)
        {
            case "playerLeftArm":
                selectedUI = lArmUI;
                break;
            case "playerRightArm":
                selectedUI = rArmUI;
                break;
            case "playerLeftLeg":
                selectedUI = lLegUI;
                break;
            case "playerRightLeg":
                selectedUI = rLegUI;
                break;
            case "playerBody":
                selectedUI = bodyUI;
                break;
            case "playerHead":
                selectedUI = headUI;
                break;
            default:
                Debug.LogWarning("Invalid name for limb ui: " + badLimb);
                break;
        }

        // Just to be sure assigned
        if(selectedUI != "null")
            setSelectedUI(selectedUI, badLimb); // set the UI message and name of element
    }

    /**
     * Assigns the selected limb info and enables UI
     */
    private void setSelectedUI(string selectUI, string name)
    {
        // Go through UI elements until you find an empty one (only 6 available)
        for(int i=0; i < limbUIElements.Length; i++)
        {
            // If that limbs container is not active in scene that means we can use it
            if(!limbUIElements[i].limbContainer.activeSelf)
            {
                // set name of struct elem to name of the bad limb passed in
                limbUIElements[i].name = name;
                // set text to that ui info
                limbUIElements[i].limbText.text = selectUI;
                // activate the element
                limbUIElements[i].limbContainer.SetActive(true);
                // break free of loop
                break;
            }
        }
    }

    /**
     * Reset UI elem. calls when healing and limb is reset back to usable
     * called in heal func of health controller.
     * Takes in limb name.
     */
    public void resetUnusableUI(string name)
    {
        for (int i = 0; i < limbUIElements.Length; i++)
        {
            // Be sure we were given one
            if (limbUIElements[i].name != null)
            {
                // Are the names equal
                if (limbUIElements[i].name == name)
                {
                    // reset name to be null
                    limbUIElements[i].name = null;
                    // set text to null 
                    limbUIElements[i].limbText.text = "null";
                    // disable in scene
                    limbUIElements[i].limbContainer.SetActive(false);
                }
            }
        }
    }
}
