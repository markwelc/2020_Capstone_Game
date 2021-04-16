using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadArenaScene : MonoBehaviour
{
    public loadLevel levelLoader;
    private bool isLoading;

    // ONLY USED FOR TESTING IN SAMPLE SCENE
    // NO NEED TO ADD TO DOCUMENTATION
    private void OnTriggerEnter(Collider other)
    {
        // check if player
        if(other.gameObject.layer == 8 && !isLoading)
        {
            // is loading!!
            // though async load was messed but..
            // since it is a trigger you will run in it multiple times
            // so make sure to only start loading once!
            isLoading = true;
            //SceneManager.LoadScene(1);
            levelLoader.loadScene(2);
        }

    }
}
