using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveUI : MonoBehaviour
{
    [SerializeField] private RectTransform movableObject; //HUD Gameobject goes here
    [SerializeField] private GameObject UIMenu; //AjustUI/HUD goes here

    private bool active = true;

    private GameObject highlightParticles;
    private Vector3 originalScale;

    private static Vector2 startPos;
    private Vector2[] startPositions;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;
    [SerializeField] private GameObject[] editableObjects; // all individual UI components go here; healthbar, potions, manabar, (keys) - in this order!!
  
    [SerializeField] private Slider scaleSlider;
    private ScaleManager scaleScript;
    private float previousScaleValue;

    [SerializeField] private Toggle individualEditToggle;
    private bool individualEditMode = false;
    private bool prototypeActivated = false;

    private GameObject markedObject;

    [SerializeField] private Button resetMarkedObject; //obs, if(toggle.marked, this button.SetActive(true), otherwise false!!!
    [SerializeField] private Button resetAllObjects;

    private Vector2[] anchorOffsets;
    private Vector3[] originalScales;
    private float[] currentScaleSlideValues;
    private GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    private bool fromSript = false;
    void OnEnable()
    {
        //InactivatePrototype(false);
        foreach(GameObject gameObject in editableObjects)
        {
            movableObject.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
    }
    void OnDisable()
    {
        //InactivatePrototype(true);
    }

    void Start()
    {
        originalScale = movableObject.localScale;
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);
        scaleSlider.onValueChanged.AddListener(delegate { ScaleUI(); });

        anchorOffsets = new Vector2[editableObjects.Length+1];
        anchorOffsets[0] = new Vector2(movableObject.anchoredPosition.x, movableObject.anchoredPosition.y);
        startPositions = new Vector2[editableObjects.Length];
        originalScales = new Vector3[editableObjects.Length + 2];
        currentScaleSlideValues = new float[editableObjects.Length + 2];
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = editableObjects[i].GetComponent<RectTransform>();
            anchorOffsets[i+1] = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            startPositions[i] = rectTransform.anchoredPosition;
            originalScales[i] = rectTransform.localScale;
            currentScaleSlideValues[i] = scaleSlider.value;
            if (i != 2)
            {
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        originalScales[editableObjects.Length] = editableObjects[0].transform.GetChild(1).localScale;
        originalScales[editableObjects.Length+1] = editableObjects[1].transform.GetChild(1).localScale;
        individualEditMode = false;
        individualEditToggle.isOn = false;
        movableObject.GetChild(0).gameObject.SetActive(false);
        movableObject.GetChild(1).gameObject.SetActive(true);

        scaleSlider.value = 1;
        scaleScript = scaleSlider.GetComponent<ScaleManager>();

        UIMenu.SetActive(false);
        InactivatePrototype(true);
    }

    //Sve function in procress
    /*
    private void SaveUIPositions()
    {
        RectTransform rect = movableObject.GetComponent<RectTransform>();
        PlayerPrefs.SetString(movableObject.name + "Position", rect.anchoredPosition.x + "," + rect.anchoredPosition.y);
        for(int i = 0; i < editableObjects.Length; i++)
        {

        }
    }
    */

    /* Load function in progress
    private void LoadUIPositions()
    {
        Debug.Log(startPos);
        PlayerPrefs.DeleteKey(movableObject.name + "Position");
        PlayerPrefs.DeleteKey(movableObject.name);
        string position = PlayerPrefs.GetString(movableObject.name + "Position");
        Debug.Log(position);

        if (position.Length == 0)
        {
            Debug.Log(position);
            position = startPos.x + "," + startPos.y;
            Debug.Log(position);

        }

        movableObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[2]);
    }
    */ 

    // Update is called once per frame
    void Update()
    {
        /*
        if (UIMenu.activeSelf && InputManager.inputActions.WindlessLand.Pause.triggered)
        {
            SaveUIPositions(); //Save function i didn't get done in time
        }
        */


        if(UIMenu.activeInHierarchy == false && active)
        {
            InactivatePrototype(true);
            active = false;
        }
        else if(UIMenu.activeInHierarchy == true && !active)
        {
            InactivatePrototype(false);
            active = true;
        }
    }

    public void ActivatePlayerUI(bool active)
    {
        foreach(GameObject editableObject in editableObjects)
        {
            if (active)
            {
                editableObject.SetActive(true);
            }
            else
            {
                editableObject.SetActive(false);
            }
        }
    }

    public void InactivatePrototype(bool setInactive)
    {
        if (setInactive)
        {
            individualEditToggle.isOn = false;
            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                editableObjects[i].GetComponent<MoveObject>().enabled = false;
                editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            movableObject.GetChild(0).gameObject.SetActive(false);
            movableObject.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            individualEditToggle.isOn = false;
            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                editableObjects[i].GetComponent<MoveObject>().enabled = true;
                editableObjects[i].SetActive(true);
            }
            movableObject.GetChild(0).gameObject.SetActive(false);
            movableObject.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChangeSelectedObject(GameObject selection)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            if (editableObjects[i] != selection && objectScript.GetSelected())
            {
                objectScript.SetSelected(false);
                scaleSlider.value = currentScaleSlideValues[i];
            }
            selection.GetComponent<MoveObject>().SetSelected(true);
        }
    }

    public void UnselectAll()
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            objectScript.SetSelected(false);
            objectScript.SetHighlighted(false);
            editableObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            editableObjects[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ToggleEditModes()
    {
        if (individualEditToggle.isOn)
        {
            scaleSlider.transform.parent.parent.GetChild(4).gameObject.SetActive(false);
            resetMarkedObject.gameObject.transform.parent.gameObject.SetActive(true);
            movableObject.GetChild(1).gameObject.SetActive(false);
            ChangeSelectedObject(editableObjects[0]);
        }
        else
        {
            scaleSlider.transform.parent.parent.GetChild(4).gameObject.SetActive(true);
            resetMarkedObject.gameObject.transform.parent.gameObject.SetActive(false);
            movableObject.GetChild(1).gameObject.SetActive(true);
            UnselectAll();
        }
    }

    public void ChangeHighlightedObject(GameObject highlighted)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            MoveObject objectScript = editableObjects[i].GetComponent<MoveObject>();
            if (editableObjects[i] != highlighted && objectScript.GetHighlighted())
            {
                objectScript.SetHighlighted(false);
            }
            highlighted.GetComponent<MoveObject>().SetHighlighted(true);
        }
    }

    public void ResetTransform(bool onlyMarkedObject)
    {
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            if (onlyMarkedObject)
            {
                if (editableObjects[i].GetComponent<MoveObject>().GetSelected())
                {
                    editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
                    editableObjects[i].transform.localScale = originalScales[i];

                }
            }
            else
            {
                fromSript = true;
                ChangeAnchoredPos("UpLeft");
                fromSript = false;
                movableObject.anchoredPosition = startPos;
                editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
                editableObjects[i].transform.localScale = originalScales[i];
                movableObject.localScale = originalScale;
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

    public void MoveUIElement(GameObject movedObject)
    {
        RectTransform rectTransform = movedObject.GetComponent<RectTransform>();
        RectTransform canvasRect = UIMenu.transform.parent.GetComponent<RectTransform>();

        float minX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * -rectTransform.anchorMin.x;
        float maxX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * rectTransform.anchorMax.x;
        float minY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * -rectTransform.anchorMin.y;
        float maxY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * rectTransform.anchorMin.y;

        Vector3[] cornerPositions = new Vector3[4];
        canvasRect.GetWorldCorners(cornerPositions);
        Vector3 canvasLowLeft = cornerPositions[0], canvasTopRight = cornerPositions[2];
        var containerSize = canvasTopRight - canvasLowLeft;
        rectTransform.GetWorldCorners(cornerPositions);
        Vector3 movedObjectLowLeft = cornerPositions[0], movedObjectTopRight = cornerPositions[2];
        var movableSize = movedObjectTopRight - movedObjectLowLeft;

        var position = rectTransform.position;
        Vector3 deltaLowLeft = position - movedObjectLowLeft, deltaTopRight = movedObjectTopRight - position;
        position.x = movableSize.x < containerSize.x 
            ? Mathf.Clamp(position.x, canvasLowLeft.x + deltaLowLeft.x, canvasTopRight.x - deltaTopRight.x)
            : Mathf.Clamp(position.x, canvasTopRight.x - deltaTopRight.x, canvasLowLeft.x + deltaLowLeft.x);
        position.y = movableSize.y < containerSize.y
            ? Mathf.Clamp(position.y, canvasLowLeft.y + deltaLowLeft.y + 10, canvasTopRight.y - deltaTopRight.y)
            : Mathf.Clamp(position.y, canvasTopRight.y - deltaTopRight.y, canvasLowLeft.y + deltaLowLeft.y + 10);
        rectTransform.position = position;
    }

    private void ScaleUI()
    {
        GameObject scaledObject = movableObject.gameObject;
        if (scaleScript.GetSelected())
        {
            if (individualEditToggle.isOn)
            {
                for (int i = 0; i <= editableObjects.Length - 1; i++)
                {
                    if (editableObjects[i].GetComponent<MoveObject>().GetSelected())
                    {
                        if (scaleScript.GetSelected())
                        {
                            //editableObjects[i].transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
                            editableObjects[i].transform.localScale = new Vector3(originalScales[i].x * scaleSlider.value, originalScales[i].y * scaleSlider.value, 1);
                            previousScaleValue = scaleSlider.value;
                            if (i == 0 || i == 2)
                            {
                                //editableObjects[i].transform.GetChild(1).localScale = new Vector3(originalScales[editableObjects.Length].x * scaleSlider.value, originalScales[editableObjects.Length].y * scaleSlider.value, 1);
                                Debug.Log(i + editableObjects[i].name);

                            }
                            else if (i == 1 || i == 3)
                            {
                                //editableObjects[i].transform.GetChild(1).localScale = new Vector3(originalScales[editableObjects.Length+1].x, originalScales[editableObjects.Length+1].y, 1);
                                Debug.Log(i + editableObjects[i].name);

                            }
                            //editableObjects[i].transform.GetChild(0).localScale = new Vector3originalScales[i] * new Vector3(scaleSlider.value, scaleSlider.value, 1);
                            scaledObject = editableObjects[i];
                        }
                        currentScaleSlideValues[i] = scaleScript.GetValueAtDeselect();
                    }
                }
            }
            else
            {
                movableObject.localScale = new Vector3(originalScale.x * scaleSlider.value, originalScale.y * scaleSlider.value, 1);
                previousScaleValue = scaleSlider.value;
            }
            RectTransform rectTransform = scaledObject.GetComponent<RectTransform>();
            Vector2 anchorPos = rectTransform.anchoredPosition;
            float ypos = anchorPos.y;
            float xpos = anchorPos.x;
            xpos = Mathf.Clamp(xpos, 0, Screen.width - rectTransform.sizeDelta.x);
            ypos = Mathf.Clamp(ypos, 10, Screen.height - rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = new Vector2(xpos, ypos);

        }
        else
        {
            scaleSlider.value = previousScaleValue;
        }
    }

    public void ChangeAnchoredPos(string buttonPos)
    {
        RectTransform selectedObject = movableObject;
        int objectInList = 0;
        if (individualEditToggle.isOn && fromSript == false)
        {
            for (int i = 0; i <= editableObjects.Length - 1; i++)
            {
                if (editableObjects[i].GetComponent<MoveObject>().GetSelected())
                {
                    selectedObject = editableObjects[i].GetComponent<RectTransform>();
                    objectInList = i + 1;
                }
            }
        }
        RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
        Vector2 rectMinMax = selectedObject.anchorMin;

        //unless i get individual anchoring to work (rectTransforms are a pain)
        for (int i = 0; i <= editableObjects.Length - 1; i++)
        {
            editableObjects[i].GetComponent<RectTransform>().anchoredPosition = startPositions[i];
        }

        if (buttonPos.Equals("UpLeft"))
        {
            rectMinMax = new Vector2(0, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, anchorOffsets[objectInList].y);
        }
        else if (buttonPos.Equals("UpMid"))
        {
            rectMinMax = new Vector2(0.5f, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(0, anchorOffsets[objectInList].y);

        }
        else if (buttonPos.Equals("UpRight"))
        {
            rectMinMax = new Vector2(1, 1);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, anchorOffsets[objectInList].y);

        }
        else if (buttonPos.Equals("MidLeft"))
        {
            rectMinMax = new Vector2(0, 0.5f);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, 0);
        }
        else if (buttonPos.Equals("MidRight"))
        {
            rectMinMax = new Vector2(1, 0.5f);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, 0);
        }
        else if (buttonPos.Equals("DownLeft"))
        {
            rectMinMax = new Vector2(0, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(anchorOffsets[objectInList].x, -anchorOffsets[objectInList].y * 4);
        }
        else if (buttonPos.Equals("DownMid"))
        {
            rectMinMax = new Vector2(0.5f, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(0, -anchorOffsets[objectInList].y * 4);
        }
        else if (buttonPos.Equals("DownRight"))
        {
            rectMinMax = new Vector2(1, 0);
            rectTransform.anchorMin = rectMinMax;
            rectTransform.anchorMax = rectMinMax;
            rectTransform.pivot = rectMinMax;
            rectTransform.anchoredPosition = new Vector2(-anchorOffsets[objectInList].x * 2, -anchorOffsets[objectInList].y * 4);
        }
    }
}
