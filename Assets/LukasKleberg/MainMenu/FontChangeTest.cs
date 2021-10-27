using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChangeTest : MonoBehaviour
{

    GameObject[] textObjects;
    private List<TextMeshProUGUI> textMeshProUGUIList = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject[] ObjectsToDisableOnStart;

    // Start is called before the first frame update
    void Start()
    {
        textObjects = GameObject.FindGameObjectsWithTag("Text");
        for (int i = 0; i <= textObjects.Length - 1; i++)
        {
            textMeshProUGUIList.Add(textObjects[i].GetComponent<TextMeshProUGUI>());
            Debug.Log("textMeshProUGUIList count is" + textMeshProUGUIList.Count);
        }

        //optionsMenu.SetActive(false);
        //array of menus to set inactive
        for (int i = 0; i <= ObjectsToDisableOnStart.Length - 1; i++)
        {
            ObjectsToDisableOnStart[i].SetActive(false);
            Debug.Log("in fontchange test. inactive object loop");
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
