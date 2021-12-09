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
    [SerializeField] private MoveUI uiManager;

    // Start is called before the first frame update
    void Start()
    {
        individualEditMode = false;
        editingParticles = gameObject.transform.GetChild(0).gameObject;
        editingParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
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
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode)
        {
            edited = false;
            editingParticles.SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode && !edited)
        {
            editingParticles.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode)
        {
            editingParticles.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        individualEditMode = uiManager.CheckEditMode();
        if (individualEditMode)
        {
            editingParticles.SetActive(false);
        }
    }
}
