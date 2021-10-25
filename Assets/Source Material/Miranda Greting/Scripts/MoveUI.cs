using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform movableObject;
    private Vector3 offset;
    private float zCoord;
    private Vector2 startPos;
    private Vector2 currentPos;
    private List <Vector2> previousPositions;

    void Start()
    {
        startPos = movableObject.anchoredPosition;
        currentPos = startPos;
        previousPositions = new List<Vector2>();
        previousPositions.Add(currentPos);
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

    private void ResetTransform()
    {
        movableObject.anchoredPosition = startPos;
    }

    private void UndoTransform()
    {
        /*
        for (int i = previousPositions.Count - 1; i >= 0; i--)
        {
            //movableObject.anchoredPosition = previousPositions[previousPositions.Count - 2];
            if (previousPositions[i] == currentPos && currentPos != previousPositions[0])
            {
                Debug.Log("PosFound");
                movableObject.anchoredPosition = previousPositions[i - 1];
            }
            else if (currentPos == previousPositions[0])
            {
                Debug.Log("EndReach");
            }
        }
        currentPos = movableObject.anchoredPosition;
        */
        
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
        /*
        for(int i = previousPositions.Count-1; i >= 0; i--)
        {
            if(previousPositions[i] == currentPos)
            {
                if (i < previousPositions.Count - 1)
                {
                    for (int y = previousPositions.Count - 1; y > i; i--)
                    {
                        previousPositions.RemoveAt(y);
                    }
                }
            }
        }
        */
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        currentPos = movableObject.anchoredPosition;
        previousPositions.Add(currentPos);
        Debug.Log("AddedPosCurrent");
    }

}
