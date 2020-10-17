using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{

    private CinemachineFreeLook cineCam;
    PlayerControls controls;
    Vector2 rotate;
    
    public float yLookSensitivity = 1f;
    public float xLookSensitivity = 1f;
    private void Awake()
    {
        controls = new PlayerControls();
        cineCam = GetComponent<CinemachineFreeLook>();
        //controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Look.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => rotate = Vector2.zero;
        //controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector2 r = new Vector2(rotate.x, rotate.y);
        cineCam.m_XAxis.Value += r.x * 200 * xLookSensitivity * Time.deltaTime;
        cineCam.m_YAxis.Value += r.y * yLookSensitivity * Time.deltaTime;
    }
}
