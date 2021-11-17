using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicParameters : MonoBehaviour
{

    private FMOD.Studio.EventInstance safeRoom;
    private Slider slider;
    [SerializeField] private float mainSafe;

    // Start is called before the first frame update
    void Start()
    {
        //safeRoom = FMODUnity.RuntimeManager.CreateInstance("event:/Music/SafeRoom");
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMusicParameters(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Main Safe", volume);
        


    }
}
