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
        PressE.transform.GetChild(0).GetComponent<TextMesh>().text = InputManager.GetBindingName("Interact", 0);
    }

    private void Update()
    {
        if ((inputActions.WindlessLand.Interact.triggered || Input.GetKeyDown(KeyCode.E)))
        {
            if (fountainFull && canInteract) 
            {
                Debug.Log("ManaFountain. interact key pressed");

                EmptyFountain();

                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().ManaIncreased(manaRestoreValue);
            }
        }
    }

    private void EmptyFountain()
    {
        fountainFull = false;
        //change mats of fountain parts here.
        PressE.SetActive(false);
        Transform fountain = gameObject.transform.GetChild(0);
        int index = fountain.childCount-1;
        fountain.GetChild(index).gameObject.SetActive(false);
        fountain.GetChild(index - 1).gameObject.SetActive(false);
        fountain.GetChild(index - 2).gameObject.SetActive(false);
        Transform parent = gameObject.transform.parent;
        GameObject particleSystems = parent.GetChild(parent.childCount - 1).gameObject;
        particleSystems.SetActive(true);
        for(int i = 0; i <= particleSystems.transform.childCount-1; i++)
        {
            particleSystems.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

    }

    public void RestoreFountain() 
    {
        fountainFull = true;
        Debug.Log("in ManaFountain, RestoreFountain(). fountainFull is now = " + fountainFull);
        //change mats of fountain parts here.
        Transform fountain = gameObject.transform.GetChild(0);
        int index = fountain.childCount - 1;
        fountain.GetChild(index).gameObject.SetActive(true);
        fountain.GetChild(index - 1).gameObject.SetActive(true);
        fountain.GetChild(index - 2).gameObject.SetActive(true);
        Transform parent = gameObject.transform.parent;
        GameObject particleSystems = parent.GetChild(parent.childCount - 1).gameObject;
        particleSystems.SetActive(true);
        for (int i = 0; i <= particleSystems.transform.childCount - 1; i++)
        {
            particleSystems.transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;

            if (fountainFull)
            {
                PressE.transform.GetChild(0).GetComponent<TextMesh>().text = InputManager.GetBindingName("Interact", 0);
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
