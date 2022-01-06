//Main Author: Miranda Greting
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ChangeGamepadIcon : MonoBehaviour
{
    [SerializeField] private GameObject gamepadBindings;
    private List<GameObject> bindingIcons = new List<GameObject>();

    public void Start()
    {
        for(int i = 0; i < gamepadBindings.transform.childCount; i++)
        {
            bindingIcons.Add(gamepadBindings.transform.GetChild(i).gameObject);
        }
    }

    public void ChangeIcon(string bindingName)
    {
        if(bindingIcons.Count <= 0)
        {
            for (int i = 0; i < gamepadBindings.transform.childCount; i++)
            {
                bindingIcons.Add(gamepadBindings.transform.GetChild(i).gameObject);
            }
        }

        foreach (GameObject icon in bindingIcons)
        {
            if (icon.name.Contains(bindingName))
            {
                icon.SetActive(true);
            }
            else
            {
                icon.SetActive(false);
            }
        }
    }
}
