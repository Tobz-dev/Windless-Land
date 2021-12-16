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

    // Start is called before the first frame update
    private void Start()
    {
        //on build Unity gets repeat values for resolutions
        //this solves the problem, but this keeps the refresh rate stuck at 60Hz. (I've tried tons of other solutions).

        Resolution[] tempResolutionArray = Screen.resolutions.Distinct().ToArray();
        var resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct();

        resolutionArray = resolutions.ToArray();

        //get the previous resolution or set to the highest avilable value
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionPrefKey", resolutionArray.Length - 1);

        SetResolutionText(resolutionArray[currentResolutionIndex]);
        
    }

    private void SetResolutionText(Resolution resolution) 
    {
        resolutionTextMeshPro.text = resolution.width + " x " + resolution.height;
    }

    public void SetNextResolution ()
    {
        currentResolutionIndex = GetNextWrappedIndex(resolutionArray, currentResolutionIndex);
        SetResolutionText(resolutionArray[currentResolutionIndex]);

        //Debug.Log("in SetNextResolution, is x "+ resolutionArray[currentResolutionIndex].width + " y " + resolutionArray[currentResolutionIndex].height);
    }

    public void SetPreviousResolution()
    {
        currentResolutionIndex = GetPreviousWrappedIndex(resolutionArray, currentResolutionIndex);
        SetResolutionText(resolutionArray[currentResolutionIndex]);

        //Debug.Log("in SetPreviousResolution, is x " + resolutionArray[currentResolutionIndex].width + " y " + resolutionArray[currentResolutionIndex].height);
    }

    public void SetDefaultResolution() 
    {
        Debug.Log("in SetDefaultResolution");

        currentResolutionIndex = resolutionArray.Length - 1;
        ApplyResolution();
    }

    private void ApplyNewResolution(int newResolutionIndex) 
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyResolution();
    }

    private void ApplyResolution() 
    {
        Resolution newResolution = resolutionArray[currentResolutionIndex];
        //Debug.Log("in ApplyResolution, is x " + newResolution.width + " y " + newResolution.height);

        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public void ApplyChanges() 
    {
        ApplyNewResolution(currentResolutionIndex);
    }

    public void ToggleWindow(bool setToFullscreen)
    {

        if (setToFullscreen == false)
        {
            //Debug.Log("ChangeScreenResolution. set to windowed");
            Screen.fullScreen = false;
        }
        else
        {
            //Debug.Log("ChangeScreenResolution. set to fullscreen");
            Screen.fullScreen = true;

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
