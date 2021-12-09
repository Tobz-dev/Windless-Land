using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform movableObject;
    private Vector2 startPos;
    private Vector2[] startPositions;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;
    [SerializeField] private GameObject[] editableObjects;
    [SerializeField] private Slider scaleSlider;

    [SerializeField] private GameObject editModePanel;

    [SerializeField] private Toggle individualEditToggle;
    private bool individualEditMode = false;
    private bool prototypeActivated = false;

    private GameObject markedObject;

    [SerializeField] private Button resetMarkedObject; //obs, if(toggle.marked, this button.SetActive(true), otherwise false!!!
    [SerializeField] private Button resetAllObjects;

    private Vector2[] anchorOffsets;
    private GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();


    void Start()
    {
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);
        scaleSlider.onValueChanged.AddListener(delegate { ScaleUI(); });

        editModePanel.SetActive(false);

        anchorOffsets = new Vector2[editableObjects.Length];
        startPositions = new Vector2[editableObjects.Length];
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = editableObjects[i].GetComponent<RectTransform>();
            anchorOffsets[i] = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            startPositions[i] = rectTransform.anchoredPosition;
            if (i != 2)
            {
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        individualEditMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTransform(false);
        }
    }

    public void ResetTransform(bool onlyMarkedObject)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            if (onlyMarkedObject)
            {
                if (editableObjects[i] == markedObject)
                {
                    markedObject.GetComponent<RectTransform>().anchoredPosition = startPositions[i];
                }
            }
            else
            {
                editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
            }
        }
    }

    public void ChangeEditMode()
    {
        if (individualEditToggle.isOn)
        {
            individualEditMode = true;
        }
        else
        {
            individualEditMode = false;
        }
    }

    public bool CheckEditMode()
    {
        return individualEditMode;
    }
    public GameObject GetParentObject()
    {
        return editableObjects[0];
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!individualEditToggle.isOn)
        {
            Debug.Log("detectedDrag");
            movableObject.anchoredPosition += eventData.delta;
        }
        for (int i = 0; i <= editableObjects.Length - 1; i++) {
            if (eventData.selectedObject == editableObjects[i])
            {
                Debug.Log("DetectedObject");
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("detectedDrag");

        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            if (eventData.selectedObject == editableObjects[i])
            {
                Debug.Log("DetectedObject");
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentPos = movableObject.anchoredPosition;
        previousPositions.Add(currentPos);
        Debug.Log("AddedPosCurrent");
    }

    public void ActivateEditMode()
    {
        editModePanel.SetActive(true);
    }

    private void ScaleUI()
    {
        foreach (GameObject gameObject in editableObjects)
        {
            gameObject.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
        }
    }

    public void ChangeAnchoredPos(string buttonPos)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = editableObjects[i].GetComponent<RectTransform>();
            Vector2 rectMinMax = rectTransform.anchorMin;

            if (buttonPos.Equals("UpLeft"))
            {
                rectMinMax = new Vector2(0, 1);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(anchorOffsets[i].x, anchorOffsets[i].y);
            }
            else if (buttonPos.Equals("UpMid"))
            {
                rectMinMax = new Vector2(0.5f, 1);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(0, anchorOffsets[i].y);

            }
            else if (buttonPos.Equals("UpRight"))
            {
                rectMinMax = new Vector2(1, 1);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(-anchorOffsets[i].x * 2, anchorOffsets[i].y);

            }
            else if (buttonPos.Equals("MidLeft"))
            {
                rectMinMax = new Vector2(0, 0.5f);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(anchorOffsets[i].x, 0);
            }
            else if (buttonPos.Equals("MidRight"))
            {
                rectMinMax = new Vector2(1, 0.5f);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(-anchorOffsets[i].x * 2, 0);
            }
            else if (buttonPos.Equals("DownLeft"))
            {
                rectMinMax = new Vector2(0, 0);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(anchorOffsets[i].x, -anchorOffsets[i].y * 4);
            }
            else if (buttonPos.Equals("DownMid"))
            {
                rectMinMax = new Vector2(0.5f, 0);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(0, -anchorOffsets[i].y * 4);
            }
            else if (buttonPos.Equals("DownRight"))
            {
                rectMinMax = new Vector2(1, 0);
                rectTransform.anchorMin = rectMinMax;
                rectTransform.anchorMax = rectMinMax;
                rectTransform.pivot = rectMinMax;
                rectTransform.anchoredPosition = new Vector2(-anchorOffsets[i].x * 2, -anchorOffsets[i].y * 4);
            }
        }
    }
}
