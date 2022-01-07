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
    [SerializeField] private InputActionReference inputActionReference; //choose an Action from inspector

    [SerializeField] private bool excludeMouse = true;

    [Range(0, 20)]
    [SerializeField] private int selectedBinding; //choose one of the bindings for selected action (keyboard, mouse or gamepad)
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
        rebindScripts = GameObject.FindGameObjectsWithTag("Binding"); //Get all objects with this script on them (needed for resetting all bindings at once or updating UI menu info for all bindings at once)
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
        if(actionText != null) //"Move" is a composite binding (up, down, left, right), checks which subbinding is selected and sets name to include direction
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
            if (Application.isPlaying) //updates UI while application is playing
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
                rebindText.text = UpdateBindingName(rebindText.text);
            }
            else //updates UI when changes are made in the editor
            {
                rebindText.text = inputActionReference.action.bindings[bindingIndex].effectivePath;
                rebindText.text = UpdateBindingName(rebindText.text);
            }
            buttonName = rebindText.text;
        }
    }

    private string UpdateBindingName(string rebindText) //updates binding name/path for selected binding tied to this script
    {
        rebindText = InputManager.GetBindingName(actionName, bindingIndex);
        if (gamepadScript != null && rebindText != null)
        {
            gamepadScript.ChangeIcon(rebindText);
        }

        return rebindText;
    }

    private void UpdateAllUI() //updates binding name for all bindings at once
    {
        foreach (GameObject script in rebindScripts)
        {
            if (script.GetComponent<RebindUI>() != null)
            {
                script.GetComponent<RebindUI>().UpdateUI();
            }
        }
    }

    private void ChangeBinding() //starts rebindingprocess for the binding tied to this script
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse, rebindWarning);
    }

    private void ResetBinding() //resets only the selected binding tied to this script
    {
        InputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }

    public string GetPathName()
    {
        return buttonName;
    }

    public void ResetAllBindings() //resets all bindings (accessed by OnClick() in AdjustUI/HUD
    {
        InputManager.ResetAllBindings();
        UpdateAllUI();
    }
}
