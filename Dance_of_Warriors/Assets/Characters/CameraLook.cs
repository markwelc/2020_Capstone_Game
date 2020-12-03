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
    public float defaultX = 1f;
    public float defaultY = 1f;
    public float xAimAssist = 0.2f;
    public float yAimAssist = 0.2f;
    public float aimFarthestPoint = 100f;
    public float aimNearestPoint = 2f;

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
        Camera ourCamera = Camera.main;

        //aim assist starts here
        RaycastHit hit;
        var forward = transform.TransformDirection(Vector3.forward);
        Vector3 ray = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1, 1, 6));

        //determine the sensitivity by looking to see if we can see an enemy or object that we can hit
        if (Physics.Raycast(ray, GetComponent<Camera>().transform.forward, out hit, aimFarthestPoint))
		{
            if (hit.collider.gameObject.tag == "Enemy" && hit.distance >= aimNearestPoint)
			{
                Debug.Log("Hitting enemy");
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
            Debug.Log("Hitting world");
            yLookSensitivity = defaultY;
            xLookSensitivity = defaultX;
		}

        // Rotate with the max angle (200 looks good i think) and the sensitivity in each direction
        cineCam.m_XAxis.Value += r.x * 200 * xLookSensitivity * Time.deltaTime;
        cineCam.m_YAxis.Value += r.y * yLookSensitivity * Time.deltaTime;
    }
}
