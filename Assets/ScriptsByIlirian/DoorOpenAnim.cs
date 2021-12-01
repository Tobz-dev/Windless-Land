using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpenAnim : MonoBehaviour
{

    private Animator anim;
    public PickUpManager player;
    private bool doorCanOpen;
    private bool doorIsOpen;
    [SerializeField] private Text pressText;
    [SerializeField] private GameObject panel;
    private Collider doorCollider;
    private FMOD.Studio.EventInstance DoorOpen;

    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
        doorCollider = GetComponent<BoxCollider>();
    }

    public void openDoor()
    {
        anim.SetBool("Open", true);
        player.resetKeyValue();
        Debug.Log("No longer have key");
        doorCanOpen = false;
        doorIsOpen = true;
        doorCollider.enabled = false;
        if (!doorCanOpen)
        {
            panel.SetActive(false);
        }

        DoorOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/DoorUnlock");
        DoorOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        DoorOpen.start();
        DoorOpen.release();

    }

    void Update()
    {
        if(doorCanOpen && player.getKey() == 1 && Input.GetKeyDown(KeyCode.E)){
            openDoor();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorCanOpen = true;
            panel.SetActive(true);
            checkIfDoorOpens();

            if (doorIsOpen)
            {
                panel.SetActive(false);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorCanOpen = false;
            panel.SetActive(false);
        }
    }

    public void checkIfDoorOpens()
    {
        if (doorCanOpen && player.getKey() == 1)
        {
            pressText.text = "Press E to unlock!";
        }
        else
        {
            pressText.text = "Door is Locked";
        }
    }

    


}
