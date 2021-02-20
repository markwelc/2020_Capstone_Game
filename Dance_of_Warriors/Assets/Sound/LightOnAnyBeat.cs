using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAnyBeat : MonoBehaviour
{
    [Header("Original Settings")]
    public float defaultIntensity;
    public float beatIntensity;

    Light[] lights;

    [Header("Beat Settings")]
    [Range(1, 32)] public int[] on32ndNote;

    // Start is called before the first frame update
    void Start()
    {
        //the beat starts right away, so this being divisible by 32 means we're on the 1
        for (int i = 0; i < on32ndNote.Length; i++)
            on32ndNote[i]--;

        //get all the lights and put thm into an array
        lights = this.GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < on32ndNote.Length; i++)
        {
            if (musicAnalyzer.beatCount == on32ndNote[i]) //if we're on a beat that we light up on
            {
                //turn on the light
                foreach (Light bulb in lights)
                    bulb.intensity = beatIntensity;
            }
            else
            {
                foreach (Light bulb in lights)
                    bulb.intensity = defaultIntensity;
            }
        }
    }
}
