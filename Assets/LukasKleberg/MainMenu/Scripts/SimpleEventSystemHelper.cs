using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleEventSystemHelper : MonoBehaviour
{
    private GameObject currentSelectedGameObject;

    public void ChangeFirstSelectedObject(GameObject newFirstSelectedObject)
    {
        Debug.Log("in EventS helper, setting first selected to " + newFirstSelectedObject.name);

        currentSelectedGameObject = newFirstSelectedObject;

        EventSystem.current.firstSelectedGameObject = newFirstSelectedObject;

        ApplyCurrentSelectedGameObject();

        Debug.Log("now it is " + EventSystem.current.ToString());

    }

    public void ApplyCurrentSelectedGameObject()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(currentSelectedGameObject);
    }
}
