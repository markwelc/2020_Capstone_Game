using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadArenaScene : MonoBehaviour
{
    public loadLevel levelLoader;
    private bool isLoading;
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
