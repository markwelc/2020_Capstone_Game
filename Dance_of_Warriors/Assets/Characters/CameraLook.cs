using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{

    public CinemachineFreeLook cineCam;
    public CinemachineImpulseSource ImpulseSource;
    private Transform cameraMain, Reticle;

    PlayerControls controls;
    Vector2 rotate;
    public GameObject reticle, gun;

    public bool isCamMovementAllowed = true; //controls when camera movement is allowed. Used for inventory.

    public float yLookSensitivity = 1f;
    public float xLookSensitivity = 1f;
    public float defaultX = 1f;
    public float defaultY = 1f;
    public float xAimAssist = 0.2f;
    public float yAimAssist = 0.2f;
    public float aimFarthestPoint = 100f;
    public float aimNearestPoint = 2f;
    private bool startOutZoomTransition = false;
    private bool startZoomTransition = false;
    public float aimTransitionSpeed = 10f;
    reticleController retController;
    private Image[] crossHairpieces;
    private void Awake()
    {
        cameraMain = Camera.main.transform;
        reticle = GameObject.Find("/Main Camera/Canvas/Reticle");
        Reticle = reticle.transform;
        retController = reticle.GetComponent<reticleController>();
        crossHairpieces = reticle.GetComponentsInChildren<Image>();
        controls = new PlayerControls();
        cineCam = GetComponent<CinemachineFreeLook>();
        // set to initalize look with mouse or right thumbstick
        controls.Gameplay.Look.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => rotate = Vector2.zero;

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

    // Update is called once per frame
    //need to add recoil here
    void Update()
    {
        if (isCamMovementAllowed)
        {
            //controls.Gameplay.Fire.performed += ctx => AddRecoil();
            RotateCamera();
            if(startZoomTransition)
            {
                fovTransition(30);
            }
            else if(startOutZoomTransition)
            {
                fovTransition(55);
            }
        }
    }

    //weapon recoil
    public void AddRecoil()
	{
        ImpulseSource.GenerateImpulse(Camera.main.transform.up);
        retController.setShot();
       // Debug.Log("impulse!");
    }

    /**
     * Rotate the camera
     */
    void RotateCamera()
    {
        //aim assist starts here

        //Ray ray = new Ray(Reticle.transform.position, Reticle.transform.forward);
        Ray ray = new Ray(Reticle.transform.position, Reticle.transform.forward);
        RaycastHit hit = new RaycastHit();
        //drawing the ray from the reticle generates an incorrect angle due to the fact that the reticle is in front of the player in the game world.
        Debug.DrawRay(Reticle.transform.position, Reticle.transform.forward * 10, Color.red, 1);
        //Debug.DrawRay(cameraMain.transform.position, cameraMain.transform.forward * 10, Color.red, 0.5f);


        //determine the sensitivity by looking to see if we can see an enemy or object that we can hit within 100
        if (Physics.Raycast(ray, out hit, aimFarthestPoint))
        {
            //making the knight continuous solves aim assist issue
            if (hit.collider.gameObject.layer == 10)
            {
                enableTargetCross(new Color32(255, 0, 0, 255));
                yLookSensitivity = yAimAssist;
                xLookSensitivity = xAimAssist;
            }
            else
            {
                yLookSensitivity = defaultY;
                xLookSensitivity = defaultX;
                enableTargetCross(new Color32(0, 255, 0, 255));
            }
        }
        else
        {
            //Debug.Log("Hitting world");
            yLookSensitivity = defaultY;
            xLookSensitivity = defaultX;
            enableTargetCross(new Color32(0, 255, 0, 255));
        }

        // get our rotation vector
        Vector2 r = new Vector2(rotate.x, rotate.y);

        // Rotate with the max angle (200 looks good i think) and the sensitivity in each direction
        cineCam.m_XAxis.Value += r.x * 200 * xLookSensitivity * Time.deltaTime;
        cineCam.m_YAxis.Value += r.y * yLookSensitivity * Time.deltaTime;
    }

    private void zoomIn()
    {
        retController.setZoomReticle(true);
        startOutZoomTransition = false;
        startZoomTransition = true;   
        //cineCam.m_Lens.FieldOfView = Mathf.Lerp(25, 60, Time.deltaTime / 200);
        defaultX = 0.5f;
        defaultY = 0.5f;

    }

    private void zoomOut()
    {
        retController.setZoomReticle(false);
        startZoomTransition = false;
        startOutZoomTransition = true;
        //cineCam.m_Lens.FieldOfView = 60;
        defaultX = 1;
        defaultY = 1;
        
    }

    private void fovTransition(float end)
    {
       
        if (end == 30)
        {
            cineCam.m_Lens.FieldOfView -= Time.deltaTime * aimTransitionSpeed;
            if (cineCam.m_Lens.FieldOfView <= end)
            {
                cineCam.m_Lens.FieldOfView = end;
                startZoomTransition = false;
            }
        }
        else
        {
            cineCam.m_Lens.FieldOfView += Time.deltaTime * aimTransitionSpeed;
            if (cineCam.m_Lens.FieldOfView >= end)
            {
                cineCam.m_Lens.FieldOfView = end;
                Debug.Log("Ending Transition");
                startOutZoomTransition = false;
            }
        }
        //Debug.Log("FOV Val :" + cineCam.m_Lens.FieldOfView);

        
    }

    void enableTargetCross(Color32 color)
    {
        foreach(Image cross in crossHairpieces)
        {
            cross.color = color;
        }
    }

}
