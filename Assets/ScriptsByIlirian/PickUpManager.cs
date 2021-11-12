using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    //public DoorOpenAnim anim;
    //public LeverAnim leveranim;


    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject logPanel;
    private bool pickUpAllowed;
    //private bool insideArea;
    private bool readLog;
    //private bool hasOpenedDoor;
    //private bool canPushLever;
    private GameObject key;
    [SerializeField] private Text pressText;
    //[SerializeField] private Text logText;
    private int hasKey;
    private GameObject[] logs;
    private Toggle toggle;




    void Start()
    {
        //panel.SetActive(false);
        hasKey = 0;
        logPanel.SetActive(false);
        logs = GameObject.FindGameObjectsWithTag("Logs");
        foreach (GameObject tagged in logs)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }

        toggle = GameObject.Find("Prototype").GetComponent<Toggle>();
    }

    private void Update()
    {

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            key.SetActive(false);
            panel.SetActive(false);
            hasKey++;
            Debug.Log("GOT KEY");
            pickUpAllowed = false;
        }








        if (readLog && Input.GetKeyDown(KeyCode.E))
        {
            logPanel.SetActive(true);

            if (logPanel == isActiveAndEnabled)
            {
                panel.SetActive(false);
            }

          
        }

        if (logPanel == isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            logPanel.SetActive(false);
        }

        /*if (canPushLever && Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(false);
            leveranim.pullLever();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            key = other.gameObject;
            pickUpAllowed = true;
            //key.SetActive(false);
            panel.SetActive(true);
            pressText.text = "Press E to pick up key!";


            //panel.SetActive(true);
            //pickUpAllowed = true;
            //hasKey++;
            //Debug.Log("I have 1 Key right now");
            //pressText.text = "Press E To Pick Up Key";
        }

        /*if (other.gameObject.CompareTag("Door"))
        {
            panel.SetActive(true);
            insideArea = true;


            if (hasKey == 1)
            {
                pressText.text = "Press E to Open";
            }
            else
            {
                pressText.text = "You need a key to open!!";
            }

        }*/
        if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(true);
            pressText.text = "Press E to read Log";
            readLog = true;
        }

        /*if (other.gameObject.CompareTag("Lever"))
        {
            panel.SetActive(true);
            pressText.text = "press E to pull lever!";
            canPushLever = true;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Key"))
        {
            key = other.gameObject;
            pickUpAllowed = false;
            panel.SetActive(false);
        }



        if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(false);
            readLog = false;
        }

    }




    public void closeLogPanel()
    {
        logPanel.SetActive(false);
        if (readLog)
        {
            panel.SetActive(true);
        }
    }

    public int getKey()
    {
        return hasKey;
    }

    public void resetKeyValue()
    {
        hasKey = 0;
    }



    public void changeLogColor()
    {

        if (toggle.isOn)
        {
            foreach (GameObject tagged in logs)
            {
                tagged.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            }
        }
        if (toggle.isOn == false)
        {
            foreach (GameObject tagged in logs)
            {
                tagged.GetComponentInChildren<Renderer>().material.color = Color.white;
            }
        }

    }
}
