using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChange : MonoBehaviour
{

    GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject[] ObjectsToGetTextFrom;

    //goes from 45 (min) to 60 (max) in increments of 5. 50 being default
    private int currentFontSize = 50;
    private int savedFontSize;

    // Start is called before the first frame update
    void Start()
    {
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

        for (int i = 1; i <= ObjectsToGetTextFrom.Length - 1; i++)
        {
            ObjectsToGetTextFrom[i].SetActive(false);
            //Debug.Log("in fontchange test. inactive object loop");
        }

        //get the currentFontSize from a playerpref
        savedFontSize = PlayerPrefs.GetInt("FontSize");
        ChangeFontSize(savedFontSize);

        //PlayerPrefs.SetInt("FontSize", 50);
        //adjust it based on the value.
        //SetFontSize();
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

    public void ChangeFontSize(int newFontSize) 
    {

        Debug.Log("in ChangeFontSize. newFontSize is " + newFontSize);
        Debug.Log("in ChangeFontSize. currentFontSize is " + currentFontSize);

        int fontSizeDifference = newFontSize - currentFontSize;

        currentFontSize = newFontSize;

        PlayerPrefs.SetInt("FontSize", currentFontSize);


        Debug.Log("in ChangeFontSize. size differnece is " + fontSizeDifference);
        //Debug.Log("in ChangeFontSize. new font size is" + currentFontSize);

        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIList)
        {
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.fontSize += fontSizeDifference;
            }
        }
    }
}
