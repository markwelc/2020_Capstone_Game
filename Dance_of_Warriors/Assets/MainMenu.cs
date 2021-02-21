using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip clickSound;
    private int playCount = 0;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.Log("Playing Game --> Transfer to staging scene");
    }

    public void viewInfo()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.Log("View game info and warnings");
    }

    public void quitApplication()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.Log("Application Closing...");
    }

    public void playOnSelectSound()
    {
        playCount++;

        // Just because I dont want it to play on scene load
        if(playCount > 1) 
            audioSource.PlayOneShot(selectSound);
    }

}
