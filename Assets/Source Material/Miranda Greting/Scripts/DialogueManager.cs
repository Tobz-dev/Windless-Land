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

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        dialogueEnded = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E) && !dialogueEnded)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, Transform spawnPos)
    {
        dialogueEnded = false;
        dialogueBox.transform.position = new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z);
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void StartDialogue(Dialogue dialogue)
    {
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

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueEnded = true;
    }

    public bool GetDialogueEnded()
    {
        return dialogueEnded;
    }
}
