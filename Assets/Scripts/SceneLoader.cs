using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadGame()
    {
        SceneManager.LoadScene(0); //load first scene
        // timescale after this is default 0, which is paused so..
        Time.timeScale = 1; //1 is real time
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
