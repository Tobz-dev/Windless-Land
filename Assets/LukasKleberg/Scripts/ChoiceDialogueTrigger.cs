using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : MonoBehaviour
{

    [SerializeField]
    private int dialogueNr;

    [SerializeField]
    private GameObject dialogueTriggerToDeactivate;

    [SerializeField]
    private SoulDialogueChanger soulDialogueChanger;

    // Start is called before the first frame update
    void Start()
    {
        soulDialogueChanger = GameObject.FindObjectOfType<SoulDialogueChanger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in dialogue trigger. Player entered. setting dialogue Nr to " + dialogueNr);

            soulDialogueChanger.SetDialogueTo(dialogueNr);
        }
    }
}
