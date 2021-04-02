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

    /**
     * Play the game.
     * Just plays sound here
     * actual level loader is called from that script on click
     */
    public void PlayGame()
    {
        audioSource.PlayOneShot(clickSound);
    }

    /**
     * Opens info panel so the user can select which to see
     * can see control configurations, about, and edit settings
     */
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

    /**
     * Disable info panel called from back button
     */
    public void disableInfoScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(regFirstSelect);

        // Play click
        if(audioSource.gameObject.activeSelf)
            audioSource.PlayOneShot(clickSound);

        // set info active disable standard
        infoPanel.SetActive(false);
        this.gameObject.SetActive(true);
    }

    /**
     * Quit the application
     * This has no effect in unity play mode
     */
    public void quitApplication()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.LogWarning("You are quitting the application");
        Application.Quit();
    }

    /**
     * Play sound when selecting button
     * skip the first one since selects at start
     */
    public void playOnSelectSound()
    {
        playCount++;

        // Just because I dont want it to play on scene load
        if(playCount > 1) 
            audioSource.PlayOneShot(selectSound);
    }

    /**
     * Set a given info screen active
     * 5 options
     * overall
     * keyboard config
     * gamepad config
     * settings 
     * about
     */
    public void setInfoScreenActive(int idx)
    {
        // Given index from click
        // only one can be active at a time
        // enable given idx set all others inactive
        for (int i = 0; i < infoScreens.Length; i++)
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
