using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFountain : MonoBehaviour
{
    [SerializeField]
    private int manaRestoreValue;

    private PlayerInputs inputActions;

    [SerializeField] private GameObject PressE;

    [SerializeField]

    private bool canInteract, fountainFull = true;

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }

    private void Update()
    {
        if ((inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(KeyCode.E)))
        {
            if (fountainFull) 
            {
                Debug.Log("ManaFountain. interact key pressed");

                EmptyFountain();

                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().ManaIncreased(manaRestoreValue);
            }
        }
    }

    private void EmptyFountain()
    {
        //this changes on all the fountains. less than ideal. turn off the trigger box collider?
        fountainFull = false;
        //change mats of fountain parts here.
        PressE.SetActive(false);
    }

    public void RestoreFountain() 
    {
        fountainFull = true;
        Debug.Log("in ManaFountain, RestoreFountain(). fountainFull is now = " + fountainFull);
        //change mats of fountain parts here.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;

            if (fountainFull)
            {
                PressE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            //panel.SetActive(false);
            PressE.SetActive(false);
        }
    }
}
