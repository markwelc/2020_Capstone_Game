using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.Collections;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{

    private CinemachineFreeLook cineCam;
    PlayerControls controls;
    Vector2 rotate;
    public GameObject followTransform;
    public float yLookSensitivity = 1f;
    public float xLookSensitivity = 1f;
    private void Awake()
    {
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
    void Update()
    {
        RotateCamera();
    }

    /**
     * Rotate the camera
     */
    void RotateCamera()
    {
        // get our rotation vector
        Vector2 r = new Vector2(rotate.x, rotate.y);
        // Rotate with the max angle (200 looks good i think) and the sensitivity in each direction
        cineCam.m_XAxis.Value += r.x * 200 * xLookSensitivity * Time.deltaTime;
        cineCam.m_YAxis.Value += r.y * yLookSensitivity * Time.deltaTime;

        followTransform.transform.rotation *= Quaternion.AngleAxis(rotate.x * 200, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(rotate.y * 1, Vector3.right);

    }
}
