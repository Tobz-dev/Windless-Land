using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScaleManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, ISelectHandler, IDeselectHandler
{
    private bool selected;
    private float currentValue;

    private PlayerInputs inputActions;
    private Vector2 movementInput;
    private float previousValue;
    private Slider slider;
    private Button selectedLeft, selectedRight, selectedUp, selectedDown;
    Navigation customNav = new Navigation();

    private void OnEnable()
    {
        inputActions.WindlessLand.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    private void Awake()
    {
        inputActions = InputManager.inputActions;
        slider = gameObject.GetComponent<Slider>();
        previousValue = slider.value;
        selected = false;
        selectedLeft = slider.navigation.selectOnLeft.GetComponent<Button>();
        selectedRight = slider.navigation.selectOnRight.gameObject.GetComponent<Button>();
        selectedUp = slider.navigation.selectOnUp.gameObject.GetComponent<Button>();
        selectedDown = slider.navigation.selectOnDown.gameObject.GetComponent<Button>();
        customNav.mode = Navigation.Mode.Explicit;
        slider.navigation = customNav;
        customNav.selectOnLeft = selectedLeft;
        customNav.selectOnRight = selectedRight;
        customNav.selectOnUp = selectedUp;
        customNav.selectOnDown = selectedDown;
    }

    private void Update()
    {
        if (selected && (Input.GetKeyDown(KeyCode.KeypadEnter) || inputActions.WindlessLand.Interact.triggered))
        {
            customNav.selectOnLeft = null;
            customNav.selectOnRight = null;
        }

        if (selected && movementInput.x > 0)
        {
            ControllerMovement(true);
        }
    }

    private void ControllerMovement(bool moveRight)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        selected = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selected = false;
        currentValue = gameObject.GetComponent<Slider>().value;
    }

    public bool GetSelected()
    {
        return selected;
    }

    public float GetValueAtDeselect()
    {
        return currentValue;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
        Debug.Log("Deselect");
        customNav.selectOnLeft = selectedLeft.GetComponent<Button>();
        customNav.selectOnRight = selectedRight.GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        selected = true;
    }

}
