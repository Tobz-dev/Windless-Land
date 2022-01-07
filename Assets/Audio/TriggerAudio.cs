using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main Authour : William Smith
public class TriggerAudio : MonoBehaviour
{
    public FMODUnity.EventReference UISounds;
    public string Event;
    public bool PlayOnAwake;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }

    private void Start()
    {
        if (PlayOnAwake)
            PlayOneShot();
    }


}
