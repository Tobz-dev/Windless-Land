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

    private static InputManager instance;

    private void Awake()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputs();
        }

        instance = this;
    }

    public static void StartRebind(string actionName, int bindingIndex, TextMeshProUGUI statusText, bool excludeMouse, GameObject rebindPanel)
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputs();
        }
        InputAction action = inputActions.asset.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding!! Check inspector reference");
        }

        rebindPanel.SetActive(true);

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstIndex = bindingIndex + 1;
            if(firstIndex < action.bindings.Count && action.bindings[firstIndex].isComposite)
            {
                ChangeRebind(action, bindingIndex, statusText, true, excludeMouse, rebindPanel);
            }
        }
        else
        {
            ChangeRebind(action, bindingIndex, statusText, false, excludeMouse, rebindPanel);
        }
    }

    private static void ChangeRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool compositeBinding, bool excludeMouse, GameObject rebindPanel)
    {
        if(actionToRebind == null || bindingIndex < 0)
        {
            return; //exits function if InputAction is null or index is invalid/less than zero
        }
        InputBinding previousBinding = actionToRebind.bindings[bindingIndex];
        statusText.text = "Press a " + "button"; //actionToRebind.expectedControlType; //gives feedback to player on which type of button is expected
        actionToRebind.Disable(); //disables action while rebinding is being performed
        
        InputActionRebindingExtensions.RebindingOperation rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex); //creates instance of the rebinding action (does Not start the rebinding process, just creates an instance of the object that's going to do the rebinding)

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if(CheckDuplicateBindings(actionToRebind, bindingIndex, compositeBinding, previousBinding, rebind, rebindPanel.transform.GetChild(2).gameObject))
            {
                //InputBinding duplicateBinding = actionToRebind.bindings[bindingIndex];
                //actionToRebind.RemoveBindingOverride(bindingIndex);
                //actionToRebind.ApplyBindingOverride(previousBinding);
                //SwapBindingsPrompt(rebindPanel.transform.GetChild(2).gameObject, previousBinding, duplicateBinding);
                //CleanUp();
                //ChangeRebind(actionToRebind, bindingIndex, statusText, compositeBinding, excludeMouse, rebindPanel);
                rebind.Cancel();
                return;
            }

            /*
            if(actionToRebind.bindings[bindingIndex].effectivePath.Equals("<Keyboard>/enter") || actionToRebind.bindings[bindingIndex].effectivePath.Equals("<Keyboard>/escape") || actionToRebind.bindings[bindingIndex].effectivePath.Equals("<Keyboard>/numpadEnter") 
            || actionToRebind.bindings[bindingIndex].effectivePath.Equals("<Keyboard>/leftMeta")) //cancels rebinding if chosen key isn't allowed/is otherwise occupied (escape, enter, windows/meta key etc)
            {
                actionToRebind.RemoveBindingOverride(bindingIndex);
                ChangeRebind(actionToRebind, bindingIndex, statusText, compositeBinding, excludeMouse, rebindPanel);
                rebindPanel.transform.GetChild(2).gameObject.SetActive(true);
                instance.StartCoroutine(DelayInactivation(2f, rebindPanel.transform.GetChild(2).gameObject));
                return;
            }
            */

            if (compositeBinding)
            {
                var nextBindingIndex = bindingIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                {
                    ChangeRebind(actionToRebind, nextBindingIndex, statusText, compositeBinding, excludeMouse, rebindPanel);
                }
            }

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
            rebindPanel.SetActive(false);
            Debug.Log(actionToRebind.bindings[bindingIndex].effectivePath);

        }); //assigns a delegate that enables the action when rebinding is complete, and disposes of delegate to prevent memory leaks

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
            rebindPanel.SetActive(false);
        }); //same functionality as above when rebinding is canceled
        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.WithCancelingThrough("<Gamepad>/buttonEast");

        if (excludeMouse)
        {
            rebind.WithControlsExcluding("Mouse");
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
        string rebindText = action.bindings[bindingIndex].effectivePath;
        int splitIndex = rebindText.IndexOf('>') + 1;
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
            }
            else if (bindingString.Equals("LeftShoulder"))
            {
                rebindText = "L1";
            }
            else if (bindingString.Equals("LeftTriggerButton") || bindingString.Equals("LeftTrigger"))
            {
                rebindText = "L2";
            }
            else if (bindingString.Equals("ButtonSouth"))
            {
                rebindText = "A/Cross";
            }
            else if (bindingString.Equals("ButtonWest"))
            {
                rebindText = "X/Square";
            }
            else if (bindingString.Equals("ButtonNorth"))
            {
                rebindText = "Y/Triangle";
            }
            else if (bindingString.Equals("ButtonEast"))
            {
                rebindText = "B/Circle";
            }
            else if (bindingString.StartsWith("LeftStick"))
            {
                rebindText = bindingString;
            }
            else if (bindingString.Equals("Start"))
            {
                rebindText = "Menu/Options";
            }
            else if (bindingString.Equals("Select"))
            {
                rebindText = "View/Share";
            }
            else
            {
                rebindText = bindingString;
            }
            //action.GetBindingDisplayString(bindingIndex);
        }
        return rebindText;
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
        SaveBindingOverride(action);
    }

    public static void ResetAllBindings()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputs();
        }

        //Note to self: removes all bindings in all actionMaps, not just current/gameplay map, change if multiple action maps will be used
        foreach (InputActionMap map in inputActions.asset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        foreach (InputAction action in inputActions.asset)
        {
            SaveBindingOverride(action);
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
        Debug.Log(actionName);
        InputAction action = inputActions.asset.FindAction(actionName);
        if (action != null)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                {
                    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
                }
            }
        }
        if(action == null)
        {
            Debug.Log(actionName);
        }
    }
    
    //checks if chosen rebinding already binds to another action
    //if it does, the player is given the option to swap rebindings for the two actions
    //or cancel the rebinding process
    private static bool CheckDuplicateBindings(InputAction actionToRebind, int bindingIndex, bool allCompositeParts, InputBinding previousBinding, InputActionRebindingExtensions.RebindingOperation rebind, GameObject rebindWarning)
    {
        InputBinding newBinding = actionToRebind.bindings[bindingIndex];
        foreach (InputBinding duplicateBinding in actionToRebind.actionMap.bindings)
        {
            if (duplicateBinding.action == newBinding.action)
            {
                continue;
            }
            if (duplicateBinding.action != newBinding.action && (duplicateBinding.effectivePath == newBinding.effectivePath 
                || (duplicateBinding.effectivePath.Contains("leftTrigger") && newBinding.effectivePath.Contains("leftTrigger"))
                || (duplicateBinding.effectivePath.Contains("rightTrigger") && newBinding.effectivePath.Contains("rightTrigger"))))
            {
                //set gameobject active
                //activate prompt if player wants to switch the bindings
                rebindWarning.SetActive(true);
                actionToRebind.RemoveBindingOverride(bindingIndex);
                actionToRebind.ApplyBindingOverride(previousBinding);
                SwapBindingsPrompt(rebindWarning, previousBinding, duplicateBinding);
                instance.StartCoroutine(DelayInactivation(2f, rebindWarning, rebind));
                Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                return true;
            }
            //Check for duplicate composite bindings
            if (allCompositeParts)
            {
                for (int i = 0; i <= bindingIndex; i++)
                {
                    if (actionToRebind.actionMap.bindings[i].effectivePath == newBinding.effectivePath)
                    {
                        //SwapBindingPrompt();
                        instance.StartCoroutine(DelayInactivation(2f, rebindWarning, rebind));
                        Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private static void SwapBindingsPrompt(GameObject promptWindow, InputBinding bindingToSwap, InputBinding duplicateFound)
    {
        promptWindow.SetActive(true);
        InputAction actionToSwap = inputActions.asset.FindAction(bindingToSwap.action);
        InputAction duplicateAction = inputActions.asset.FindAction(duplicateFound.action);

        Debug.Log("ActionsToSwap: " + actionToSwap.name + " & " + duplicateAction.name);
        Debug.Log("BindingsToSwap: " + bindingToSwap.name + " & " + duplicateFound.name);

        InputBinding temp = bindingToSwap;
        //if (swap)
        //{
        actionToSwap.ApplyBindingOverride(duplicateFound);
        duplicateAction.ApplyBindingOverride(bindingToSwap);
        //}
    }

    public static void OnOkay(GameObject promptWindow)
    {
        promptWindow.SetActive(false);
        //swap = true;
    }

    public static void OnCancel(GameObject promptWindow)
    {
        promptWindow.SetActive(false);
    }

    private static IEnumerator DelayInactivation(float waitTime, GameObject rebindWarning, InputActionRebindingExtensions.RebindingOperation rebind)
    {
        Debug.Log("Started");
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + waitTime)
        {
            yield return null;
        }
        rebindWarning.SetActive(false);
        Debug.Log("setactivefalse");
        //rebind.Cancel();
    }

    public static string GetBindingPath(InputAction action, int bindingIndex)
    {
        string path = action.bindings[bindingIndex].effectivePath;
        int pathNameIndex = path.IndexOf('/') + 1;
        if (path.Equals("<Mouse>/leftButton") || path.Equals("<Mouse>/middleButton") || path.Equals("<Mouse>/rightButton"))
        {
            if (path.Equals("<Mouse>/leftButton"))
            {
                path = "mouse 0";
            }
            else if (path.Equals("<Mouse>/middleButton"))
            {
                path = "mouse 1";
            }
            else if (path.Equals("<Mouse>/rightButton"))
            {
                path = "mouse 2";
            }
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/buttonWest"))
        {
            path = "joystick button 0";
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().CheckControlUsed().Contains("XInputControllerWindows"))
            {
                path = "joystick button 2";
            }
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/buttonSouth"))
        {
            path = "joystick button 1";
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().CheckControlUsed().Contains("XInputControllerWindows"))
            {
                path = "joystick button 0";
            }
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/buttonNorth"))
        {
            path = "joystick button 3";
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/buttonEast"))
        {
            path = "joystick button 1";
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/leftShoulder"))
        {
            path = "joystick button 4";
            pathNameIndex = 0;
        }

        if(path.Equals("<Gamepad>/rightShoulder"))
        {
            path = "joystick button 5";
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/leftTrigger"))
        {
            path = "joystick button 6";
            pathNameIndex = 0;
        }

        if (path.Equals("<Gamepad>/rightTrigger"))
        {
            path = "joystick button 7";
            pathNameIndex = 0;
        }

        return path.Substring(pathNameIndex);
    }

    public static KeyCode GetGamepadButton(string buttonName)
    {
        KeyCode gamepadButton = 0;
        if (buttonName.Equals("X"))
        {
            gamepadButton = KeyCode.JoystickButton0;
        }
        if (buttonName.Equals("A"))
        {
            gamepadButton = KeyCode.Joystick1Button1;
        }
        if (buttonName.Equals("B"))
        {
            gamepadButton = KeyCode.Joystick1Button2;
        }
        if (buttonName.Equals("Y"))
        {
            gamepadButton = KeyCode.Joystick1Button3;
        }
        if (buttonName.Equals("LeftShoulder"))
        {
            gamepadButton = KeyCode.Joystick1Button4;
        }
        if (buttonName.Equals("RightShoulder"))
        {
            gamepadButton = KeyCode.Joystick1Button5;
        }
        if (buttonName.Equals("LeftTrigger"))
        {
            gamepadButton = KeyCode.Joystick1Button6;
        }
        if (buttonName.Equals("RightTrigger"))
        {
            gamepadButton = KeyCode.Joystick1Button7;
        }
        if (buttonName.Equals("Start"))
        {
            gamepadButton = KeyCode.Joystick1Button9;
        }

        return gamepadButton;
    }
}
