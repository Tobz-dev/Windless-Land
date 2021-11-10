using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSize : MonoBehaviour
{

    [SerializeField]
    private GameObject[] menuCanvases;

    [SerializeField]
    private Vector3 scaleBig = new Vector3(1,1,1);

    private Vector3 scaleNormal = new Vector3(1, 1, 1);

    private bool isBig = false;
    public void ToggleMenuSize() 
    {
        if (isBig == false)
        {
            for (int i = 0; i < menuCanvases.Length; i++)
            {
                menuCanvases[i].GetComponent<RectTransform>().localScale = scaleBig;
            }
            isBig = true;
        }
        else 
        {
            for (int i = 0; i < menuCanvases.Length; i++)
            {
                menuCanvases[i].GetComponent<RectTransform>().localScale = scaleNormal;
            }
            isBig = false;
        }

    }
}
