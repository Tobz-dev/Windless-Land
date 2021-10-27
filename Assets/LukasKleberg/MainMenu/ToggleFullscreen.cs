using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFullscreen : MonoBehaviour
{

    private bool inFullscreen;
    public Image checkBoxImage;

    public void Start()
    {
        //check if the game starts in fullscreen
        if (Screen.fullScreen == true)
        {
            Debug.Log("Togglefullsreen. on start set to fullscreen");
            inFullscreen = true;
            checkBoxImage.enabled = true;
        }
    }

    public void WindowedHandler()
    {

        if (inFullscreen)
        {
            Debug.Log("Togglefullsreen. set to windowed");
            Screen.fullScreen = false;
            inFullscreen = false;
        }
        else
        {
            Debug.Log("Togglefullsreen. set to fullscreen");
            Screen.fullScreen = true;
            inFullscreen = true;
        }
    }
}
