using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{

    private FMOD.Studio.VCA VcaController;
    public string VcaName;

    [SerializeField] private float vcaVolume;

    private Slider slider;

    private float savedVolumeSetting;
    private bool isCurrentlyMuted;
    public Image checkBoxImage;

    // Start is called before the first frame update
    void Start()
    {
        SetToSavedValues();
    }

    public void SetVolume(float volume)
    {
        VcaController.setVolume(volume);
        VcaController.getVolume(out vcaVolume);
        //Debug.Log("Setvolume, vcaVolume is:" + vcaVolume);
        PlayerPrefs.SetFloat(VcaName, vcaVolume);


    }

    //the settings need to be applied when the player starts the game. 
    //so call this method. If the applying is in start it only applies when the sliders are set to active. 
    public void SetToSavedValues() 
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
        VcaController.getVolume(out vcaVolume);

        slider.value = PlayerPrefs.GetFloat(VcaName, 1.0f);

        string mutedString = PlayerPrefs.GetString(VcaName + "IsMuted", "NotMuted");

        if (mutedString.Equals("Muted"))
        {
            Debug.Log("vca controller. string was muted");
            checkBoxImage.enabled = true;
            isCurrentlyMuted = true;
        }
    }

    public void AudioToggle()
    {
        //this could use vcaVolume but I'm a bit unsure of how it works. -Lukas
        //if it is currently muted, then it unmutes by setting to the saved value.
        Debug.Log("in audiotoggle. currently muted is: " + isCurrentlyMuted);
        if (isCurrentlyMuted)
        {
            slider.value = PlayerPrefs.GetFloat(VcaName + "SavedValue");

            isCurrentlyMuted = !isCurrentlyMuted;
            //set string to NotMuted
            PlayerPrefs.SetString(VcaName + "IsMuted", "NotMuted");
        }
        //if it is currently not muted, it saves the current value and then mutes.
        else
        {
            PlayerPrefs.SetFloat(VcaName + "SavedValue", slider.value);

            slider.value = 0.0f;
            isCurrentlyMuted = !isCurrentlyMuted;
            //set string to Muted
            PlayerPrefs.SetString(VcaName + "IsMuted", "Muted");
        }
    }
}
