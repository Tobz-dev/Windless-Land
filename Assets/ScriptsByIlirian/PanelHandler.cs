using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PanelHandler : MonoBehaviour
{
    //private PickUpManager pma;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject logPanel;
    //[SerializeField] private Text logText;
    [SerializeField] private Text pressText;
    [SerializeField] private TextMeshProUGUI textmesh;
    private bool readLog;
    public int logNumber;



    void Start()
    {
        logPanel.SetActive(false);
        

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
                    textmesh.text = "If you are reading this... Get out! NOW!";
                    break;
                case 2:
                    textmesh.text = "They have taken the bridge... and the second hall. We have barred the gates, but cannot hold them for long. The ground shakes. Drums... drums in the deep. We cannot get out. " +
                        "The shadows move in the dark. We cannot get out. They are coming......";
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
