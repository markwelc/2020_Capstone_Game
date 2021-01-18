using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject PauseMenuUI;
    PlayerControls controls;
    
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
        PauseGame = !PauseGame;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (IsGamePaused)
        //     {
        //         Resume();
        //     }
        //     else
        //     {
        //         Pause();
        //     }
        // }

        if (PauseGame == true)
        {
            // PauseMenuUI.SetActive(true);
            // Time.timeScale = 0;
            Pause();
        }
        else
        {
            // inventory.SetActive(false);
            // Time.timeScale = 1;
            Resume();
        }
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
    
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }
}
