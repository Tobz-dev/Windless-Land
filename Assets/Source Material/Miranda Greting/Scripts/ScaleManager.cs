using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScaleManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private bool selected;
    private float currentValue;
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

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }
}
