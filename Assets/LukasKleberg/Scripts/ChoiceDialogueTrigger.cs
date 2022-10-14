using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueSystem[] dialogueSystems;
    [SerializeField]
    private int dialogueNr;

    [SerializeField]
    private GameObject dialogueTriggerB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in dialogue trigger. Player entered. setting dialogue Nr to " + dialogueNr);
            //dialogueSystem.ChangeCurrentDialogueNr(dialogueNr);
            foreach (DialogueSystem dialogueSystem in dialogueSystems)
            {
                dialogueSystem.ChangeCurrentDialogueNr(dialogueNr);
            }
            //ugly, easy way to turn of the BTrigger
            dialogueTriggerB.SetActive(false);
        }
    }
}
