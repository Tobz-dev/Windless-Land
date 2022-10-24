using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDialogueChanger : MonoBehaviour
{
    [SerializeField]
    private DialogueSystem[] dialogueSystems;

    // Start is called before the first frame update
    void Start()
    {

        //checkplayer pref. but this comes later.
        //change all the dialogueSystems to 0.

        SetDialogueTo(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogueTo(int newDialogueNr) 
    {
        foreach (DialogueSystem dialogueSystem in dialogueSystems)
        {
            dialogueSystem.ChangeCurrentDialogueNr(newDialogueNr);
        }
    }
}
