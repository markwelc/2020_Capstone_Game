using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAnalyzer : MonoBehaviour
{
    private int beat = 1;
    private float elapsedTime = 0f;
    public float beatSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beat > 4)
            beat = 1;
        setBeat();   
    }

    /**
     * 
     */
    private void setBeat()
    {
      
        elapsedTime += Time.deltaTime * beatSpeed;
        if (elapsedTime >= 1f)
        {
            //Debug.Log("beat num: " + beat);
            beat++;
            
            
            elapsedTime = 0f;
        }
    }

    public int getbeat()
    {
        return beat;
    }
}
