using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PrototypeScript : MonoBehaviour
{
    public static bool allowedMove = true;

    private bool slowMotion = false;

    public float timeScaleVariable = 1;

    public Image checkbox;

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
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(slowMotion == false)
            {
                Debug.Log("slowmotion");
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                slowMotion = true;
            }
            else
            {
                Debug.Log("slowmotion");
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                slowMotion = false;
            }
            
        }
        
        
    }

    public void FreezeEnemies()
    {
        allowedMove = false;
    }

    public void ChangeTimeScale(float x)
    {
        if(prototypeEnabled == true)
        {

            timeScaleVariable = x;
        }
       
    }
}
