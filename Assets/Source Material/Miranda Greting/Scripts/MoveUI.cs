//Main Author Miranda Greting
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class MoveUI : MonoBehaviour
{
    [SerializeField] private RectTransform movableObject; //HUD Gameobject goes here
    [SerializeField] private GameObject UIMenu; //AjustUI/HUD goes here
    [SerializeField] private GameObject interactTutorial; //InteractionTutorial gameobject

    private bool active = true;

    private GameObject highlightParticles;
    private Vector3 originalScale;

    private static Vector2 startPos;
    private Vector2[] startPositions;
    private Vector2 currentPos;
    private List<Vector2> previousPositions;
    [SerializeField] private GameObject[] editableObjects; // all individual UI components go here; healthbar, potions, manabar, (keys) - in this order!!

    [SerializeField] private Slider scaleSlider;
    [SerializeField] private Image handleImage; //for changing color of slider from script
    [SerializeField] private Color selectedColor;  //to change color when slider is 'hovered over/selected' with keyboard/gamepad
    [SerializeField] private Color pressedColor; //to change color when slider is dragged with keyboard/gamepad
    [SerializeField] private Color defaultColor; //to change back to original color
    private ScaleManager scaleScript;
    private float previousScaleValue;

    [SerializeField] private Toggle individualEditToggle;
    private bool individualEditMode = false;
    private bool prototypeActivated = false;

    private GameObject markedObject;

    [SerializeField] private Button resetMarkedObject; //if individual edit mode is on, this button.SetActive(true), otherwise false
    [SerializeField] private Button resetAllObjects;
    [SerializeField] private Button[] anchorButtons;

    private Vector2[] anchorOffsets;
    private Vector3[] originalScales;
    private float[] currentScaleSlideValues;
    private GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    private bool fromSript = false;

    private PlayerInputs inputActions;
    private Vector2 movementInput;
    private string controlUsed = "Keyboard";
    private string gamepadTypeButton = "joystick button 1"; // if playstation gamepad is used, otherwise changed later in script
    private bool pressed;
    private bool scalePressed;
    private float cooldown = 0;
    private float selectedCooldown = 0;
    private float timeWhenSelected;
    private GameObject lastSelectedElement;
    private GameObject lastSelectedGameObject;
    private bool selectionChange = false;
    private bool canDeselect;

    void OnEnable()
    {
        //InactivatePrototype(false);
        foreach (GameObject gameObject in editableObjects)
        {
            movableObject.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
        //Debug.Log("SavedScaleOnEnable: " + PlayerPrefs.GetString(movableObject.name + "Scale"));
    }
    void OnDisable()
    {
        //InactivatePrototype(true);
        SaveParentScale(movableObject.localScale);
        SaveUIScale();
        PlayerPrefs.Save();
        //Debug.Log("SavedScaleOnDisable: " + PlayerPrefs.GetString(movableObject.name + "Scale"));

    }

    void Start()
    {
        originalScale = movableObject.localScale;
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);

        if (PlayerPrefs.GetString(movableObject.name + "Scale") != null)
        {
            //Debug.Log("NotNull");
            scaleSlider.value = 1;
        }
        scaleScript = scaleSlider.GetComponent<ScaleManager>();

        scaleSlider.onValueChanged.AddListener(delegate { ScaleUI(); });

        anchorOffsets = new Vector2[editableObjects.Length + 1];
        anchorOffsets[0] = new Vector2(movableObject.anchoredPosition.x, movableObject.anchoredPosition.y);
        startPositions = new Vector2[editableObjects.Length];
        originalScales = new Vector3[editableObjects.Length + 2];
        currentScaleSlideValues = new float[editableObjects.Length + 2];
        previousScaleValue = scaleSlider.value;
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = editableObjects[i].GetComponent<RectTransform>();
            anchorOffsets[i + 1] = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            startPositions[i] = rectTransform.anchoredPosition;
            originalScales[i] = rectTransform.localScale;
            currentScaleSlideValues[i] = scaleSlider.value;
            if (i != 2)
            {
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        originalScales[editableObjects.Length] = editableObjects[0].transform.GetChild(1).localScale;
        originalScales[editableObjects.Length + 1] = editableObjects[1].transform.GetChild(1).localScale;
        individualEditMode = false;
        if (PlayerPrefs.GetString("Toggle").Equals(null)) //checks if status of editmodetoggle has been saved
        {
            individualEditToggle.isOn = false;
            lastSelectedElement = editableObjects[0];
        }
        else if (PlayerPrefs.GetString("Toggle").Equals("On"))
        {
            lastSelectedElement = editableObjects[0];
        }
        else
        {
            lastSelectedElement = editableObjects[2];
        }
        movableObject.GetChild(0).gameObject.SetActive(false); //inactivates 'highlighted particles'
        movableObject.GetChild(1).gameObject.SetActive(false); //inactivates 'selected particles'
        inputActions = InputManager.inputActions;
        inputActions.WindlessLand.Move.performed += MovePerformed;
        //Debug.Log(movableObject.anchoredPosition);
        LoadUI();
        individualEditToggle.onValueChanged.AddListener((value) => { SaveToggleStatus(); });
        //Debug.Log("After Save/Load: " + movableObject.anchoredPosition);

        UIMenu.SetActive(false);
        InactivatePrototype(true);
    }

    public void ActivateTutorial(bool activate)
    {
        interactTutorial.SetActive(activate);
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>(); //saves which direction character is moving in
        controlUsed = ctx.control.path;
        if (controlUsed.Contains("XInputControllerWindows"))
        {
            gamepadTypeButton = "joystick button 0";
        }
        else if (controlUsed.Contains("Gamepad"))
        {
            gamepadTypeButton = "joystick button 1";
        }
    }

    public string GetGamepadButton()
    {
        return gamepadTypeButton;
    }

    //Save function in procress
    public void SaveUIPositions()
    {
        RectTransform rect = movableObject.GetComponent<RectTransform>();
        PlayerPrefs.SetString(movableObject.name + "Position", rect.anchoredPosition.x + "&" + rect.anchoredPosition.y); //saves position of parent object
        for (int i = 0; i < editableObjects.Length; i++)
        {
            rect = editableObjects[i].GetComponent<RectTransform>();
            PlayerPrefs.SetString(editableObjects[i].name + "Position", rect.anchoredPosition.x + "&" + rect.anchoredPosition.y); //saves position of each individual UI object
        }
    }

    public void SaveUIScale()
    {
        RectTransform rect = movableObject;
        //PlayerPrefs.SetString(movableObject.name + "Scale", movableObject.localScale.x + "&" + movableObject.localScale.y); //saves scale of parent object
        for (int i = 0; i < editableObjects.Length; i++)
        {
            rect = editableObjects[i].GetComponent<RectTransform>();
            PlayerPrefs.SetString(editableObjects[i].name + "Scale", rect.localScale.x + "&" + rect.localScale.y); //saves scale of each individual UI object
        }
    }

    public void SaveParentScale(Vector3 scale)
    {
        PlayerPrefs.SetString(movableObject.name + "Scale", scale.x + "&" + scale.y);
        //Debug.Log(movableObject.name + " Saved Scale = " + scale);
        //Debug.Log("Playerprefs save: " + PlayerPrefs.GetString(movableObject.name + "Scale"));
    }

    public void SaveToggleStatus()
    {
        if (individualEditToggle.isOn) //saves toggle status
        {
            PlayerPrefs.SetString("Toggle", "On");
        }
        else
        {
            PlayerPrefs.SetString("Toggle", "Off");
        }
    }

    public void LoadUI()
    {
        if (PlayerPrefs.GetString("Toggle").Equals("On"))
        {
            //individualEditToggle.isOn = true;
        }
        else
        {
            //individualEditToggle.isOn = false;
        }
        //PlayerPrefs.DeleteKey(movableObject + "Scale");
        LoadPositions(movableObject.gameObject, startPos, "Position");
        LoadPositions(movableObject.gameObject, originalScale, "Scale");
        //Debug.Log(originalScale + " = originalScale");
        for (int i = 0; i < editableObjects.Length; i++)
        {
            LoadPositions(editableObjects[i], startPositions[i], "Position");
            LoadPositions(editableObjects[i], originalScales[i], "Scale");
            //PlayerPrefs.DeleteKey(editableObjects[i] + "Scale");
        }
    }

    private void LoadPositions(GameObject savedObject, Vector2 startPos, string type)
    {
        string scaleOrPos = PlayerPrefs.GetString(savedObject.name + type);
        if (savedObject == movableObject.gameObject && type.Equals("Scale"))
        {
            //Debug.Log(savedObject.name + "Saved playerprefs scale" + PlayerPrefs.GetString(savedObject.name + type));
        }

        if (scaleOrPos.IndexOf('&') == -1)
        {
            if (type.Equals("Position"))
            {
                scaleOrPos = startPos.x + "&" + startPos.y;
            }
            else if (type.Equals("Scale"))
            {
                if (savedObject == this.movableObject)
                {
                    scaleOrPos = originalScale.x + "&" + originalScale.y;
                }
                else
                {
                    for (int i = 0; i < editableObjects.Length; i++)
                    {
                        if (editableObjects[i] == savedObject)
                        {
                            scaleOrPos = originalScales[i].x + "&" + originalScales[i].y;
                        }
                    }
                }
            }
        }
        if (scaleOrPos != null)
        {
            string newString = scaleOrPos;

            for (int i = 0; i < scaleOrPos.Length; i++) //replaces all ',' (e.g if position.x = 2,5263) with '.' to fit with float.Parse further down
            {
                if (scaleOrPos[i].Equals(','))
                {
                    newString = scaleOrPos.Substring(0, i) + '.' + scaleOrPos.Substring(i + 1);
                    scaleOrPos = newString;
                }
            }

            string scaleOrPosX = scaleOrPos.Substring(0, scaleOrPos.IndexOf('&'));
            string scaleOrPosY = scaleOrPos.Substring(scaleOrPos.IndexOf('&') + 1);
            int xMultiplier;
            int yMultiplier;

            try
            {
                int.Parse(scaleOrPosX.Substring(0, 1));

                xMultiplier = 1;
            }
            catch (FormatException) //removes '-' from string if it's the first char (preventing FormatException error), and sets multiplier to -1
            {
                Debug.Log(savedObject.name + scaleOrPosX.Substring(0, 1));
                scaleOrPosX = scaleOrPosX.Substring(1);
                xMultiplier = -1;
            }

            try
            {
                int.Parse(scaleOrPosY.Substring(0, 1));
                yMultiplier = 1;
            }
            catch (FormatException)  //removes '-' from string if it's the first char (preventing FormatException error), and sets multiplier to -1
            {
                scaleOrPosY = scaleOrPosY.Substring(1);
                yMultiplier = -1;
            }
            //Debug.Log("loaded x value: " + type + scaleOrPosX);
            //Debug.Log("loaded y value: " + type + scaleOrPosY);
            Vector2 savedData = new Vector2(startPos.x, startPos.y);

            try
            {
                savedData.x = float.Parse(scaleOrPosX, System.Globalization.CultureInfo.InvariantCulture) * xMultiplier; //transforms the X-value of saved string into a Vector2
            }
            catch (FormatException e)
            {
                //Debug.Log(e + " pos.x");
            }
            try
            {
                savedData.y = float.Parse(scaleOrPosY, System.Globalization.CultureInfo.InvariantCulture) * yMultiplier; //transforms the Y-value of saved string into a Vector2
            }
            catch (FormatException e)
            {
                //Debug.Log(e + " pos.y");
            }
            if (type.Equals("Position"))
            {
                savedObject.GetComponent<RectTransform>().anchoredPosition = savedData; //sets anchoredPosition to saved position
            }
            else if (type.Equals("Scale"))
            {
                try
                {
                    //Debug.Log(savedObject.name + " loaded scale = " + savedData);
                    savedObject.GetComponent<RectTransform>().localScale = new Vector3(savedData.x, savedData.y, 1); //sets current scale to save data
                }
                catch (Exception)
                {
                    Debug.Log("Did not work");
                }
            }

        }
        else
        {
            Debug.Log("No saved data found!!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        ScalesliderManager();
        if (cooldown > 0)
        {
            cooldown -= Time.unscaledTime;
        }

        if (UIMenu.activeInHierarchy == false && active)
        {
            InactivatePrototype(true);
            active = false;
        }
        else if (UIMenu.activeInHierarchy == true && !active)
        {
            InactivatePrototype(false);
            active = true;
        }


        if (UIMenu.activeInHierarchy) //following code is for movement of UI elements through keyboard or gamepad, only runs if UIMenu is active
        {
            //Move with keyboard or gamepad
            if (movementInput.x != 0 || movementInput.y != 0)
            {
                if (pressed)
                {
                    if (individualEditToggle.isOn)
                    {
                        GameObject pressedObject = GetSelectedObject();
                        if (pressedObject != null)
                        {
                            EventSystem.current.SetSelectedGameObject(pressedObject);
                            pressedObject.GetComponent<RectTransform>().anchoredPosition += (movementInput * 9);// * Time.deltaTime * moveSpeed; //moves selected ui element along with keyboard or gamepad input
                            MoveUIElement(pressedObject); // prevents gameobject from being moved outside screen
                        }
                    }
                    else
                    {
                        movableObject.GetComponent<RectTransform>().anchoredPosition += movementInput;
                    }
                }
            }



            //Selects the right gameobject when using keyboard or gamepad
            if (!selectionChange) //checks that another gameobject haven't been selected within cooldown timer
            {
                //when cooldown reaches 0 another gameobject can be selected again
                if (EventSystem.current.currentSelectedGameObject == individualEditToggle.gameObject) //checks if editToggle is selected
                {
                    StartCoroutine(DeselectCooldown(0.5f));
                    if (movementInput.x == 0 && movementInput.y == 0)
                    {
                        canDeselect = true;
                    }

                    if (canDeselect)
                    {
                        if (movementInput.x < 0 || movementInput.y > 0)
                        {
                            UnselectAll();  //makes all elements unselected, a movable element can be selected by pressing enter or interact when highlighted
                            Debug.Log("Manabar Selected");
                            EventSystem.current.SetSelectedGameObject(editableObjects[2]); //selects Manabar
                            selectionChange = true; //bool for preventing code being run twice (else the selection can jump further than one object if button is held down for longer than one frame)
                            canDeselect = false;
                        }
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject); //selects Manabar

                    }
                }

                else if (EventSystem.current.currentSelectedGameObject == editableObjects[2] && !selectionChange) //checks if Manabar is selected
                {
                    if (!editableObjects[2].GetComponent<MoveObject>().GetSelected() && !movableObject.GetChild(1).gameObject.activeInHierarchy) //checks that object isn't currently movable/'selected' by pressing enter once
                    {
                        if (movementInput.x > 0 || movementInput.y < 0)
                        {
                            EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject); //selects EditToggle
                            selectionChange = true;
                        }

                        if (movementInput.y > 0)
                        {
                            if (individualEditToggle.isOn)
                            {
                                Debug.Log("Healthbar Selected");
                                EventSystem.current.SetSelectedGameObject(editableObjects[0]); //selects Healthbar
                                selectionChange = true;
                            }
                        }
                    }
                }

                else if (EventSystem.current.currentSelectedGameObject == editableObjects[0] && individualEditToggle.isOn && !selectionChange) //checks if Healthbar is selected
                {
                    if (!editableObjects[0].GetComponent<MoveObject>().GetSelected()) //checks that object isn't currently movable/'selected' by pressing enter once
                    {
                        if (movementInput.x > 0)
                        {
                            Debug.Log("Potion Selected");
                            EventSystem.current.SetSelectedGameObject(editableObjects[1]); //selects Health Potions
                            selectionChange = true;
                        }

                        if (movementInput.x < 0)
                        {
                            EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject); //selects EditModeToggle
                            selectionChange = true;
                        }
                        if (movementInput.y < 0)
                        {
                            Debug.Log("Manabar Selected");
                            EventSystem.current.SetSelectedGameObject(editableObjects[2]); //selects Manabar
                            selectionChange = true;
                        }
                    }
                }

                else if (EventSystem.current.currentSelectedGameObject == editableObjects[1] && individualEditToggle.isOn && !selectionChange) //checks if HealthPotion is selected
                {
                    if (!editableObjects[1].GetComponent<MoveObject>().GetSelected())
                    {
                        if (movementInput.x < 0)
                        {
                            Debug.Log("HealthBar Selected");
                            EventSystem.current.SetSelectedGameObject(editableObjects[0]); //selects Healthbar
                            selectionChange = true;
                        }
                        else if (movementInput.x > 0)
                        {
                            Debug.Log("EditToggle Selected");
                            EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject); //selects EditModeToggle
                            selectionChange = true;
                        }

                        if (movementInput.y < 0)
                        {
                            Debug.Log("Manabar Selected");

                            EventSystem.current.SetSelectedGameObject(editableObjects[2]); //selects Manabar
                            selectionChange = true;
                        }
                    }
                }
            }

            if (selectionChange)
            {
                if (EventSystem.current.currentSelectedGameObject != individualEditToggle.gameObject && EventSystem.current.currentSelectedGameObject != scaleSlider.gameObject)
                {
                    for (int i = 0; i < editableObjects.Length; i++)
                    {
                        if (editableObjects[i] == EventSystem.current.currentSelectedGameObject)
                        {
                            lastSelectedElement = EventSystem.current.currentSelectedGameObject; //saves the last selected UI element (so that it turns selected again so other functions (scaling & anchoring) can still be performed with keyboard & gamepad)
                        }
                    }
                }
                //timeWhenSelected = Time.realtimeSinceStartup;
                //selectedCooldown = timeWhenSelected; //puts a 1 second cooldown on switching selected element, so code can't be run twice
                StartCoroutine(SelectionCooldown(0.5f));
                //Debug.Log("SelectionChangeCooldownStart!!!");
            }

            if (EventSystem.current.currentSelectedGameObject == movableObject.gameObject || EventSystem.current.currentSelectedGameObject == editableObjects[0]
                || EventSystem.current.currentSelectedGameObject == editableObjects[1] || EventSystem.current.currentSelectedGameObject == editableObjects[2]) //only runs code if EventSystem's selected gameobject is an editable UI element
            {
                if (individualEditToggle.isOn)
                {
                    if (EventSystem.current.currentSelectedGameObject != individualEditToggle)
                    {
                        lastSelectedElement = EventSystem.current.currentSelectedGameObject;
                    }
                    for (int i = 0; i < editableObjects.Length; i++)
                    {
                        if (!pressed && editableObjects[i].transform.GetChild(0).gameObject.activeInHierarchy && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton)))
                        {
                            pressed = true;
                            ChangeSelectedObject(editableObjects[i]);
                            Debug.Log("Object pressed, should move!!");
                            ActivateTutorial(true);
                        }
                        else if (CheckSelectedObject(editableObjects[i]) && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton)))
                        {
                            pressed = false;
                            //ChangeSelectedObject(editableObjects[2]);
                            ActivateTutorial(false);
                            editableObjects[i].GetComponent<MoveObject>().SetSelected(false);
                            Debug.Log("Object not 'pressed'");
                            SaveUIPositions();
                        }
                    }
                }
                else
                {
                    if (!pressed && movableObject.GetChild(0).gameObject.activeInHierarchy && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton)))
                    {
                        pressed = true;
                        movableObject.GetChild(1).gameObject.SetActive(true);
                        Debug.Log("Object pressed, should move!!");
                        ActivateTutorial(true);
                    }
                    else if (pressed && movableObject.GetChild(1).gameObject.activeInHierarchy && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton)))
                    {
                        pressed = false;
                        //ChangeSelectedObject(editableObjects[2]);
                        ActivateTutorial(false);
                        editableObjects[2].GetComponent<MoveObject>().SetSelected(false);
                        movableObject.GetChild(1).gameObject.SetActive(false);
                        Debug.Log("Object not 'pressed'");
                        SaveUIPositions();
                    }
                }
            }
            else
            {
                if (individualEditToggle.isOn)
                {
                    foreach(GameObject editableObject in editableObjects)
                    {
                        if (!editableObject.GetComponent<MoveObject>().GetMouseDrag())
                        {
                            ChangeSelectedObject(lastSelectedElement);
                        }
                    }
                }
                else
                {
                    movableObject.GetChild(1).gameObject.SetActive(true);
                }
            }
            if (canDeselect)
            {
                if (EventSystem.current.currentSelectedGameObject != lastSelectedGameObject)
                {
                    canDeselect = false;
                }
            }
        }
    }

    public void SetLastSelectedElement(GameObject element)
    {
        lastSelectedElement = element;
    }

    private void ScalesliderManager() //code for making scale slider work as intended for gamepad and keyboard (not moved immediately, but rather after pressing enter to interact, and then enter again to stop & deselect it)
    {
        if (EventSystem.current.currentSelectedGameObject == scaleSlider.gameObject)
        {
            if (!scaleScript.GetMouseDrag() && !Input.GetKey(KeyCode.Mouse0))
            {
                StartCoroutine(DeselectCooldown(0.5f));
                ActivateTutorial(true);

                if (!scalePressed && canDeselect)
                {
                    if (movementInput.x < 0 && !selectionChange)
                    {
                        if (!individualEditToggle.isOn)
                        {
                            EventSystem.current.SetSelectedGameObject(anchorButtons[2].gameObject);
                        }
                        else
                        {
                            EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject);
                        }
                        ActivateTutorial(false);
                        //canDeselect = false;
                        selectionChange = true;
                        handleImage.color = defaultColor;
                    }
                    else if (movementInput.x > 0 && !selectionChange)
                    {
                        if (!individualEditToggle.isOn)
                        {
                            EventSystem.current.SetSelectedGameObject(anchorButtons[0].gameObject);
                        }
                        else
                        {
                            EventSystem.current.SetSelectedGameObject(individualEditToggle.gameObject);
                        }
                        ActivateTutorial(false);
                        //canDeselect = false;
                        selectionChange = true;
                        handleImage.color = defaultColor;
                    }
                }

                if (scalePressed)
                {
                    if (movementInput.x != 0 || movementInput.y != 0)
                    {
                        EventSystem.current.SetSelectedGameObject(scaleSlider.gameObject);
                        //Debug.Log("ShouldNotSwitch");
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton))
                {
                    if (!scalePressed)
                    {
                        scalePressed = true;
                        handleImage.color = pressedColor;
                        Debug.Log("Scalepressed: " + scalePressed);
                    }
                    else if (scalePressed)
                    {
                        scalePressed = false;
                        handleImage.color = selectedColor;
                        Debug.Log("ScalePressed: " + scalePressed);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ActivateTutorial(false);
                scalePressed = false;
                EventSystem.current.SetSelectedGameObject(null);
                handleImage.color = defaultColor;
            }
        }
        else if (EventSystem.current.currentSelectedGameObject == anchorButtons[2].gameObject || EventSystem.current.currentSelectedGameObject == anchorButtons[4].gameObject && !selectionChange)
        {
            handleImage.color = defaultColor;
            if (movementInput.x == 0 && movementInput.y == 0)
            {
                canDeselect = true;
            }
            else
            {
                canDeselect = false;
            }
            if (movementInput.x > 0 && movementInput.x > movementInput.y && canDeselect)
            {
                EventSystem.current.SetSelectedGameObject(scaleSlider.gameObject); //selects scaleslider gameobject
                selectionChange = true;
                Debug.Log("ScaleSlider Selected " + " CanDeselect = " + canDeselect);
                canDeselect = false;
            }
        }
        else
        {
            handleImage.color = defaultColor;
            scalePressed = false;
            ActivateTutorial(false);
        }
    }

    private IEnumerator SelectionCooldown(float cooldown)
    {
        while (movementInput.x != 0 || movementInput.y != 0)
        {
            yield return null;
        }
        //yield return new WaitForSecondsRealtime(cooldown);
        selectionChange = false;
        //canDeselect = true;
        //yield return 0;
    }

    private IEnumerator DeselectCooldown(float cooldown)
    {
        while (movementInput.x != 0 || movementInput.y != 0)
        {
            yield return null;
        }
        canDeselect = true;
        lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
    }

    public void ActivatePlayerUI(bool active)
    {
        foreach (GameObject editableObject in editableObjects)
        {
            if (active)
            {
                editableObject.SetActive(true);
            }
            else
            {
                editableObject.SetActive(false);
            }
        }
    }

    public void InactivatePrototype(bool setInactive)
    {
        if (setInactive)
        {
            individualEditToggle.isOn = false;
            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                editableObjects[i].GetComponent<MoveObject>().enabled = false; //inactivates script that allows objects to be moved
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                editableObjects[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            movableObject.GetChild(0).gameObject.SetActive(false);
            movableObject.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetString("Toggle").Equals("On")) //activates 
            {
                editableObjects[0].transform.GetChild(0).gameObject.SetActive(false);
                editableObjects[0].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                movableObject.GetChild(0).gameObject.SetActive(false);
                movableObject.GetChild(1).gameObject.SetActive(true);
            }

            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                editableObjects[i].GetComponent<MoveObject>().enabled = true; //activates script that allows objects to be moved (so that it's only possible from ui menu screen)
                editableObjects[i].SetActive(true);
            }

        }
    }

    public GameObject[] GetEditableObjects()
    {
        return editableObjects;
    }

    public void ChangeSelectedObject(GameObject selection)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            if (editableObjects[i] != selection && objectScript.GetSelected())
            {
                objectScript.SetSelected(false);
                //scaleSlider.value = currentScaleSlideValues[i];
            }
            selection.GetComponent<MoveObject>().SetSelected(true);
        }
    }

    public bool CheckSelectedObject(GameObject selection)
    {
        for (int i = 0; i < editableObjects.Length; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            if (editableObjects[i] == selection && objectScript.GetSelected())
            {
                return true;
            }
        }
        return false;
    }

    public GameObject GetSelectedObject()
    {
        if (individualEditToggle.isOn)
        {
            for (int i = 0; i < editableObjects.Length; i++)
            {
                MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
                if (objectScript.GetSelected())
                {

                    return editableObjects[i];
                }
            }
        }
        return null;
    }

    public void UnselectAll()
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            objectScript.SetSelected(false);
            objectScript.SetHighlighted(false);
            editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            editableObjects[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        movableObject.GetChild(0).gameObject.SetActive(false);
        movableObject.GetChild(1).gameObject.SetActive(false);
    }

    public void ToggleEditModes()
    {
        if (individualEditToggle.isOn)
        {
            scaleSlider.transform.parent.parent.GetChild(4).gameObject.SetActive(false);
            //resetMarkedObject.gameObject.transform.parent.gameObject.SetActive(true);
            movableObject.GetChild(1).gameObject.SetActive(false);
            ChangeSelectedObject(lastSelectedElement);
        }
        else
        {
            scaleSlider.transform.parent.parent.GetChild(4).gameObject.SetActive(true);
            //resetMarkedObject.gameObject.transform.parent.gameObject.SetActive(false);
            UnselectAll();
            movableObject.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeHighlightedObject(GameObject highlighted)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            if (editableObjects[i] != highlighted && objectScript.GetHighlighted())
            {
                objectScript.SetHighlighted(false);
            }
            highlighted.GetComponent<MoveObject>().SetHighlighted(true);
        }
    }

    public void ResetTransform(bool onlyMarkedObject)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            if (!onlyMarkedObject || (onlyMarkedObject && !individualEditToggle.isOn))
            {
                    fromSript = true;
                    ChangeAnchoredPos("UpLeft");
                    fromSript = false;
                    movableObject.anchoredPosition = startPos;
                    editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
                    editableObjects[i].transform.localScale = originalScales[i];
                    movableObject.localScale = originalScale;
            }
            else
            {
                if (editableObjects[i].GetComponent<MoveObject>().GetSelected())
                {
                    editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
                    editableObjects[i].transform.localScale = originalScales[i];
                }
            }
        }
        SaveUIPositions();
        SaveParentScale(originalScale);
        SaveUIScale();
    }

    public void ChangeEditMode()
    {
        if (individualEditToggle.isOn)
        {
            individualEditMode = true;
        }
        else
        {
            individualEditMode = false;
        }
    }

    public bool CheckEditMode()
    {
        return individualEditMode;
    }
    public GameObject GetParentObject()
    {
        return editableObjects[0];
    }

    public void MoveUIElement(GameObject movedObject)
    {
        RectTransform rectTransform = movedObject.GetComponent<RectTransform>();
        RectTransform canvasRect = UIMenu.transform.parent.GetComponent<RectTransform>();

        float minX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * -rectTransform.anchorMin.x;
        float maxX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * rectTransform.anchorMax.x;
        float minY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * -rectTransform.anchorMin.y;
        float maxY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * rectTransform.anchorMin.y;

        Vector3[] cornerPositions = new Vector3[4];
        canvasRect.GetWorldCorners(cornerPositions);
        Vector3 canvasLowLeft = cornerPositions[0], canvasTopRight = cornerPositions[2];
        var containerSize = canvasTopRight - canvasLowLeft;
        rectTransform.GetWorldCorners(cornerPositions);
        Vector3 movedObjectLowLeft = cornerPositions[0], movedObjectTopRight = cornerPositions[2];
        var movableSize = movedObjectTopRight - movedObjectLowLeft;

        var position = rectTransform.position;
        Vector3 deltaLowLeft = position - movedObjectLowLeft, deltaTopRight = movedObjectTopRight - position;
        position.x = movableSize.x < containerSize.x
            ? Mathf.Clamp(position.x, canvasLowLeft.x + deltaLowLeft.x, canvasTopRight.x - deltaTopRight.x)
            : Mathf.Clamp(position.x, canvasTopRight.x - deltaTopRight.x, canvasLowLeft.x + deltaLowLeft.x);
        position.y = movableSize.y < containerSize.y
            ? Mathf.Clamp(position.y, canvasLowLeft.y + deltaLowLeft.y + 10, canvasTopRight.y - deltaTopRight.y)
            : Mathf.Clamp(position.y, canvasTopRight.y - deltaTopRight.y, canvasLowLeft.y + deltaLowLeft.y + 10);
        rectTransform.position = position;
    }

    private void ScaleUI()
    {
        if (scalePressed || scaleScript.GetMouseDrag())
        {
            Debug.Log("Scalepressed: " + scalePressed);
            Debug.Log("Mousedrag: " + scaleScript.GetMouseDrag());
            GameObject scaledObject = movableObject.gameObject;
            if (individualEditToggle.isOn)
            {
                for (int i = 0; i <= editableObjects.Length - 1; i++)
                {
                    if (editableObjects[i].GetComponent<MoveObject>().GetSelected() && (scalePressed || scaleScript.GetMouseDrag()))
                    {

                        //editableObjects[i].transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
                        editableObjects[i].transform.localScale = new Vector3(originalScales[i].x * scaleSlider.value, originalScales[i].y * scaleSlider.value, 1);
                        previousScaleValue = scaleSlider.value;
                        //editableObjects[i].transform.GetChild(0).localScale = new Vector3(originalScales[i] * new Vector3(scaleSlider.value, scaleSlider.value, 1);
                        scaledObject = editableObjects[i];
                        MoveUIElement(editableObjects[i]);
                        SaveUIScale();

                        currentScaleSlideValues[i] = scaleScript.GetValueAtDeselect();
                    }
                }
            }
            else if(scalePressed || scaleScript.GetMouseDrag())
            {
                Vector3 newScale = new Vector3(originalScale.x * scaleSlider.value, originalScale.y * scaleSlider.value, 1);
                movableObject.localScale = newScale;
                MoveUIElement(movableObject.gameObject);
                SaveParentScale(newScale);
                previousScaleValue = scaleSlider.value;
            }
            RectTransform rectTransform = scaledObject.GetComponent<RectTransform>();
            if (scaledObject == movableObject)
            {
                Debug.Log("ScaleShouldReset");
                Vector2 anchorPos = rectTransform.anchoredPosition;
                float ypos = anchorPos.y;
                float xpos = anchorPos.x;
                xpos = Mathf.Clamp(xpos, 0, Screen.width - rectTransform.sizeDelta.x);
                ypos = Mathf.Clamp(ypos, 10, Screen.height - rectTransform.sizeDelta.y);
                rectTransform.anchoredPosition = new Vector2(xpos, ypos);
            }
        }
        else
        {
            scaleSlider.value = previousScaleValue;
            scaleSlider.value = previousScaleValue;
        }
    }

    public void ChangeAnchoredPos(string buttonPos)
    {
        RectTransform selectedObject = movableObject;
        int objectInList = 0;
        if (individualEditToggle.isOn && fromSript == false)
        {
            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                if (editableObjects[i].GetComponent<MoveObject>().GetSelected())
                {
                    selectedObject = editableObjects[i].GetComponent<RectTransform>();
                    objectInList = i + 1;
                }
            }
        }
        RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
        Vector2 rectMinMax = selectedObject.anchorMin;

        //unless i get individual anchoring to work (rectTransforms are a pain)
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
        }

        if (buttonPos.Equals("UpLeft"))
        {
            rectMinMax = new Vector2(0, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, anchorOffsets[objectInList].y);
        }
        else if (buttonPos.Equals("UpMid"))
        {
            rectMinMax = new Vector2(0.5f, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(0, anchorOffsets[objectInList].y);

        }
        else if (buttonPos.Equals("UpRight"))
        {
            rectMinMax = new Vector2(1, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, anchorOffsets[objectInList].y);

        }
        else if (buttonPos.Equals("MidLeft"))
        {
            rectMinMax = new Vector2(0, 0.5f);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, 0);
        }
        else if (buttonPos.Equals("MidRight"))
        {
            rectMinMax = new Vector2(1, 0.5f);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, 0);
        }
        else if (buttonPos.Equals("DownLeft"))
        {
            rectMinMax = new Vector2(0, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, -anchorOffsets[objectInList].y * 4);
        }
        else if (buttonPos.Equals("DownMid"))
        {
            rectMinMax = new Vector2(0.5f, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(0, -anchorOffsets[objectInList].y * 4);
        }
        else if (buttonPos.Equals("DownRight"))
        {
            rectMinMax = new Vector2(1, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, -anchorOffsets[objectInList].y * 4);
        }
    }
}
