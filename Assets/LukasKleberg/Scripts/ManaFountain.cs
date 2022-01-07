using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFountain : MonoBehaviour
{
    [SerializeField]
    private int manaRestoreValue;

    private PlayerInputs inputActions;

    [SerializeField] private GameObject PressE;
    [SerializeField] private GameObject gamepadBindings;

    [SerializeField]

    private bool canInteract, fountainFull = true;
    private string controlUsed = "Keyboard";

    private string rebindPath;
    private CharacterController playerScript;
    private ChangeGamepadIcon gamepadScript;

    private FMOD.Studio.EventInstance ManaFountainSound;

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        controlUsed = playerScript.CheckControlUsed();
        gamepadScript = gameObject.GetComponent<ChangeGamepadIcon>();
        rebindPath = InputManager.GetBindingName("Interact", 0);
        if (controlUsed.Contains("Gamepad") || controlUsed.Contains("XInputControllerWindows"))
        {
            rebindPath = InputManager.GetBindingName("Interact", 1);
            gamepadScript.ChangeXboxIcon(rebindPath);
            PressE.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            PressE.transform.GetChild(0).GetComponent<TextMesh>().text = InputManager.GetBindingName("Interact", 0);
            PressE.transform.GetChild(0).gameObject.SetActive(true);
        }
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
        PressE.SetActive(false);

        //Inactivates water
        Transform fountain = gameObject.transform.GetChild(0);
        int index = fountain.childCount-1;
        fountain.GetChild(index).gameObject.SetActive(false);
        fountain.GetChild(index - 1).gameObject.SetActive(false);
        fountain.GetChild(index - 2).gameObject.SetActive(false);

        //Activates particle system
        Transform parent = gameObject.transform.parent;
        GameObject particleSystems = parent.GetChild(parent.childCount - 1).gameObject;
        particleSystems.SetActive(true);
        for(int i = 0; i <= particleSystems.transform.childCount-1; i++)
        {
            particleSystems.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

        ManaFountainSound = FMODUnity.RuntimeManager.CreateInstance("event:/Game/ManaFountain");
        ManaFountainSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ManaFountainSound.start();
        ManaFountainSound.release();

    }

    public void RestoreFountain() 
    {
        fountainFull = true;
        Debug.Log("in ManaFountain, RestoreFountain(). fountainFull is now = " + fountainFull);

        //Activate water
        Transform fountain = gameObject.transform.GetChild(0);
        int index = fountain.childCount - 1;
        fountain.GetChild(index).gameObject.SetActive(true);
        fountain.GetChild(index - 1).gameObject.SetActive(true);
        fountain.GetChild(index - 2).gameObject.SetActive(true);

        //Inactivate particle system, stops it from playing on respawn
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
                controlUsed = playerScript.CheckControlUsed();
                if (controlUsed.Contains("Gamepad") || controlUsed.ToUpper().Contains("XBOX") || controlUsed.Contains("XInputControllerWindows"))
                {
                    rebindPath = InputManager.GetBindingName("Interact", 1);
                    gamepadScript.Activate();
                    gamepadScript.ChangeXboxIcon(rebindPath);
                    PressE.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    PressE.transform.GetChild(0).GetComponent<TextMesh>().text = InputManager.GetBindingName("Interact", 0);
                    PressE.transform.GetChild(0).gameObject.SetActive(true);
                }
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
            gamepadScript.Inactivate();
        }
    }
}
