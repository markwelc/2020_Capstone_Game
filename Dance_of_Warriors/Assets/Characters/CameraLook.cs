﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{

    private CinemachineFreeLook cineCam;
    private Transform cameraMain;
    PlayerControls controls;
    Vector2 rotate;
    
    public float yLookSensitivity = 1f;
    public float xLookSensitivity = 1f;
    public float defaultX = 1f;
    public float defaultY = 1f;
    public float xAimAssist = 0.2f;
    public float yAimAssist = 0.2f;
    public float aimFarthestPoint = 100f;
    public float aimNearestPoint = 2f;
    public float VerRecoil = 0;
    public float HorRecoil = 0;

    private void Awake()
    {
        cameraMain = Camera.main.transform;
        controls = new PlayerControls();
        cineCam = GetComponent<CinemachineFreeLook>();
        // set to initalize look with mouse or right thumbstick
        controls.Gameplay.Look.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => rotate = Vector2.zero;
        
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
        controls.Gameplay.Fire.performed += ctx => AddRecoil(5, 1);
        RotateCamera();
    }

    //weapon recoil
    void AddRecoil(float vertical, float horizontal)
	{
        VerRecoil += vertical;
        HorRecoil += horizontal;
	}

    /**
     * Rotate the camera
     */
    void RotateCamera()
    {
        //aim assist starts here
        Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward);
        RaycastHit hit = new RaycastHit();

        //determine the sensitivity by looking to see if we can see an enemy or object that we can hit within 100
        if (Physics.Raycast(ray, out hit, aimFarthestPoint))
        {
            //hit.transform.gameObject.layer == LayerMask.NameToLayer("enemy")
            if (hit.collider.gameObject.CompareTag("Enemy") && hit.distance >= aimNearestPoint)
            {
                //Debug.Log("Hitting enemy");
                //Debug.DrawLine(cameraMain.transform.position, hit.point, Color.red, 1.f);
                yLookSensitivity = yAimAssist;
                xLookSensitivity = xAimAssist;
            }
            else
            {
                yLookSensitivity = defaultY;
                xLookSensitivity = defaultX;
            }
        }
        else
        {
            //Debug.Log("Hitting world");
            yLookSensitivity = defaultY;
            xLookSensitivity = defaultX;
        }

        // get our rotation vector
        Vector2 r = new Vector2(rotate.x, rotate.y);
        
        // Rotate with the max angle (200 looks good i think) and the sensitivity in each direction
        cineCam.m_XAxis.Value += (r.x + HorRecoil) * 200 * xLookSensitivity * Time.deltaTime;
        //cineCam.m_XAxis.Value += HorRecoil;
        cineCam.m_YAxis.Value += (r.y + VerRecoil) * yLookSensitivity * Time.deltaTime;
        //cineCam.m_YAxis.Value += VerRecoil;
        VerRecoil = 0;
        HorRecoil = 0;
    }
}
