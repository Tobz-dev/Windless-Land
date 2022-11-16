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
    private bool dialogueStarted;

    private bool dialogueActiveBeforePause;

    private PlayerInputs inputActions;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        dialogueEnded = true;

        inputActions = InputManager.inputActions;
    }

    private void Update()
    {
        if ((inputActions.WindlessLand.Interact.triggered) && !dialogueEnded)
        {
            Debug.LogWarning("DM, update show next dialogue.");
            DisplayNextSentence();
        }
    }



    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("DM, start dialogue 1");
        dialogueEnded = false;


        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //dialogueText.text = sentences.Dequeue();

        if (PlayerPrefs.GetString("FirstDialogueHasBeenActivated") == "True")
        {
            DisplayNextSentence();
        }
        else 
        {
            PlayerPrefs.SetString("FirstDialogueHasBeenActivated", "True");
        }

    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            Debug.LogWarning("DM, sent count 0.");
            EndDialogue();
            return;
        }
        Debug.LogWarning("DM, sentence amount:" + sentences.Count);

        string sentence = sentences.Dequeue();
        //Debug.Log("DM, next sentence:" + sentence);
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.LogWarning("DM, end dialogue.");
        dialogueBox.SetActive(false);
        dialogueEnded = true;

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
