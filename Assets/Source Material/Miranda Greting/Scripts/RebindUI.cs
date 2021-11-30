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


    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => ChangeBinding());
        resetButton.onClick.AddListener(() => ResetBinding());

        if(inputActionReference != null)
        {
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

            if(actionName.Equals("Move") && (selectedBinding == 3 || selectedBinding == 8))
            {
                actionText.text = "Move Left";
            }
            if(actionName.Equals("Move") && (selectedBinding == 4 || selectedBinding == 9))
            {
                actionText.text = "Move Right";
            }
            if (actionName.Equals("Move") && (selectedBinding == 2 || selectedBinding == 7))
            {
                actionText.text = "Move Down";
            }
            if (actionName.Equals("Move") && (selectedBinding == 1 || selectedBinding == 6))
            {
                actionText.text = "Move Up";
            }
        }

        if(rebindText != null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
                int splitIndex = rebindText.text.IndexOf('/');

                if (splitIndex >= 0)
                {
                    string bindingString = rebindText.text.Substring(splitIndex + 1);
                    bindingString = bindingString[0].ToString().ToUpper() + bindingString.Substring(1);
                    if (rebindText.text[1].Equals('M'))
                    {
                        rebindText.text = "Mouse/" + bindingString;
                    }
                    else
                    {
                        rebindText.text = bindingString;
                    }
                }
            }
            else
            {
                rebindText.text = inputActionReference.action.bindings[bindingIndex].effectivePath;
                int splitIndex = rebindText.text.IndexOf('/');
                
                if (splitIndex >= 0) 
                { 
                    string bindingString = rebindText.text.Substring(splitIndex+1);
                    bindingString = bindingString[0].ToString().ToUpper() + bindingString.Substring(1);
                    if (rebindText.text[1].Equals('M'))
                    {
                        rebindText.text = "Mouse/" + bindingString;
                    }
                    else
                    {
                        rebindText.text = bindingString;
                    }
                }
                    //inputActionReference.action.GetBindingDisplayString(bindingIndex).ToUpper();
                //inputActionReference.action.controls[0].name;
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
        }
    }

    private void ChangeBinding()
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    private void ResetBinding()
    {
        InputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }
}
