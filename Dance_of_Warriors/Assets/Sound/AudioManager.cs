using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioMixer fxMixer;
    public Sound[] sounds;

    
    //use this for initialization
    void Awake()
    {
        //go through the array of objects and add an audio source, volume, and pitch for each
        /*
        foreach(Sound s in sounds)
		{
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

		}
        */
    }

    //look for the sound with the appropriate name to play
    public void Play (GameObject gameObject, String name)
	{
        //Debug.Log(gameObject.name);
        //find the correct sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = fxMixer.FindMatchingGroups("Sounds FX")[0];
        s.source.playOnAwake = false;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.clip = s.clip;
        s.source.spatialBlend = 1.0f;
        
        //if not correct sound, return
        if (s == null)
            return;
        //if the audio source is empty for some reason, do nothing.
        if (s.source == null)
            return;
        //otherwise play the sound on the sound manager
        s.source.Play();
	}
    
}
