using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip clickSound;
    private int playCount = 0;
    public GameObject loadingScreen;
    public Slider slider;

    public GameObject regFirstSelect;
    public GameObject altFirstSelect;
   

    public GameObject infoScreen;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(clickSound);

        //Debug.Log("Playing Game --> Transfer to staging scene using async");
    }

   

    public void viewInfo()
    {
        // This select isnt working
        // this is really just for controller movement on that UI screem
        // same for the one in disable func below
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(altFirstSelect);

        audioSource.PlayOneShot(clickSound);
        Debug.Log("View game info and warnings");
        infoScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void disableInfoScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(regFirstSelect);

        audioSource.PlayOneShot(clickSound);
        infoScreen.SetActive(false);
        this.gameObject.SetActive(true);
    }

    public void quitApplication()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.Log("Application Closing...");
        Application.Quit();
    }

    public void playOnSelectSound()
    {
        playCount++;

        // Just because I dont want it to play on scene load
        if(playCount > 1) 
            audioSource.PlayOneShot(selectSound);
    }


}
