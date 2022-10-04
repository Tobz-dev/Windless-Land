using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Main Author: Lukas Kleberg
public class EventSystemHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject currentSelectedGameObject;
    [SerializeField]
    private bool isKeyBoardActive = true;
    [SerializeField]
    private bool shouldSetToStartObject = false;
     

    public void Start()
    {
        currentSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }

    public void Update()
    {

        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving left or right
            //Debug.Log("Mouse moved left");
            if(isKeyBoardActive) 
            {
                Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
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

                //Hmm. this makes it so the cursor can be on a button in the center. and I do want to avoid that.
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;


                isKeyBoardActive = true;

                SetToStartObject();

                //Debug.Log("key press");
            }

        }
    }


    public void ChangeFirstSelectedObject(GameObject newFirstSelectedObject) 
    {
        //Debug.Log("in EventSystemHelper, setting first selected to " + newFirstSelectedObject.name);

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

    public void UnlockMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
