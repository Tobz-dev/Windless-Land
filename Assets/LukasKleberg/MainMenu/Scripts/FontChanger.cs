using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FontChanger : MonoBehaviour
{
    //it can update the children of an inactive object. but not if the inactive object is a child of the parent.
    //could solve that by having an array. for each of the menus?
    [SerializeField]
    private GameObject[] parentObjects;

    //if the text is on UI, it has to be specified on declaration
    private TextMeshProUGUI[] textMeshProUGUIarray;

    //remembering fonts between play sessions would be nice.

    [SerializeField]
    private TextMeshProUGUI[] fontChoiceArray;

    //save the buttons fonts on Start. and then set them back to their original font.
    [SerializeField]
    private TMP_FontAsset[] fontArray;

    public void Start()
    {
        for(int i = 0; i < fontChoiceArray.Length; i++) 
        {
            Debug.Log("in fontchanger, Start. for loop");
            fontArray[i] = fontChoiceArray[i].font;
        }
    }

    public void FontChange(TMP_FontAsset fontToChangeTo) 
    {

        //find all the TMPs in the children
        //a foreach loop if I use multiple parent objects?
        //ok this didnt work
        foreach (GameObject parentObject in parentObjects)
        {
            Debug.Log("in font change, adding objects");
            textMeshProUGUIarray = parentObject.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log("in font change" + textMeshProUGUIarray.Length);
        }



        //textMeshProUGUIarray.

        //change the font for each of them. 
        //cloud probably also change the size as well.
        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIarray) 
        {
            Debug.Log("in font change, changing fonts");
            textMeshProUGUI.font = fontToChangeTo;
        }

        //then change the fonts back
        for (int i = 0; i < fontChoiceArray.Length; i++)
        {
            fontChoiceArray[i].font = fontArray[i];
        } 
    }
}
