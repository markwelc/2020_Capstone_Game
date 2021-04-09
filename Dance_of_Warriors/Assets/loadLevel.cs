using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadLevel : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;


    // Given scene idx load that scene
    public void loadScene(int sceneIdx)
    {
        // Doing async so we can be sure everything is loaded before putting player in
        // need courintine to continue and get progress asyncronisouly
        StartCoroutine(asyncLoad(sceneIdx));
        // StartCoroutine(oldLoad(sceneIdx));
    }

    
    /**
     * Async loading..
     * Allows for the scene to actually get ready before allowing the player to enter
     * in meantime just show loading screen with updates
     */
    IEnumerator asyncLoad(int sceneIdx)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdx);
        // enable loading screen ui
        //operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            //Debug.LogWarning(progress); // log or progress for now can make ui loader soon

            yield return null;
        }
        Debug.LogWarning("out da loop");
    }
}
