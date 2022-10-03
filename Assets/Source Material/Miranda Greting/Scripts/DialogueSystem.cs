using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject interactionPrompt;
    public Transform spawnPos;

    private DialogueManager dialogueManager;

    private void Start()
    {
        interactionPrompt.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, spawnPos);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("player entered dialogue");
            if(!interactionPrompt.activeInHierarchy && dialogueManager.GetDialogueEnded())
            {
                interactionPrompt.SetActive(true);
            }

            if (interactionPrompt.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
            {
                interactionPrompt.SetActive(false);
                dialogueManager.StartDialogue(dialogue);
            }
        }
    }
}
