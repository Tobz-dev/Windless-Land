using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChangeTest : MonoBehaviour
{

    GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject[] ObjectsToGetTextFrom;

    // Start is called before the first frame update
    void Start()
    {

        textObjects = GameObject.FindGameObjectsWithTag("Text");
        for (int i = 0; i <= textObjects.Length - 1; i++)
        {
            textMeshProUGUIList.Add(textObjects[i].GetComponent<TextMeshProUGUI>());
            //Debug.Log("textMeshProUGUIList count is" + textMeshProUGUIList.Count);
        }

        //this turns off the options menu if it is not active when starting the program. thats bad.
        for (int i = 0; i <= ObjectsToGetTextFrom.Length - 1; i++)
        {
            //ObjectsToGetTextFrom[i].SetActive(false);
            //Debug.Log("in fontchange test. inactive object loop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFont(TMP_FontAsset font)
    {
        Debug.Log(textMeshProUGUIList.Count);
        foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIList)
        {
            Debug.Log("in font change test, changing fonts");
            textMeshProUGUI.font = font;
        }


    }
}
