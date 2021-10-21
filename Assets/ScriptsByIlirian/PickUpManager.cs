using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    public DoorOpenAnim anim;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject logPanel;
    private bool pickUpAllowed;
    private bool insideArea;
    private bool readLog;
    [SerializeField] private GameObject key;
    [SerializeField] private Text pressText;
    [SerializeField] private Text logText;
    private bool hasKey;
    //[SerializeField] private GameObject door;
    


    void Start()
    {
        panel.SetActive(false);
        hasKey = false;
        logPanel.SetActive(false);
    }

    private void Update()
    {
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if(insideArea && hasKey && Input.GetKeyDown(KeyCode.E))
        {
            anim.openDoor();
            hasKey = false;
            Debug.Log("No longer have key");
           
        }
        if(readLog && Input.GetKeyDown(KeyCode.E))
        {
            logPanel.SetActive(true);

            if(logPanel == isActiveAndEnabled)
            {
                panel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            panel.SetActive(true);
            pickUpAllowed = true;
            pressText.text = "Press E To Pick Up Key";
        }

        if (other.gameObject.CompareTag("Door"))
        {
            panel.SetActive(true);
            insideArea = true;

            if (hasKey)
            {
                pressText.text = "Press E to Open";
            }
            else
            {
            pressText.text = "You need a key to open!!";
            }
            
        }
        if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(true);
            pressText.text = "Press E to read Log";
            readLog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            panel.SetActive(false);
            pickUpAllowed = false;
        }
        if (other.gameObject.CompareTag("Door"))
        {
            panel.SetActive(false);
            insideArea = false;
        }
        if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(false);
            readLog = false;
        }
    }

    private void PickUp()
    {
        key.SetActive(false);
        panel.SetActive(false);
        hasKey = true;
        Debug.Log("HAS KEY");
    }

    public void closeLogPanel()
    {
        logPanel.SetActive(false);
        if (readLog)
        {
            panel.SetActive(true);
        }
    }
}
