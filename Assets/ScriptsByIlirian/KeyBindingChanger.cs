using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBindingChanger : MonoBehaviour
{
    //public RebindUI rebindscript;
    [SerializeField] private TextMeshProUGUI keybinding;
    private TextMesh pressE;

    // Start is called before the first frame update
    void Start()
    {
        //rebindscript = GetComponent<RebindUI>();
        pressE = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        pressE.text = keybinding.text;
    }

}
