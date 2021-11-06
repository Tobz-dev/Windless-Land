using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenResolutionList : MonoBehaviour
{

    private const string ResolutionPrefKey = "Resoluion";

    [SerializeField]
    private TextMeshProUGUI resolutionTextMeshPro;

    private Resolution[] resolutionArray;

    //private Resolution resolution

    private int currentResolutionIndex = 0;

    public Text text1, text2;

    private bool inFullscreen;
    public Image windowedCheckBoxImage;

    // Start is called before the first frame update
    void Start()
    {
        //on build Unity gets repeat values for resolutions
        //this solves the problem, but this keeps the refresh rate stuck at 60Hz. (I've tried tons of other solutions).

        Resolution[] tempResolutionArray = Screen.resolutions.Distinct().ToArray();
        var resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct();

        resolutionArray = resolutions.ToArray();

        text2.text = resolutionArray.Length.ToString();

        //get the previous resolution or set to the highest avilable value
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionPrefKey", resolutionArray.Length - 1);

        SetResolutionText(resolutionArray[currentResolutionIndex]);

        //check if the game starts in fullscreen. TODO: playerpref that remembers this setting
        //this is reversed because the button says "toggle windowed", so if it shouldn't be changed if the game is fullscreen
        if (Screen.fullScreen == true)
        {
            Debug.Log("ChangeScreenResolution. on start set to fullscreen");
            inFullscreen = false;
            windowedCheckBoxImage.enabled = false;
        }
    }

    private void SetResolutionText(Resolution resolution) 
    {
        resolutionTextMeshPro.text = resolution.width + " x " + resolution.height;
    }

    public void SetNextResolution ()
    {
        currentResolutionIndex = GetNextWrappedIndex(resolutionArray, currentResolutionIndex);
        SetResolutionText(resolutionArray[currentResolutionIndex]);

        Debug.Log("in SetNextResolution, is x "+ resolutionArray[currentResolutionIndex].width + " y " + resolutionArray[currentResolutionIndex].height);
    }

    public void SetPreviousResolution()
    {
        currentResolutionIndex = GetPreviousWrappedIndex(resolutionArray, currentResolutionIndex);
        SetResolutionText(resolutionArray[currentResolutionIndex]);

        Debug.Log("in SetPreviousResolution, is x " + resolutionArray[currentResolutionIndex].width + " y " + resolutionArray[currentResolutionIndex].height);
    }

    private void ApplyNewResolution(int newResolutionIndex) 
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyResolution();
    }

    private void ApplyResolution() 
    {
        Resolution newResolution = resolutionArray[currentResolutionIndex];
        Debug.Log("in ApplyResolution, is x " + newResolution.width + " y " + newResolution.height);

        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public void ApplyChanges() 
    {
        ApplyNewResolution(currentResolutionIndex);
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

    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex) 
    {
        //if at the end of the collection, return 0. return to start.
        if (collection.Count < 1) 
        {
            return 0;
        }
        else 
        {
            //otherwise go to next
            return (currentIndex + 1) % collection.Count;
        }
        
    }

    private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) 
        {
            return 0;
        }

        if ((currentIndex - 1) < 0)
        {
            return collection.Count - 1;
        }
        else 
        {
           return (currentIndex - 1) % collection.Count;
        }

    }

}
