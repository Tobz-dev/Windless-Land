//Main Author: Miranda Greting
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference;

    [SerializeField] private bool excludeMouse = true;
    //note to self: add exclude keyboard too?

    [Range(0, 20)]
    [SerializeField] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;
   
    [Header("Binding info - DO NOT EDIT")]
    [SerializeField] private InputBinding inputBinding;
    private int bindingIndex;

    private string actionName;

    [Header("UI Fields")]
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private Button rebindButton;
    [SerializeField] private TextMeshProUGUI rebindText;
    [SerializeField] private Button resetButton;

    private GameObject[] rebindScripts;

    public GameObject rebindWarning;

    private string buttonName;

    private ChangeGamepadIcon gamepadScript;

    private void Start()
    {
        rebindScripts = GameObject.FindGameObjectsWithTag("Binding");
        gamepadScript = gameObject.GetComponent<ChangeGamepadIcon>();

    }

    private void Awake()
    {
        //InputManager.LoadBindingOverride(actionName);
        //ResetAllBindings();
    }

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => ChangeBinding());
        resetButton.onClick.AddListener(() => ResetBinding());

        if(inputActionReference != null)
        {
            if(actionName == null)
            {
                //Debug.Log("ActionName is Null!!!");
                actionName = inputActionReference.action.name;
            }
            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }
        InputManager.rebindComplete += UpdateUI;
        InputManager.rebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCanceled -= UpdateUI;

    }

    private void OnValidate()
    {
        if(inputActionReference == null)
        {
            return;
        }

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if(inputActionReference.action != null)
        {
            actionName = inputActionReference.action.name;
            if(inputActionReference.action.bindings.Count > selectedBinding)
            {
                inputBinding = inputActionReference.action.bindings[selectedBinding];
                bindingIndex = selectedBinding;
            }
        }
    }

    private void UpdateUI()
    {
        if(actionText != null)
        {
            actionText.text = actionName;

            if(actionName.Equals("Move") && (selectedBinding == 3 || selectedBinding == 8 || selectedBinding == 13))
            {
                actionText.text = "Move Left";
            }
            if(actionName.Equals("Move") && (selectedBinding == 4 || selectedBinding == 9 || selectedBinding == 14))
            {
                actionText.text = "Move Right";
            }
            if (actionName.Equals("Move") && (selectedBinding == 2 || selectedBinding == 7 || selectedBinding == 12))
            {
                actionText.text = "Move Down";
            }
            if (actionName.Equals("Move") && (selectedBinding == 1 || selectedBinding == 6 || selectedBinding == 11))
            {
                actionText.text = "Move Up";
            }
        }

        if(rebindText != null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
                rebindText.text = UpdateBindingName(rebindText.text);
            }
            else
            {
                rebindText.text = inputActionReference.action.bindings[bindingIndex].effectivePath;
                rebindText.text = UpdateBindingName(rebindText.text);
            }
            if (rebindText.text.Equals("BLANKSTEG"))
            {
                rebindText.text = "Space";
            }
            if (rebindText.text.Equals("À"))
            {
                rebindText.text = "A";
            }
            //rebindText.text = rebindText.text.ToUpper();
            buttonName = rebindText.text;
        }
    }

    private void NameChange(string name)
    {

    }

    private string UpdateBindingName(string rebindText)
    {
        rebindText = InputManager.GetBindingName(actionName, bindingIndex);
        /*
        int splitIndex = rebindText.IndexOf('>')+1;
        if (splitIndex >= 0)
        {
            string bindingString = rebindText.Substring(splitIndex + 1);
            bindingString = bindingString[0].ToString().ToUpper() + bindingString.Substring(1);
            if (rebindText[1].Equals('M'))
            {
                rebindText = "Mouse/" + bindingString;
            }
            else if (bindingString.Equals("RightShoulder"))
            {
                rebindText = "R1";
                //gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("R1");
            }
            else if (bindingString.Equals("RightTriggerButton") || bindingString.Equals("RightTrigger"))
            {
                rebindText = "R2";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("R2");
            }
            else if (bindingString.Equals("LeftShoulder"))
            {
                rebindText = "L1";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("L1");
            }
            else if (bindingString.Equals("LeftTriggerButton") || bindingString.Equals("LeftTrigger"))
            {
                rebindText = "L2";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("L2");
            }
            else if (bindingString.Equals("ButtonSouth"))
            {
                rebindText = "A/Cross";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("A/Cross");
            }
            else if (bindingString.Equals("ButtonWest"))
            {
                rebindText = "X/Square";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("X/Square");
            }
            else if (bindingString.Equals("ButtonNorth"))
            {
                rebindText = "Y/Triangle";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("Y/Triangle");
            }
            else if (bindingString.Equals("ButtonEast"))
            {
                rebindText = "B/Circle";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("B/Circle");
            }
            else if (bindingString.StartsWith("LeftStick"))
            {
                rebindText = bindingString;
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon(rebindText);
            }
            else if (bindingString.Equals("Start"))
            {
                rebindText = "Menu/Options";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon("Menu/Options");
            }
            else if (bindingString.Equals("Select"))
            {
                rebindText = "View/Share";
                gameObject.GetComponent<ChangeGamepadIcon>().ChangeIcon(rebindText);
            }
            else
            {
                rebindText = bindingString;
            }
        }
        */
        if (gamepadScript != null && rebindText != null)
        {
            gamepadScript.ChangeIcon(rebindText);
        }

        return rebindText;
    }

    private void UpdateAllUI()
    {
        foreach (GameObject script in rebindScripts)
        {
            if (script.GetComponent<RebindUI>() != null)
            {
                script.GetComponent<RebindUI>().UpdateUI();
            }
        }
    }

    private void ChangeBinding()
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse, rebindWarning);
    }

    private void ResetBinding()
    {
        InputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }

    public string GetPathName()
    {
        return buttonName;
    }

    public void ResetAllBindings()
    {
        InputManager.ResetAllBindings();
        UpdateAllUI();
    }
}
