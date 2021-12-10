using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private bool individualEditMode;
    private bool edited;
    private GameObject highlightingParticles;
    private GameObject selectedParticles;
    [SerializeField] private Toggle editModeToggle;
    [SerializeField] private GameObject parent;
    [SerializeField] private MoveUI uiManager;
    bool highlighted;
    bool selected;

    // Start is called before the first frame update
    void Start()
    {
        individualEditMode = false;
        highlightingParticles = gameObject.transform.GetChild(0).gameObject;
        selectedParticles = gameObject.transform.GetChild(1).gameObject;
        highlightingParticles.SetActive(false);
        selectedParticles.SetActive(false);
    }

    public void InactivatePrototype()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (isSelected && !selectedParticles.activeInHierarchy)
        {
            selectedParticles.SetActive(true);
            return;
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SetHighlighted(bool isMarked)
    {
        highlighted = isMarked;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            highlightingParticles.SetActive(true);
            uiManager.ChangeSelectedObject(gameObject);
        }
        else if(!editModeToggle.isOn)
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
                edited = true;
                gameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
                uiManager.MoveUIElement(gameObject);
            }
            else
            {
                edited = false;
            }
        }

        else if (!editModeToggle.isOn)
        {            
            edited = true;
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
        //currentPos = movableObject.anchoredPosition;
        //previousPositions.Add(currentPos);
        //Debug.Log("AddedPosCurrent");
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
        }
        if (individualEditMode)
        {
            edited = false;
            highlightingParticles.SetActive(false);
        }

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
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
            parent.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (editModeToggle.isOn)
        {
            individualEditMode = true;
        }
        else if (!editModeToggle.isOn)
        {
            individualEditMode = false;
        }
        if (individualEditMode)
        {
            highlightingParticles.SetActive(false);
        }
    }
}
