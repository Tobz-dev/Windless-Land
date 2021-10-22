using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventSystemHelper : MonoBehaviour
{
    // Start is called before the first frame update
    //public EventSystem eventSystem;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            Debug.Log("in EventS helper");
        }
    }



    public void ChangeFirstSelectedObject(GameObject newFirstSelectedObject) 
    {
        Debug.Log("in EventS helper, setting first selected to " + newFirstSelectedObject.name);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(newFirstSelectedObject);

        Debug.Log("then " + EventSystem.current.ToString());

    }

}
