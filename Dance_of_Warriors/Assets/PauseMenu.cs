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
}
