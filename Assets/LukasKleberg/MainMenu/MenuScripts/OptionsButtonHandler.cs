using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonHandler : MonoBehaviour
{
    public Image checkBoxImage;

    private void Start()
    {
        if (PlayerPrefs.HasKey("inputDelay"))
        {
            if (PlayerPrefs.GetInt("inputDelay") == 1)
            {
                checkBoxImage.enabled = true;
            }
            if (PlayerPrefs.GetInt("inputDelay") == 0)
            {
                checkBoxImage.enabled = true;
            }
        }
    }

    public void OnButtonPress()
    {
        //makes the checkbox active if it was inactive, and vice versa
        checkBoxImage.enabled = !checkBoxImage.enabled;
        //Debug.Log("reached OnButtonPress");
    }
}
