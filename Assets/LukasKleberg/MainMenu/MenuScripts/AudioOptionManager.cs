using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Slider slider;
    private float savedVolumeSetting;
    private bool turnAudioOn;

    public void Start()
    {
        //like get a player pref to remmber if it's turned off or not.
        //and then set things accordingly.
        //but how. there are multipel objects active that have this script
        //like a string for the player pref. and then the volume, when that happens.
    }

    /*public void AudioToggle()
    {
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
    }*/
}
