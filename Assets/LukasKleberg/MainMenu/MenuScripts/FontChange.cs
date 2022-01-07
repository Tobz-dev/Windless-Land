using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Main Author: Lukas Kleberg
public class FontChange : MonoBehaviour
{

    GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject[] objectsToGetTextFromAndSetInactive, objectsToGetTextFromAndSetActive;
    [SerializeField] private TMP_FontAsset[] fontsArray;

    //goes from 45 (min) to 60 (max) in increments of 5. 50 being default
    private int currentFontSize = 50;
    private int savedFontSize;

    //Variabler som använder config filer.
    private int fontIndex2;
    private int fontSize2;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= objectsToGetTextFromAndSetInactive.Length - 1; i++)
        {
            //Debug.Log(" FontChange object is " + objectsToGetTextFromAndSetInactive[i].name);
            if (objectsToGetTextFromAndSetInactive[i] != null)
            {
                objectsToGetTextFromAndSetInactive[i].SetActive(true);
            }
            
            //Debug.Log("in fontchange test. after activate object loop");
        }

        for (int i = 0; i <= objectsToGetTextFromAndSetActive.Length - 1; i++)
        {
            if (objectsToGetTextFromAndSetActive[i] != null)
            {
                objectsToGetTextFromAndSetActive[i].SetActive(true);
            }
        }


        textObjects = GameObject.FindGameObjectsWithTag("Text");
        for (int i = 0; i <= textObjects.Length - 1; i++)
        {
            textMeshProUGUIList.Add(textObjects[i].GetComponent<TextMeshProUGUI>());
            //Debug.Log("textMeshProUGUIList count is" + textMeshProUGUIList.Count);
        }

        for (int i = 0; i <= objectsToGetTextFromAndSetInactive.Length - 1; i++)
        {
            objectsToGetTextFromAndSetInactive[i].SetActive(false);
            //Debug.Log("in fontchange test. inactive object loop");
        }

        //get the currentFontSize from a playerpref
        ChangeFontSize(PlayerPrefs.GetInt("fontSize", 50));

        ChangeFont(PlayerPrefs.GetInt("fontIndex", 0));

    }

    public void ChangeFont(int fontIndex)
    {
        //TODO set a player pref. so that other scenes can access the new font
        TMP_FontAsset fontToChangeTo = fontsArray[fontIndex];
        PlayerPrefs.SetInt("fontIndex", fontIndex);

        //TMP_FontAsset testFont = fontToChangeTo;
        //Debug.Log("in ChangeFont. TextList is " + textMeshProUGUIList.Count);
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

        //Debug.Log("in ChangeFontSize. newFontSize is " + newFontSize);
        //Debug.Log("in ChangeFontSize. currentFontSize is " + currentFontSize);

        int fontSizeDifference = newFontSize - currentFontSize;

        currentFontSize = newFontSize;

        PlayerPrefs.SetInt("fontSize", currentFontSize);


        //Debug.Log("in ChangeFontSize. size differnece is " + fontSizeDifference);
        //Debug.Log("in ChangeFontSize. new font size is" + currentFontSize);

        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIList)
        {
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.fontSize += fontSizeDifference;
            }
        }
    }

    public void setConfig(int newfontIndex, int newfontSize)
    {
        fontIndex2 = newfontIndex;
        fontSize2 = newfontSize;
    }

    public int GetFontIndex()
    {
        return fontIndex2;
    }

    public int GetFontSize()
    {
        return fontSize2;
    }
}
