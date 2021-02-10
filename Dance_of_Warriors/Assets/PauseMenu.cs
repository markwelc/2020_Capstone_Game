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
        playa.menuEnabled(false);
    }
    
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        playa.menuEnabled(true);
        
    }

    public void ResetGame()
    {
        Debug.Log("Resetting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
