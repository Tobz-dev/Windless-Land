using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenChecker : MonoBehaviour
{
    [SerializeField]
    private Image image; 
    //this exists to correct the goddamn cehckmark on the "windowed" button. 
    void Start()
    {
        if (Screen.fullScreen != true)
        {
            Debug.Log("FullscreenChecker. is windowed");
            //

        }
    }


}
