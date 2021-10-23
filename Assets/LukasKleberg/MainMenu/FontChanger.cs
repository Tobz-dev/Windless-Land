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
    private GameObject parentObject;

    //if the text is on UI, it has to be specified on declaration
    private TextMeshProUGUI[] textMeshProUGUIarray;

    //remembering fonts between play sessions would be nice.


    public void FontChange(TMP_FontAsset fontToChangeTo) 
    {

        //find all the TMPs in the children
        textMeshProUGUIarray = parentObject.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log("in font change" + textMeshProUGUIarray.Length);

        //change the font for each of them. 
        //cloud probably also change the size as well.
        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIarray) 
        {
            Debug.Log("in font change, for each loop");
            textMeshProUGUI.font = fontToChangeTo;
        }

        //TODO don't change the buttons that showcase the different fonts. or just change them back.
    }
}
