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

    [Header("Info Items")]
    public GameObject infoPanel;
    public GameObject[] infoScreens;
    // 0 = general UI
    // 1 = keyboard controls
    // 2 = gamepad controls
    // 3 = settings
    // 4 = about

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
        if(audioSource.gameObject.activeSelf)
            infoPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void disableInfoScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(regFirstSelect);

        if(audioSource.gameObject.activeSelf)
            audioSource.PlayOneShot(clickSound);
        infoPanel.SetActive(false);
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

    public void setInfoScreenActive(int idx)
    {
        for(int i = 0; i < infoScreens.Length; i++)
        {
            if(i == idx)
            {
                infoScreens[i].SetActive(true);
            }
            else
            {
                infoScreens[i].SetActive(false);
            }
        }
    }

}
