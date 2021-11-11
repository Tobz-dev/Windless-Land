using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChange : MonoBehaviour
{

    GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject[] ObjectsToGetTextFrom;

    // Start is called before the first frame update
    void Start()
    {
        //this turns off the options menu if it is not active when starting the program. thats bad.
        for (int i = 0; i <= ObjectsToGetTextFrom.Length - 1; i++)
        {
            ObjectsToGetTextFrom[i].SetActive(true);
            //Debug.Log("in fontchange test. activate object loop");
        }


        textObjects = GameObject.FindGameObjectsWithTag("Text");
        for (int i = 0; i <= textObjects.Length - 1; i++)
        {
            textMeshProUGUIList.Add(textObjects[i].GetComponent<TextMeshProUGUI>());
            //Debug.Log("textMeshProUGUIList count is" + textMeshProUGUIList.Count);
        }

        //this turns off the options menu if it is not active when starting the program. thats bad.
        for (int i = 1; i <= ObjectsToGetTextFrom.Length - 1; i++)
        {
            ObjectsToGetTextFrom[i].SetActive(false);
            //Debug.Log("in fontchange test. inactive object loop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFont(TMP_FontAsset fontToChangeTo)
    {
        //TODO set a player pref. so that other scenes can access the new font

        //TMP_FontAsset testFont = fontToChangeTo;
        Debug.Log("in ChangeFont. TextList is " + textMeshProUGUIList.Count);
        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIList)
        {
            if (textMeshProUGUI != null) 
            {
                //Debug.Log("in ChangeFont " + textMeshProUGUI.name);
                textMeshProUGUI.font = fontToChangeTo;
            }
        }

    }

    public void ChangeFontSize(int fontSize) 
    {
        //TODO set a player pref. so that other scenes can access the new font size

        Debug.Log("in ChangeFontSize. TextList is " + textMeshProUGUIList.Count);
        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIList)
        {
            if (textMeshProUGUI != null)
            {
                //Debug.Log("in ChangeFontSize " + textMeshProUGUI.name);
                textMeshProUGUI.fontSize = fontSize;
            }
        }
    }
}
