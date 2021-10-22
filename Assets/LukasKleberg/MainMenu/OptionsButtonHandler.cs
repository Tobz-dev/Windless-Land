using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonHandler : MonoBehaviour
{
    public Image checkBoxImage;

    public void OnButtonPress()
    {
        //makes the checkbox active if it was inactive, and vice versa
        checkBoxImage.enabled = !checkBoxImage.enabled;
        Debug.Log("reached OnButtonPress");
    }
}
