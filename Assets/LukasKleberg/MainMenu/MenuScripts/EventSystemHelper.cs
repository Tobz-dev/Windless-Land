using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventSystemHelper : MonoBehaviour
{
    private GameObject currentSelectedGameObject;
    private bool isKeyBoardActive = true;
    private bool shouldSetToStartObject = false;
     

    public void Start()
    {
        currentSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }


    //so something is making the keyboard selection stay unless you first give a keyboard input.
    public void Update()
    {

        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            //EventSystem.current.SetSelectedGameObject(null);
            //Debug.Log("Mouse moved left");
            if(isKeyBoardActive) 
            {
                Cursor.lockState = CursorLockMode.None;
                DisableSelection();
            }
            
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            //EventSystem.current.SetSelectedGameObject(null);
            //Debug.Log("Mouse moved right");

            if (isKeyBoardActive)
            {
                Cursor.lockState = CursorLockMode.None;

                DisableSelection();
            }
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                //if it is a mouse press do nothing.
                //Debug.Log("left mouse click");
                return;
            }

            else 
            {
                //else it is a keyboard input, then diable the mouse cursor. and enable to eventsystem selection.
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;

                isKeyBoardActive = true;

                SetToStartObject();

                //ApplyCurrentSelectedGameObject();
                //Debug.Log("key press");
            }

        }
    }


    public void ChangeFirstSelectedObject(GameObject newFirstSelectedObject) 
    {
        //Debug.Log("in EventS helper, setting first selected to " + newFirstSelectedObject.name);

        currentSelectedGameObject = newFirstSelectedObject;

        EventSystem.current.firstSelectedGameObject = newFirstSelectedObject;

        if (isKeyBoardActive)
        {
            ApplyCurrentSelectedGameObject();
        }

        //Debug.Log("now it is " + EventSystem.current.ToString());

    }

    public void SetToStartObject()
    {
        if (shouldSetToStartObject) 
        {
            ApplyCurrentSelectedGameObject();
            shouldSetToStartObject = false;
        }
    }


    public void ApplyCurrentSelectedGameObject() 
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(currentSelectedGameObject);
    }

    public void DisableSelection() 
    {
        currentSelectedGameObject = EventSystem.current.firstSelectedGameObject;

        //Debug.Log("in disable selection, set null");
        EventSystem.current.SetSelectedGameObject(null); 

        isKeyBoardActive = false;
        shouldSetToStartObject = true;
    }

}
