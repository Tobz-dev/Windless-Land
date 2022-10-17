using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private PlayerPrefReseter playerPrefReseter;

    //This script is only used on the mainmenu.
    //So prefs are only reset from the mainmenu.
    public void LoadNewScene(string nameOfNewScene) 
    {
        playerPrefReseter.ResetPlayerPrefs();

        Debug.Log("in SceneLoader. nameOfNewScene is:" + nameOfNewScene);
        SceneManager.LoadScene(nameOfNewScene);
    }

    public void QuitGame() 
    {
        Debug.Log("in SceneLoader. Quiting game");
        Application.Quit();
    }

}
