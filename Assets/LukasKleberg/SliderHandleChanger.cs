using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderHandleChanger : MonoBehaviour
{
    public GameObject speakerCone0, speakerCone1, speakerCone2;

    public Slider slider;

    private float savedVolumeSetting;
    private bool turnOn;

    public AudioMixerSnapshot volumeOff;
    public AudioMixerSnapshot volumeLow;
    public AudioMixerSnapshot volumeMed;
    public AudioMixerSnapshot volumeHig;

    public void SliderChange()
    {
        float sliderValue = slider.value;
        //Debug.Log("reached SliderChange. value is" + sliderValue);

        //would use a switch case but it cant handle comparisions. rip.
        if (sliderValue < 0.2)
        {
            speakerCone0.SetActive(false);
        }
        else
        {
            speakerCone0.SetActive(true);
        }

        if (sliderValue < 3.2)
        {
            speakerCone1.SetActive(false);
        }
        else
        {
            speakerCone1.SetActive(true);
        }

        if (sliderValue < 7.2)
        {
            speakerCone2.SetActive(false);
        }
        else
        {
            speakerCone2.SetActive(true);
        }
    }

    public void SnapshotChange() 
    {
        float sliderValue = slider.value;
        //Debug.Log("reached SliderChange. value is" + sliderValue);

        //would use a switch case but it cant handle comparisions. rip.
        if (sliderValue < 0.2)
        {
            volumeOff.TransitionTo(1.0f);
        }
        else if (0.2 < sliderValue && sliderValue < 3.2)
        {
            volumeLow.TransitionTo(1.0f);
        }
        else if (3.2 < sliderValue && sliderValue < 7.2)
        {
            volumeMed.TransitionTo(1.0f);
        }
        else if (7.2 < sliderValue) 
        {
            volumeHig.TransitionTo(1.0f);
        }

        //volumeLow.TransitionTo(1.0f);
    }

    public void AudioToggle() 
    {
        if (turnOn)
        {
            slider.value = savedVolumeSetting;
            turnOn = !turnOn;
        }
        else 
        {
            savedVolumeSetting = slider.value;
            slider.value = 0.0f;
            turnOn = !turnOn;
        }
    }
}