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
            
            
            


            if (logPanel == isActiveAndEnabled)
            {
              panel.SetActive(false);
            }


            switch (logNumber)
            {
                case 1:
                    textmesh.text = "WASD to walk, \n \n  Spacebar to roll, \n \n  Left-Mouse Button to attack";
                    break;
                case 2:
                    textmesh.text = "Q to heal, \n \n 1 or 2 to swap between weapons";
                    break;
                case 3:
                    textmesh.text = "[Placeholder]";
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
