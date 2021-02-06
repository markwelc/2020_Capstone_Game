using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class reticleController : MonoBehaviour
{
    private RectTransform ourReticle;
    public float transitionSpeed;
    public float aimSize;
    public float restingSize;
    public float movingSize;
    public float movingAndAimingSize;

    private float currentSize;
    private bool zoomingR = false;
    private bool movingR = false;
    // Start is called before the first frame update
    void Start()
    {
        ourReticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //ourReticle.sizeDelta = new Vector2(size, size);

        if(zoomingR && movingR)
            currentSize = Mathf.Lerp(currentSize, movingAndAimingSize, Time.deltaTime * transitionSpeed);
        else if (zoomingR)
            currentSize = Mathf.Lerp(currentSize, aimSize, Time.deltaTime * transitionSpeed);
        else if(movingR)
            currentSize = Mathf.Lerp(currentSize, movingSize, Time.deltaTime * transitionSpeed);
        else
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * transitionSpeed);

        ourReticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    public void setZoomReticle(bool val)
    {
        zoomingR = val;
    }

    public void setMoving(bool val)
    {
        movingR = val;
    }

    public void setShot()
    {
        currentSize = Mathf.Lerp(currentSize, currentSize + 30, Time.deltaTime * 5 * transitionSpeed);
        ourReticle.sizeDelta = new Vector2(currentSize, currentSize);
    }
}
