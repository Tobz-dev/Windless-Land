using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionActive : MonoBehaviour
{
    public GameObject visionedCamera;
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
            activ = !activ;
        }
    }
}
