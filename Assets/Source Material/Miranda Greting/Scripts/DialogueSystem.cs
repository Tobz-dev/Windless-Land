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

    private PlayerInputs inputActions;

    private bool playerInRange;

    private void Start()
    {
        interactionPrompt.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();

        inputActions = InputManager.inputActions;
    }

    private void Update()
    {

        if (playerInRange && ((Input.GetKeyDown(KeyCode.E) || inputActions.WindlessLand.Interact.triggered)))
        {
            Debug.Log("in DialogueSystem. input E.");
            //interactionPrompt.SetActive(false);
            playerInRange = false;
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
            Debug.Log("in DialogueSystem. Player entered.");
            playerInRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //Debug.Log("in DialogueSystem. Player exited.");
            interactionPrompt.SetActive(false);
            dialogueManager.EndDialogue();
        }
    }

    public void DialogueHasEnded() 
    {

        playerInRange = false;
        Debug.Log("in DialogueSystem. dialogue has ended.");
        interactionPrompt.SetActive(false);
        Debug.Log(interactionPrompt.activeInHierarchy);
    }

    public void ChangeCurrentDialogueNr(int newDialogueNr)
    {
        currentDialogueNr = newDialogueNr;
    }
}
