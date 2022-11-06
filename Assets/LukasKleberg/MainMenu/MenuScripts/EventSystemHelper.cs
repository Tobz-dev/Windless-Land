using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Main Author: Lukas Kleberg
public class EventSystemHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject currentFirstSelectedGameObject;
    [SerializeField]
    private bool isKeyBoardActive = true;
    [SerializeField]
    private bool shouldSetToStartObject = false;

    [SerializeField]
    private GraphicRaycaster raycaster;

    private PointerEventData m_PointerEventData;

    public void Start()
    {
        currentFirstSelectedGameObject = EventSystem.current.firstSelectedGameObject;

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

            //else it is a keyboard input, then diable the mouse cursor. and enable to eventsystem selection.
            else
            {
                Debug.Log("key press");

                //raycast. and check for button.
                //set its anim to normal.

                m_PointerEventData = new PointerEventData(EventSystem.current);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    Debug.Log("Hit " + result.gameObject.name);
                    if (TryGetComponent(out RectTransform buttonAnimator))
                    {
                        Debug.Log(result.gameObject.name + " had a RectTransform");
                        //buttonAnimator.SetTrigger("Normal");
                    }
                    else 
                    {
                        Debug.Log(result.gameObject.name + " did not have a RectTransform");
                    }                    
                }


                //currentGameObject.GetComponent<Animator>().SetTrigger("Normal");

                Cursor.lockState = CursorLockMode.Locked;


                isKeyBoardActive = true;

                SetToStartObject();
            }
        }
    }


    public void ChangeFirstSelectedObject(GameObject newFirstSelectedObject) 
    {
        //Debug.Log("in EventSystemHelper, setting first selected to " + newFirstSelectedObject.name);

        currentFirstSelectedGameObject = newFirstSelectedObject;

        EventSystem.current.firstSelectedGameObject = newFirstSelectedObject;

        if (isKeyBoardActive)
        {
            ApplyCurrentSelectedGameObject();
        }
        //Debug.Log("now it is " + EventSystem.current.ToString());

    }

    private void SetToStartObject()
    {
        if (shouldSetToStartObject) 
        {
            ApplyCurrentSelectedGameObject();
            shouldSetToStartObject = false;
        }
    }


    private void ApplyCurrentSelectedGameObject() 
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(currentFirstSelectedGameObject);
    }

    private void DisableSelection() 
    {
        currentFirstSelectedGameObject = EventSystem.current.firstSelectedGameObject;

        Debug.Log("in disable selection, set null");
        EventSystem.current.SetSelectedGameObject(null); 

        isKeyBoardActive = false;
        shouldSetToStartObject = true;
    }

    public void UnlockMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
