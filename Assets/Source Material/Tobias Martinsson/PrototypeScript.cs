using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;

//Main Author: Tobias Martinsson

public class PrototypeScript : MonoBehaviour
{
    public static bool allowedMove = true;

    [SerializeField]
    public float timeScaleVariable = 1;

    [SerializeField]
    private Image checkbox;

    [SerializeField]
    private TextMeshProUGUI timeScaleText;

    [SerializeField]
    public bool prototypeEnabled = false;

    [SerializeField]
    private float newTimeScaleVariable;

    // Update is called once per frame
    void Update()
    {
        //Checks if a checkbox is enabled or not, to see whether to enable the prototype or not.
        if(checkbox.enabled == true)
        {
            prototypeEnabled = true;
        }
        else if(checkbox.enabled == false)
        {
            prototypeEnabled = false;
        }

        //If the prototype is enabled, change the timescale of the game. If it it's disabled, it simply resets it to 1 (default) when you close the main menu.
        if (prototypeEnabled == true)
        {
            timeScaleVariable = newTimeScaleVariable;
        }
    }

    //Legacy code
    /*
    public void FreezeEnemies()
    {
        allowedMove = false;
    }
    */

    //Changes the timescale of the game. 
    public void ChangeTimeScale(float x)
    {
        //timeScaleText.text = (x % 1).ToString();
        timeScaleText.text = Math.Round(x, 2).ToString();

        newTimeScaleVariable = x;
        
    }
}
