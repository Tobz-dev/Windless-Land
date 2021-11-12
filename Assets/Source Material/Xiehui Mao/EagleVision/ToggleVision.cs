using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    private GameObject[] lever;
    private GameObject[] logs;
    private bool istoggled;
    private GameObject[] doors;
    private GameObject[] keys;


    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        istoggled = false;
        

        lever = GameObject.FindGameObjectsWithTag("Lever");
        logs = GameObject.FindGameObjectsWithTag("Logs");
        doors = GameObject.FindGameObjectsWithTag("Door");
        keys = GameObject.FindGameObjectsWithTag("Key");

        foreach (GameObject tagged in lever)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }
        
        foreach (GameObject tagged in logs)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }

        foreach (GameObject tagged in doors)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }

        foreach (GameObject tagged in keys)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }
    }

    void Vision()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
        istoggled = true;
        

        foreach (GameObject tagged in lever)
        {
            tagged.GetComponentInChildren<Renderer>().material.color = Color.yellow;
        }

        foreach (GameObject tagged in logs)
        {
            tagged.GetComponentInChildren<Renderer>().material.color = Color.yellow;
        }

        foreach (GameObject tagged in doors)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.yellow;
        }

        foreach (GameObject tagged in keys)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.yellow;
        }



    }






    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!istoggled)
            {
                Vision();
            }
            else
            {
                visionOff();
                
            }
            
        }
    }

    private void visionOff()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;

        foreach (GameObject tagged in lever)
        {
            tagged.GetComponentInChildren<Renderer>().material.color = Color.white;
        }

        foreach (GameObject tagged in logs)
        {
            tagged.GetComponentInChildren<Renderer>().material.color = Color.white;
        }

        foreach (GameObject tagged in doors)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }

        foreach (GameObject tagged in keys)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }

        istoggled = false;
    }
}
