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

    private float beatInterval;
    private float beatTimer;
    public int beatCount;
    private int beatDivisions;


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
        beatTimer = 0;
        beatCount = 0;
        beatDivisions = 32;

        // 60 define minute so minutes / beats per minute
        // beatInterval is set to the percentage of a beat completed in a second
        // so beatInterval is beats per second
        beatInterval = 60 / bpm;

        //the counting for the music (1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4) starts the instant that this function is called
        //as opposed to a beat before
        //so beatCountFull being divisble by 4 indicates that we're on the one
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
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatCount++;
            beatCount = beatCount % beatDivisions;
        }
    }
}
