//Main Author: Miranda Greting
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    //script attached to each UI object that can be moved

    private bool individualEditMode;
    private GameObject highlightingParticles;
    private GameObject selectedParticles;
    [SerializeField] private Toggle editModeToggle;
    [SerializeField] private GameObject parent;
    [SerializeField] private MoveUI uiManager;
    [SerializeField] private int moveSpeed = 2;
    private bool highlighted;
    private bool selected;
    private PlayerInputs inputActions;
    private Vector2 movementInput;
    private string controlUsed = "Keyboard";
    private string gamepadTypeButton = "joystick button 1"; // if playstation gamepad is used, otherwise changed later in script
    private bool pressed;
    private bool mouseDrag;

    private GameObject[] editableObjects;
    private GameObject manaBar;
    private GameObject healthBar;
    private GameObject potions;

    private float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        individualEditMode = false;
        highlightingParticles = gameObject.transform.GetChild(0).gameObject;
        selectedParticles = gameObject.transform.GetChild(1).gameObject;
        highlightingParticles.SetActive(false);
        selectedParticles.SetActive(false);
        uiManager.ActivateTutorial(false);
        inputActions = InputManager.inputActions;
        inputActions.WindlessLand.Move.performed += MovePerformed;

        editableObjects = uiManager.GetEditableObjects();
        manaBar = editableObjects[2];
        healthBar = editableObjects[0];
        potions = editableObjects[1];
    }

    public void InactivatePrototype()
    {

    }

    private void Update()
    {
        /*
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            if (pressed && selected)
            {
                if (editModeToggle.isOn)
                {
                    //EventSystem.current.SetSelectedGameObject(gameObject);
                    gameObject.GetComponent<RectTransform>().anchoredPosition += (movementInput * 9);// * Time.deltaTime * moveSpeed; //moves selected ui element along with keyboard or gamepad input
                    uiManager.MoveUIElement(gameObject); // prevents gameobject from being moved outside screen
                }
                else
                {
                    parent.GetComponent<RectTransform>().anchoredPosition += movementInput;
                }
            }
        }
        */

        /*
        if (cooldown > 0)
        {
            cooldown -= Time.unscaledTime;
        }
        */
        /*
        if (editModeToggle.isOn)
        {
            
            if (EventSystem.current.currentSelectedGameObject == editModeToggle.gameObject && cooldown <= 0)
            {
                if (editModeToggle.isOn && gameObject == manaBar && (movementInput.x < 0 || movementInput.y > 0))
                {
                    cooldown = 1;
                    EventSystem.current.SetSelectedGameObject(manaBar);
                }
            }
            if (EventSystem.current.currentSelectedGameObject == manaBar && cooldown <= 0)
            {
                if (!selected && gameObject.name.Contains("Manabar"))
                {
                    if ((movementInput.x > 0 || movementInput.y < 0))
                    {
                        cooldown = 1;
                        EventSystem.current.SetSelectedGameObject(editModeToggle.gameObject);
                    }
                    /*
                    if (movementInput.y > 0)
                    {
                        EventSystem.current.SetSelectedGameObject(healthBar);
                    }
                    
                }
            }
            */
            
            /*
            if (EventSystem.current.currentSelectedGameObject == healthBar && cooldown <= 0)
            {
                cooldown = 1;
                if (!selected && gameObject.name.Contains("HealthBar"))
                {
                    if (movementInput.x > 0 && movementInput.x > movementInput.y)
                    {
                        EventSystem.current.SetSelectedGameObject(potions);
                        Debug.Log("Potions selected!!");
                    }
                    if (movementInput.y != 0 && movementInput.y < movementInput.x)
                    {
                        EventSystem.current.SetSelectedGameObject(manaBar);
                    }
                    Debug.Log(EventSystem.current.currentSelectedGameObject.name + "selected");
                }
            }

            if (EventSystem.current.currentSelectedGameObject == potions && cooldown <= 0)
            {
                cooldown = 1;
                if (!selected)
                {
                    if (gameObject.name.Contains("Potion"))
                    {
                        if (movementInput.x > 0 && movementInput.x > movementInput.y)
                        {
                            EventSystem.current.SetSelectedGameObject(editModeToggle.gameObject);
                        }
                        if (movementInput.y < 0 && movementInput.y < movementInput.x)
                        {
                            EventSystem.current.SetSelectedGameObject(manaBar);
                        }
                        if (movementInput.x < 0)
                        {
                            EventSystem.current.SetSelectedGameObject(healthBar);
                           
                        }
                        Debug.Log(EventSystem.current.currentSelectedGameObject.name + "selected");
                    }
                }
            }
            
        }
            */

        /*
        if (!pressed && gameObject.name.Contains("Manabar") && highlightingParticles.activeInHierarchy && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return)) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton))
        {
            pressed = true;
            uiManager.ChangeSelectedObject(gameObject);
            Debug.Log(gameObject.name);
            Debug.Log("Manabar selected, should move!!");
            cooldown = 1;
            uiManager.ActivateTutorial(true);
        }
        else if (selected && cooldown <= 0 && (Input.GetKeyDown(KeyCode.Return)) || inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(gamepadTypeButton))
        {
            pressed = false;
            uiManager.ChangeSelectedObject(gameObject);
            uiManager.ActivateTutorial(false);
            selectedParticles.SetActive(false);
            selected = false;
            Debug.Log("Manabar Deselected");
            cooldown = 1;
        }
        */

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            uiManager.ChangeSelectedObject(gameObject);
        }
    }

    public bool GetSelected()
    {
        return selected;
    }

    public bool GetHighlighted()
    {
        return highlighted;
    }

    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
        if (isSelected)
        {
            if (selectedParticles.activeInHierarchy)
            {
                return;
            }
            selectedParticles.SetActive(true);
            //selected = true;
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //selected = false;
        }
    }

    public void SetHighlighted(bool isMarked)
    {
        highlighted = isMarked;
    }

    public bool GetMouseDrag()
    {
        return mouseDrag;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseDrag = true;
        if (editModeToggle.isOn)
        {
            highlightingParticles.SetActive(true);
            uiManager.ChangeSelectedObject(gameObject);
        }
        else if (!editModeToggle.isOn)
        {
            highlightingParticles.SetActive(false);
            parent.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            if (!gameObject.name.Equals("EditAll"))
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta; //moves ui gameobject along with mouse drag
                uiManager.MoveUIElement(gameObject); // prevents gameobject from being moved outside screen
            }
        }

        else if (!editModeToggle.isOn)
        {
            parent.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
            uiManager.MoveUIElement(parent);
            /*
            else
            {
                edited = false;
            }
            */
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mouseDrag = false;
        //currentPos = movableObject.anchoredPosition;
        //previousPositions.Add(currentPos);
        //Debug.Log("AddedPosCurrent");
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
            highlightingParticles.SetActive(false);
            uiManager.SetLastSelectedElement(this.gameObject);
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
        }
        uiManager.GetComponent<MoveUI>().SaveUIPositions();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
            highlightingParticles.SetActive(true);
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
            parent.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
            parent.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (individualEditMode)
        {
            highlightingParticles.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
            highlightingParticles.SetActive(true);
            parent.transform.GetChild(0).gameObject.SetActive(false);
            uiManager.ActivateTutorial(true);
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
            parent.transform.GetChild(0).gameObject.SetActive(true);
            uiManager.ActivateTutorial(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
            highlightingParticles.SetActive(false);

        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
            parent.transform.GetChild(0).gameObject.SetActive(false);
        }
        uiManager.ActivateTutorial(false);
        if (gameObject.name.Contains("Potion"))
        {
            Debug.Log("Potions Deselected!!");
        }
    }
}
