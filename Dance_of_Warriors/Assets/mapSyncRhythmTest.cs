using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSyncRhythmTest : MonoBehaviour
{
    public musicAnalyzer music;
    public GameObject[] beams;
    lBeam l1, l2, l3, l4;
    private bool lightToggled = false;
    struct lBeam
    {
        public Light[] lights;
    }
    // Start is called before the first frame update
    void Start()
    {
        l1 = new lBeam();
        l1.lights = beams[0].GetComponentsInChildren<Light>();
        l2 = new lBeam();
        l2.lights = beams[1].GetComponentsInChildren<Light>();
        l3 = new lBeam();
        l3.lights = beams[2].GetComponentsInChildren<Light>();
        l4 = new lBeam();
        l4.lights = beams[3].GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (music.getbeat() == 3 && !lightToggled)
        {
            toggleSyncLight();
        }
        else if(music.getbeat() == 1)
        {
            resetLights();
            lightToggled = false;
        }
    }

    private void toggleSyncLight()
    {
        int idx = Random.Range(0, 4);

        switch(idx)
        {
            case 0:
                foreach(Light l in l1.lights)
                {
                    l.intensity = 40;
                }
                break;
            case 1:
                foreach (Light l in l2.lights)
                {
                    l.intensity = 40;
                }
                break;
            case 2:
                foreach (Light l in l3.lights)
                {
                    l.intensity = 40;
                }
                break;
            case 3:
                foreach (Light l in l4.lights)
                {
                    l.intensity = 40;
                }
                break;
            default:
                Debug.LogWarning("Accessing light out of range");
                break;
        }
        lightToggled = true;
    }

    private void resetLights()
    {
        foreach (Light l in l1.lights)
        {
            l.intensity = 10;
        }
        foreach (Light l in l2.lights)
        {
            l.intensity = 10;
        }
        foreach (Light l in l3.lights)
        {
            l.intensity = 10;
        }
        foreach (Light l in l4.lights)
        {
            l.intensity = 10;
        }
    }
}
