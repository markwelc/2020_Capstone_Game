using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimController : MonoBehaviour
{
    PlayerControls controls;
    public GameObject generalCam;
    public GameObject aimCam;


    private void Awake()
    {
        
        controls = new PlayerControls();
        // set to initalize look with mouse or right thumbstick
        controls.Gameplay.Aim.performed += ctx => zoomIn();
        controls.Gameplay.Aim.canceled += ctx => zoomOut();

    }

    /**
     * Enable controls
     */
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    /**
     * Disable controls
     */
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    private void zoomIn()
    {
        Debug.Log("zoom in");
        
    }

    private void zoomOut()
    {
        Debug.Log("zoom out");
        

    }
}
