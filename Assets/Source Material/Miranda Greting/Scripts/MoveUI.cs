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
    private Vector3 offset;
    private float zCoord;
    private Vector2 startPos;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;
    [SerializeField] private GameObject[] scalableObjects;
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private GameObject rebindingMenu;
    [SerializeField] private GameObject rebindingMenuFirstSelected;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseFirstSelected;
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private GameObject uiMenuFirstSelected;
    [SerializeField] private InputAction pause;
    [SerializeField] private GameObject editModePanel;
    private Vector2[] anchorOffsets;


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
        rebindingMenu.SetActive(false);
        pause.performed += _ => TogglePauseMenu();
        editModePanel.SetActive(false);
        pauseMenu.SetActive(false);
        uiMenu.SetActive(false);
        anchorOffsets = new Vector2[scalableObjects.Length];
        for (int i = 0; i <= scalableObjects.Length - 1; i++)
        {
            RectTransform rectTransform = scalableObjects[i].GetComponent<RectTransform>();
            anchorOffsets[i] = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        }
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

        if (Input.GetKey(KeyCode.Escape))
        {
            //ToggleMenu();
        }

    }

    private void ResetTransform()
    {
        movableObject.anchoredPosition = startPos;
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

    public void OnDrag(PointerEventData eventData)
    {
        movableObject.anchoredPosition += eventData.delta;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

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
        foreach (GameObject gameObject in scalableObjects)
        {
            gameObject.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, 1);
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

    public void ToggleRebindMenu()
    {
        if (rebindingMenu.activeInHierarchy)
        {
            rebindingMenu.SetActive(false);
        }
        else
        {
            rebindingMenu.SetActive(true);
        }
        //rebindingMenu.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            rebindingMenu.SetActive(false);
            uiMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstSelected);
        }
    }

    public void ToggleUIMenu()
    {
        if (uiMenu.activeInHierarchy)
        {
            uiMenu.SetActive(false);
            editModePanel.SetActive(false);
        }
        else
        {
            uiMenu.SetActive(true);
            editModePanel.SetActive(true);
        }
    }

}
