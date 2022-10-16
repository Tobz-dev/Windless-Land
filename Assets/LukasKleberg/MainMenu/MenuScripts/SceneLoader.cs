using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNewScene(string nameOfNewScene) 
    {
        //to reset the PortalChoice values when starting a new game.
        //TODO: place this in a seperate script. that also resets the other PlayerPrefs.
        PlayerPrefs.SetString("TriggerA", "NotActivated");
        PlayerPrefs.SetString("TriggerB", "NotActivated");

        Debug.Log("in SceneLoader. nameOfNewScene is:" + nameOfNewScene);
        SceneManager.LoadScene(nameOfNewScene);
    }

    public void QuitGame() 
    {
        Debug.Log("in SceneLoader. Quiting game");
        Application.Quit();
    }

}
