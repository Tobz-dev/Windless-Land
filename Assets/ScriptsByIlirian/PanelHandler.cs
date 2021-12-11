using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PanelHandler : MonoBehaviour
{
    //private PauseMenu pm;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject logPanel;
    //[SerializeField] private Text logText;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private TextMeshProUGUI textmesh;
    private bool readLog;
    public int logNumber;
    private FMOD.Studio.EventInstance PageOpen;



    void Start()
    {
        logPanel.SetActive(false);
        //pm = GetComponent<PauseMenu>();
        

    }
    void Update()
    {
        if (readLog && Input.GetKeyDown(KeyCode.E))
        {
            
            logPanel.SetActive(true);

            PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
            PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PageOpen.start();
            PageOpen.release();




            if (logPanel == isActiveAndEnabled)
            {
              panel.SetActive(false);
            }


            switch (logNumber)
            {
                //TODO. have them change if the player is using a controller. 
                //or if they have rebound the controls.
                case 1:
                    textmesh.text = "WASD to walk, \n \n  Spacebar to roll, \n \n  Left Mouse Button to attack";
                    break;
                case 2:
                    textmesh.text = "Q to drink a healing potion";
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
                default:
                    textmesh.text = "Default";
                    break;


            }
        }

        if (logPanel == isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            logPanel.SetActive(false);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
            pressText.text = "Press E to read Log";
            readLog = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panel.SetActive(false);
            readLog = false;
        }
    }





}
