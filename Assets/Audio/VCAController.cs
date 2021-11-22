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
    private bool turnAudioOn;

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

        slider.value = (PlayerPrefs.GetFloat(VcaName));

    }

    public void AudioToggle()
    {
        //this could use vcaVolume but I'm a bit unsure of how it works. -Lukas
        if (turnAudioOn)
        {
            slider.value = savedVolumeSetting;
            turnAudioOn = !turnAudioOn;
        }
        else
        {
            savedVolumeSetting = slider.value;
            slider.value = 0.0f;
            turnAudioOn = !turnAudioOn;
        }
    }
}
