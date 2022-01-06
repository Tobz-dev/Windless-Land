using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

//Main Author: Ilirian Zuta, 
//Secondary Author: Miranda Greting
public class PanelHandler : MonoBehaviour
{
    //private PauseMenu pm;
    //[SerializeField] private GameObject panel;
    [SerializeField] private GameObject logPanel;
    //[SerializeField] private Text logText;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private TextMeshProUGUI textmesh;
    private bool readLog;
    public int logNumber;
    private FMOD.Studio.EventInstance PageOpen;
    private PauseMenuRebindTest pauseMenu;

    [SerializeField] private GameObject PressE;

    private PlayerInputs inputActions;
    private string bindingName;
    private string controllerType;
    
    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }
    
    private void OnEnable()
    {
        inputActions.WindlessLand.Enable();
        inputActions.WindlessLand.Interact.performed += Triggered;
    }

    private void Triggered(InputAction.CallbackContext ctx)
    {
        if (ctx.control.path.Contains("Gamepad"))
        {
            controllerType = "Gamepad";
            bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Interact, 1);
        }
        else
        {
            controllerType = "Keyboard";
            bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Interact, 0);
        }
    }

    private void OnDisable()
    {
        inputActions.WindlessLand.Disable();
    }
    

    void Start()
    {
        logPanel.SetActive(false);
        //pm = GetComponent<PauseMenu>();
        PressE.SetActive(false);
        pauseMenu = GameObject.Find("MenuCanvas").GetComponent<PauseMenuRebindTest>();
        readLog = false;
        bindingName = InputManager.GetBindingPath(inputActions.WindlessLand.Interact, 0);
    }
    void Update()
    {
        if (readLog && /*Input.GetKeyUp(bindingName)*/inputActions.WindlessLand.Interact.triggered && !pauseMenu.CheckIfPaused()) /*Input.GetKeyUp(KeyCode.E)*/
        {
            if (!logPanel.activeSelf)
            {
                logPanel.SetActive(true);

                PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
                PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                PageOpen.start();
                PageOpen.release();
                Debug.Log(this.gameObject.name);




                if (logPanel == isActiveAndEnabled)
                {
                    //panel.SetActive(false);
                    PressE.SetActive(false);
                }


                switch (logNumber)
                {
                    //TODO. have them change if the player is using a controller. 
                    //or if they have rebound the controls.
                    case 1:
                        textmesh.text = "WASD to walk, Space to roll, Left Mouse Button or X to attack";
                        /*
                        if (controllerType.Equals("Keyboard"))
                        {
                            textmesh.text = "WASD to walk, \n \n  " + InputManager.GetBindingPath(inputActions.WindlessLand.Dodgeroll, 0) + " to roll, \n \n  " + InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 0) + " or " + InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 1) + " to attack";
                            return;
                        }
                        else
                        {
                            textmesh.text = "Left stick to walk, \n \n  " + InputManager.GetBindingPath(inputActions.WindlessLand.Dodgeroll, 1) + " to roll, \n \n  " + InputManager.GetBindingPath(inputActions.WindlessLand.Attack, 3) + " to attack";
                        }
                        */
                        break;
                    case 2:
                        textmesh.text = "Press Q to drink a healing potion";
                        break;
                    case 3:
                        textmesh.text = "Press the Right Mouse Button for a heavy attack. Heavy attacks consume mana";
                        break;
                    case 4:
                        textmesh.text = "Hitting enemies will restore your mana. Another way is to go near a Mana Fountain";
                        break;
                    case 5:
                        textmesh.text = "Enemies respawn when a checkpoint is activated. Be careful.";
                        break;
                    case 6:
                        textmesh.text = "Press 1 & 2 to switch between the sword and the bow. Hold Left Mouse Button to aim the bow, release to fire";
                        break;
                    default:
                        textmesh.text = "Default";
                        break;


                }
            }
        }
        else if (logPanel == isActiveAndEnabled && Input.anyKeyDown && !Input.GetKeyDown(bindingName)/* Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)  /* && !inputActions.WindlessLand.Interact.triggered */)
        {
            logPanel.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //panel.SetActive(true);
            pressText.text = "Press E to read Log";
            readLog = true;
            PressE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //panel.SetActive(false);
            readLog = false;
            PressE.SetActive(false);
            logPanel.SetActive(false);
        }
    }





}
