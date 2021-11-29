using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class InputManager : MonoBehaviour
{
    //public static PlayerInput inputActions;
    public static PlayerInputs inputActions;
    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;

    private void Awake()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputs();
        }
    }

    public static void StartRebind(string actionName, int bindingIndex, TextMeshProUGUI statusText, bool excludeMouse)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding!! Check inspector reference");
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstIndex = bindingIndex + 1;
            if(firstIndex < action.bindings.Count && action.bindings[firstIndex].isComposite)
            {
                ChangeRebind(action, bindingIndex, statusText, true, excludeMouse);
            }
        }
        else
        {
            ChangeRebind(action, bindingIndex, statusText, false, excludeMouse);
        }
    }

    private static void ChangeRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool compositeBinding, bool excludeMouse)
    {
        if(actionToRebind == null || bindingIndex < 0)
        {
            return; //exits function if InputAction is null or index is invalid/less than zero
        }

        statusText.text = "Press a " + actionToRebind.expectedControlType; //gives feedback to player on which type of button is expected
        actionToRebind.Disable(); //disables action while rebinding is being performed

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex); //creates instance of the rebinding action (does Not start the rebinding process, just creates an instance of the object that's going to do the rebinding)

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (compositeBinding)
            {
                var nextBindingIndex = bindingIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                {
                    ChangeRebind(actionToRebind, nextBindingIndex, statusText, compositeBinding, excludeMouse);
                }
            }

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();

        }); //assigns a delegat that enables the action when rebinding is complete, and disposes of delegate to prevent memory leaks

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        }); //same functionality as above when rebinding is canceled

        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.WithCancelingThrough("<Gamepad>/buttonEast");

        if (excludeMouse)
        {
            rebind.WithControlsExcluding("Mouse");
            /*
            rebind.WithControlsExcluding("<Mouse>/leftButton")
                .WithControlsExcluding("<Mouse>/rightButton")
                .WithControlsExcluding("<Mouse>/press")
                .WithControlsExcluding("<Pointer>/position")
            */
        }

        rebindStarted?.Invoke(actionToRebind, bindingIndex);

        rebind.Start(); //starts the rebinding processs
    }

    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputs();
        }

        InputAction action = inputActions.asset.FindAction(actionName);
        return action.bindings[bindingIndex].effectivePath;
        //action.GetBindingDisplayString(bindingIndex);
    }

    public static void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding!! (aka action is null or binding doesn't exist)");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for(int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
            {
                action.RemoveBindingOverride(i);
            }
        }
        else
        {
            action.RemoveBindingOverride(bindingIndex);
        }
    }

    private static void SaveBindingOverride(InputAction action)
    {
        for(int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputs();
        }

        InputAction action = inputActions.asset.FindAction(actionName);
        for(int i = 0; i < action.bindings.Count; i++)
        {
            if(!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
            {
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }
    }
}
