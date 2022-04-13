using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author:Xiehui Mao
public class VisionActive : MonoBehaviour
{
    public GameObject visionedCamera;
    public Camera normalcamera;
    public Camera visionedcamera;
    private bool activ;
    // Start is called before the first frame update
    void Start()
    {
        activ = false;
    }

    public void OnButtonPress()
    {
        if (activ == false)
        {
            visionedCamera.SetActive(true);
            activ = !activ;
        }
        else
        {
            visionedCamera.SetActive(false);
            normalcamera.enabled = true;
            visionedcamera.enabled = false;
            activ = !activ;
        }
    }
}
