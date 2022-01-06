using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputDelayButton : MonoBehaviour
{
    public Image checkBoxImage;

    void Start()
    {
        if (PlayerPrefs.HasKey("inputDelay")) {
            if (PlayerPrefs.GetInt("inputDelay") == 1)
            {
                checkBoxImage.enabled = true;
            }
            else {
                checkBoxImage.enabled = false;
            }
           
        }
     
    }

   
}
