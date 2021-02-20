using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAnalyzer : MonoBehaviour
{
    // Create an instance so there is only one and can access easy
    private static musicAnalyzer _musicAnalyzer;

    // Plug beats per minute from song in
    public float bpm;

    // Interval between, time, full beat, beat count
    // D8 = divided by 8
    //private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;
    //public static bool beatFull, beatD8;
    //public static int beatCountFull, beatCountD8;

    private float interval;
    private float timer;
    public static int count;
    private int divisions;


    // Ensure there is only 1 instance
    private void Awake()
    {
        if(_musicAnalyzer != null && _musicAnalyzer != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _musicAnalyzer = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        count = 1; //start at 1 but start right away
        divisions = 32;

        bpm = bpm / 60; //convert beats per minute to beats per second

        interval = bpm / (divisions / 4);
    }

    // Update is called once per frame
    void Update()
    {
        beatCounter();
        //BeatDetection();
    }

    //void BeatDetection()
    //{
    //    // Not full yet
    //    // this will be full when we have waited long enough for one beat
    //    beatFull = false;

    //    beatTimer += Time.deltaTime; // add a second

    //    // Is the timer grater?
    //    if(beatTimer >= beatInterval)
    //    {
    //        // beat is now full
    //        beatTimer -= beatInterval;
    //        beatFull = true;
    //        beatCountFull++;
    //    }
    //    // Divided beat count
    //    beatD8 = false;
    //    beatIntervalD8 = beatInterval / 8;
    //    beatTimerD8 += Time.deltaTime;
    //    if(beatTimerD8 >= beatIntervalD8)
    //    {
    //        beatTimerD8 -= beatIntervalD8;
    //        beatD8 = true;
    //        beatCountD8++;
    //    }
    //}

    void beatCounter()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer -= interval;
            count++;
            Debug.Log("count = " + count);
            count = count % divisions;
        }
    }
}
