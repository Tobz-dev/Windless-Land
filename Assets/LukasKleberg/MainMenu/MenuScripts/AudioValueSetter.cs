using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioValueSetter : MonoBehaviour
{
    [SerializeField]
    private VCAController[] vcaControllerArray;

    // Start is called before the first frame update
    void Start()
    {
        SetAudioValues();
    }

    public void SetAudioValues() 
    {
        for (int i = 0; i < vcaControllerArray.Length; i++) 
        {
            vcaControllerArray[i].SetToSavedValues();
        }
    }
}
