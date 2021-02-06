using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnBeat : MonoBehaviour
{
    [Header("Original Settings")]
    public float defaultIntensity;
    public float beatIntensity;
    // Music stuff
    [Header("Beat Settings")]
    [Range(0, 3)]
    public int onFullBeat;
    [Range(0, 7)]
    public int[] onBeatD8;
    private int beatCountFull;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkBeat();
    }

    void checkBeat()
    {
        bool hit = false;
        Light[] ll = this.GetComponentsInChildren<Light>();
        // Loop in 4 steps
        beatCountFull = musicAnalyzer.beatCountFull % 4;
        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if (musicAnalyzer.beatD8 && beatCountFull == onFullBeat && musicAnalyzer.beatCountD8 % 8 == onBeatD8[i])
            {
                timer = 0f;
                hit = true;
                foreach(Light l in ll)
                {
                    l.intensity = beatIntensity;
                }
                
            }
        }
        if(!hit)
        {
            timer += Time.deltaTime;
            if (timer > 0.25f)
            {
                foreach (Light l in ll)
                {
                    l.intensity = defaultIntensity;
                }
                timer = 0f;
            }
        }
            

    }
}
