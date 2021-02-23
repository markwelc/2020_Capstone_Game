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
        StartCoroutine(LoadAsync(sceneIdx));
    }

    // Since async we want to run courontine to display progress til finished
    IEnumerator LoadAsync(int sceneIdx)
    {
        // our operation given the desired sceneIndex
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdx);

        // enable loading screen ui
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            // this instead of progress to take activation time into accound
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Debug.Log(progress); // log or progress for now can make ui loader soon

            yield return null;
        }
    }
}
