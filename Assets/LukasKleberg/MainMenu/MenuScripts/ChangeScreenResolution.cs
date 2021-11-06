using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenResolution : MonoBehaviour
{

    private bool inFullscreen;
    public Image checkBoxImage;

    private Resolution[] resolutionsArray;

    [SerializeField]
    private TMP_Dropdown resolutionTMPDropdown;

    public void Start()
    {
        //gather info about the possible resolutions
        resolutionsArray = Screen.resolutions;

        //clear the revious options in the dropdown
        resolutionTMPDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;


        for (int i = 0; i < resolutionsArray.Length; i++) 
        {
            string option = resolutionsArray[i].width + " x " + resolutionsArray[i].height;
            resolutionOptions.Add(option);

            if (resolutionsArray[i].width == Screen.currentResolution.width && 
                resolutionsArray[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIndex = i;
            }
        }
        Debug.Log("ChangeScreenResolution. resolutionOptions count is: " + resolutionOptions.Count);
        resolutionTMPDropdown.AddOptions(resolutionOptions);
        resolutionTMPDropdown.value = currentResolutionIndex;
        resolutionTMPDropdown.RefreshShownValue();



        //check if the game starts in fullscreen. 
        //this is reversed because the button says "toggle windowed", so if it shouldn't be checkeed if the game is fullscreen
        if (Screen.fullScreen == true)
        {
            Debug.Log("ChangeScreenResolution. on start set to fullscreen");
            inFullscreen = false;
            checkBoxImage.enabled = false;
        }
    }

    public void SetResolution(int resolutionIndex) 
    {
        Resolution newResolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public void ToggleWindow()
    {

        if (Screen.fullScreen == true)
        {
            Debug.Log("ChangeScreenResolution. set to windowed");
            Screen.fullScreen = false;
            inFullscreen = false;
        }
        else
        {
            Debug.Log("ChangeScreenResolution. set to fullscreen");
            Screen.fullScreen = true;
            inFullscreen = true;
        }
    }
}
