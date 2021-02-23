using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadArenaScene : MonoBehaviour
{
    public loadLevel levelLoader;

    private void OnTriggerEnter(Collider other)
    {
        // check if player
        if(other.gameObject.layer == 8)
        {
            //SceneManager.LoadScene(1);
            levelLoader.loadScene(2);
        }

    }
}
