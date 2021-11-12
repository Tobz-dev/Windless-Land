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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIManager;
    [SerializeField] private Canvas canvas;
    private Vector3 offset;
    private float zCoord;
    private Vector2 startPos;
    private Vector2[] startPositions;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;
    [SerializeField] private GameObject[] scalableObjects;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private Slider fontScaleSlider;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseFirstSelected;
    [SerializeField] private InputAction pause;
    [SerializeField] private GameObject editModePanel;
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private Toggle mouseToggle;
    private Vector2[] anchorOffsets;
    private GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();

    private bool editMode;
    public PlayerInput playerInput;


    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    void Start()
    {
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);
        scaleSlider.onValueChanged.AddListener(delegate { ScaleUI(); });
        pause.performed += _ => TogglePauseMenu();

        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        textObjects = GameObject.FindGameObjectsWithTag("Text");
        for (int i = 0; i <= textObjects.Length - 1; i++)
        {
            textMeshProUGUIList.Add(textObjects[i].GetComponent<TextMeshProUGUI>());
        }

        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        editModePanel.SetActive(false);
        
        anchorOffsets = new Vector2[scalableObjects.Length];
        startPositions = new Vector2[scalableObjects.Length];
        for (int i = 0; i <= scalableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = scalableObjects[i].GetComponent<RectTransform>();
            anchorOffsets[i] = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            startPositions[i] = rectTransform.anchoredPosition;
        }

        dropDown.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTransform();
        }


        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoTransform();
        }

    }

    public void SetRemapPreset(string preset)
    {
        if(preset.Equals("leftHand"))
        {

        }
        if (preset.Equals("rightHand"))
        {
            //reset bindings from ActionEvent in script
            playerInput.actions["Interact"].ApplyBindingOverride("<Keyboard>/#(i)");
            playerInput.actions["Health Refill"].ApplyBindingOverride("<Keyboard>/#(o)");
            playerInput.actions["Dodgeroll"].ApplyBindingOverride("<Keyboard>/#(LShift)");
            playerInput.actions["Dodgeroll"].ApplyBindingOverride("<Keyboard>/#(LShift)");
        }
    }

    public void ResetTransform()
    {
        movableObject.anchoredPosition = startPos;
        for (int i = 0; i <= scalableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = scalableObjects[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = startPositions[i];
        }
    }

    private void UndoTransform()
    {
        if (currentPos == previousPositions[previousPositions.Count - 1] && currentPos != previousPositions[0])
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
            movableObject.anchoredPosition = previousPositions[previousPositions.Count - 1];
            currentPos = previousPositions[previousPositions.Count - 1];
        }

    }

    public void MoveUIElement(GameObject movedObject)
    {
        RectTransform rectTransform = movableObject.GetComponent<RectTransform>();
        RectTransform movingObject = movableObject;

        /*
        for (int i = 0; i <= scalableObjects.Length - 1; i++)
        {
            if (movedObject.name.Equals(scalableObjects[i]))
            {
                rectTransform = scalableObjects[i].GetComponent<RectTransform>();
            }
            
        }
        */
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        

        float minX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * -rectTransform.anchorMin.x;
        float maxX = (canvasRect.sizeDelta.x - rectTransform.sizeDelta.x) * rectTransform.anchorMax.x;
        float minY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * -rectTransform.anchorMin.y;
        float maxY = (canvasRect.sizeDelta.y - rectTransform.sizeDelta.y) * rectTransform.anchorMin.y;

        /*
        Vector2 anchorPos = rectTransform.anchoredPosition;
        float xpos = anchorPos.x;
        float ypos = anchorPos.y;
        xpos = Mathf.Clamp(xpos, minX, maxX);
        ypos = Mathf.Clamp(ypos, minY, maxY);
        anchorPos.x = xpos;
        anchorPos.y = ypos;
        rectTransform.anchoredPosition = anchorPos;
        */

        Vector3[] cornersCache = new Vector3[4];
        canvasRect.GetWorldCorners(cornersCache);
        Vector3 canvasLowLeft = cornersCache[0], canvasTopRight = cornersCache[2];
        var containerSize = canvasTopRight - canvasLowLeft;
        movingObject.GetWorldCorners(cornersCache);
        Vector3 movableLowLeft = cornersCache[0], movableTopRight = cornersCache[2];
        var movableSize = movableTopRight - movableLowLeft;

        var position = movingObject.position;
        Vector3 deltaLowLeft = position - movableLowLeft, deltaTopRight = movableTopRight - position;
        position.x = movableSize.x < containerSize.x
            ? Mathf.Clamp(position.x, canvasLowLeft.x + deltaLowLeft.x, canvasTopRight.x - deltaTopRight.x)
            : Mathf.Clamp(position.x, canvasTopRight.x - deltaTopRight.x, canvasLowLeft.x + deltaLowLeft.x);
        position.y = movableSize.y < containerSize.y
            ? Mathf.Clamp(position.y, canvasLowLeft.y + deltaLowLeft.y, canvasTopRight.y - deltaTopRight.y)
            : Mathf.Clamp(position.y, canvasTopRight.y - deltaTopRight.y, canvasLowLeft.y + deltaLowLeft.y);
        movingObject.position = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (editMode)
        {
            RectTransform movedObject = movableObject;
            /*for (int i = 0; i <= scalableObjects.Length - 1; i++)
            {
                if (movedObject.name.Equals(scalableObjects[i]))
                {
                    movedObject = scalableObjects[i].GetComponent<RectTransform>();
                }
            }
            */
            

            movedObject.anchoredPosition += eventData.delta;
            MoveUIElement(eventData.selectedObject);
        }

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (editMode)
        {
            currentPos = movableObject.anchoredPosition;
            previousPositions.Add(currentPos);
            Debug.Log("AddedPosCurrent");
        }
    }

    public void ActivateEditMode()
    {
        if (!editModePanel.activeInHierarchy)
        {
            editMode = true;
            editModePanel.SetActive(true);
        }
        else
        {
            editMode = false;
            editModePanel.SetActive(false);
        }
    }

    private void ScaleUI()
    {
        foreach (GameObject gameObject in scalableObjects)
        {
            gameObject.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 anchorPos = rectTransform.anchoredPosition;
            float xpos = anchorPos.x;
            xpos = Mathf.Clamp(xpos, 0, Screen.width - rectTransform.sizeDelta.x);
            anchorPos.x = xpos;
            rectTransform.anchoredPosition = anchorPos;

        }
    }

    public void ChangeFontSize()
    {
        foreach (TextMeshProUGUI textObject in textMeshProUGUIList)
        {
            if (textObject.GetComponent<TMP_FontAsset>() != null)
            {
                textObject.fontSize = fontScaleSlider.value;
            }

            /*if (dropDown.value == 0)
            {
                textObject.fontSize = 30;
            }
            if (dropDown.value == 1)
            {
                textObject.fontSize = 40;
            }
            if (dropDown.value == 2)
            {
                textObject.fontSize = 50;
            }*/
            
        }
    }

    public void ChangeAnchoredPos(string buttonPos)
    {
        for (int i = 0; i <= scalableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = scalableObjects[i].GetComponent<RectTransform>();
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

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeInHierarchy)
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(false);
            }
            pauseMenu.SetActive(false);
            movableObject.gameObject.SetActive(true);
            player.GetComponent<CharacterControllerRemapTest>().SetMoveAllow(true);
            Time.timeScale = 1;
        }
        else
        {
            foreach(GameObject menu in menus)
            {
                menu.SetActive(false);
            }
            pauseMenu.SetActive(true);
            movableObject.gameObject.SetActive(false);
            player.GetComponent<CharacterControllerRemapTest>().SetMoveAllow(false);

            Time.timeScale = 0;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstSelected);
        }
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }




}
