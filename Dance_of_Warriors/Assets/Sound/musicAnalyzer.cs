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

        float secondsPerBeat = 1 / ( bpm / 60 ); //convert beats per minute to seconds per beat

        interval = secondsPerBeat / (divisions / 4);
        //a quarter note is one beat, so bpm could also be seen as quarter notes per minute
        //we converted beats per minute to seconds per beat, so now secondsPerBeat can be seen as the amount of time one quarter note takes
        //we don't want quarter notes though, we want 32nd notes (or whatever divisions is)
        // so take secondsPerBeat and divide it by 8 to get the time in seconds between each 32nd note
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
            timer -= interval; //decrease by interval to prevent drifting errors
            count++; //move to the next count
            if (count == 33) count = 1; //I wasn't sure the mod operator was working as expected
            Debug.Log("count = " + count);
        }
    }
}
