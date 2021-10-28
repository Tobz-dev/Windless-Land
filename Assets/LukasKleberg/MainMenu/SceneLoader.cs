using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNewScene(string nameOfNewScene) 
    {
        Debug.Log("in SceneLoader. nameOfNewScene is:" + nameOfNewScene);
        SceneManager.LoadScene(nameOfNewScene);
    }

    public void QuitGame() 
    {
        Debug.Log("in SceneLoader. Quiting game");
        Application.Quit();
    }

}
