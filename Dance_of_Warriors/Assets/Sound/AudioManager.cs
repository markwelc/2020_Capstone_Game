using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    //use this for initialization
    void Awake()
    {
        //go through the array of objects and add an audio source, volume, and pitch for each
        foreach(Sound s in sounds)
		{
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

		}
    }

    //look for the sound with the appropriate name to play
    public void Play (string name)
	{
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
	}
    
}
