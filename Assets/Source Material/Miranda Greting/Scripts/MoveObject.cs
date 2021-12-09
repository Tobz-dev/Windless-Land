using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private bool individualEditMode;
    private bool edited;
    private GameObject editingParticles;
    // Start is called before the first frame update
    void Start()
    {
        editingParticles = gameObject.transform.GetChild(0).gameObject;
        editingParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (individualEditMode)
        {
            edited = true;
            gameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //currentPos = movableObject.anchoredPosition;
        //previousPositions.Add(currentPos);
        //Debug.Log("AddedPosCurrent");
        if (individualEditMode)
        {
            edited = false;
            editingParticles.SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        individualEditMode = true;
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        individualEditMode = true;
        if (individualEditMode && !edited)
        {
            editingParticles.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        individualEditMode = true;
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        individualEditMode = true;
        if (individualEditMode)
        {
            editingParticles.SetActive(false);
        }
    }
}
