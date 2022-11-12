using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    private Queue<string> sentences;
    private bool dialogueEnded;

    private bool dialogueActiveBeforePause;

    private PlayerInputs inputActions;

    private DialogueSystem dialogueSystem;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        dialogueEnded = true;

        inputActions = InputManager.inputActions;
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    private void Update()
    {
        if ((inputActions.WindlessLand.Interact.triggered) && !dialogueEnded)
        {
            DisplayNextSentence();
        }
    }



    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("DM, start dialogue 1");
        dialogueEnded = false;


        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Debug.Log("DM, next sentence");
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {

        Debug.Log("DM, end dialogue");
        dialogueBox.SetActive(false);
        dialogueEnded = true;

        dialogueSystem.DialogueHasEnded();
    }

    public void EndDialogueBeforePause()
    {
        if (!dialogueEnded) 
        {
            dialogueActiveBeforePause = true;
        }
        EndDialogue();
    }

    public void SetBoxActiveAfterPause() 
    {
        if (dialogueActiveBeforePause) 
        {
            dialogueBox.SetActive(true);
            dialogueEnded = false;
            dialogueActiveBeforePause = false;
        }
        
    }

    public bool GetDialogueEnded()
    {
        return dialogueEnded;
    }


}
