using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject PauseMenuUI;
    PlayerControls controls;

    public GameObject pauseFirstButton;
    public NewPlayer playa;
    public AudioSource music;

    public loadLevel levelLoader;

    public GameObject regFirstSelect;
    public GameObject altFirstSelect;

    // Info items
    [Header("Info Items")]
    public GameObject infoPanel;
    public GameObject[] infoScreens;
    // 0 = general UI
    // 1 = keyboard controls
    // 2 = gamepad controls
    // 3 = settings
    // 4 = about

    private void Awake()
    {
        controls = new PlayerControls();    // Initialize our controls object
        controls.Gameplay.PauseGame.performed += ctx => GamePause();
    }

    /**
     * Enable Gameplay controls
     */
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    /**
     * Disable Gameplay controls
     */
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void GamePause()
    {
        if(PauseMenuUI.activeSelf)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        infoPanel.SetActive(false); // assery info panel inactive just in case
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        if(playa != null)
            playa.menuEnabled(false);
        if(music != null)
            music.Play();
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        if(pauseFirstButton != null)
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        if(playa != null)
            playa.menuEnabled(true);
        if(music != null)
            music.Pause();

    }

    public void ResetGame()
    {
        Debug.Log("Resetting Game");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        levelLoader.loadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        //Application.Quit();
        levelLoader.loadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
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
        
        // Set panel active
        infoPanel.SetActive(true);
        
    }

    /**
     * Disable info panel called from back button
     */
    public void disableInfoScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(regFirstSelect);

        // disable panel
        infoPanel.SetActive(false);
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
            if (i == idx)
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
