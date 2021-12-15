using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;

public class PrototypeScript : MonoBehaviour
{
    public static bool allowedMove = true;

    private bool slowMotion = false;

    public float timeScaleVariable = 1;

    public Image checkbox;

    public TextMeshProUGUI timeScaleText;

    public bool prototypeEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if(checkbox.enabled == true)
        {
            prototypeEnabled = true;
        }
        else if(checkbox.enabled == false)
        {
            prototypeEnabled = false;
        }        
    }

    public void FreezeEnemies()
    {
        allowedMove = false;
    }

    public void ChangeTimeScale(float x)
    {
        //timeScaleText.text = (x % 1).ToString();
        timeScaleText.text = Math.Round(x, 2).ToString();

        if (prototypeEnabled == true)
        {

            timeScaleVariable = x;
        }
       
    }
}
