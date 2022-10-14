using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueSystem dialogueSystem;
    [SerializeField]
    private int dialogueNr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in choicetrigger. Player entered.");
            dialogueSystem.currentdialogueNr = dialogueNr;
        }
    }
}
