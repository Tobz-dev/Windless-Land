using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue[] dialogueArray;
    private int currentDialogueNr;
    public GameObject interactionPrompt;
    public Transform spawnPos;

    private DialogueManager dialogueManager;

    private void Start()
    {
        interactionPrompt.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {

        if (interactionPrompt.activeInHierarchy && (Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("in DialogueSystem. input E.");
            interactionPrompt.SetActive(false);
            dialogueManager.StartDialogue(dialogueArray[currentDialogueNr]);
        }

    }

    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, spawnPos);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("in DialogueSystem. Player entered.");

            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("in DialogueSystem. Player exited.");
            interactionPrompt.SetActive(false);
            dialogueManager.EndDialogue();
        }
    }

    public void ChangeCurrentDialogueNr(int newDialogueNr)
    {
        currentDialogueNr = newDialogueNr;
    }
}
