using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    //public DoorOpenAnim anim;
    //public LeverAnim leveranim;


    [SerializeField] private GameObject panel;
    //[SerializeField] private GameObject logPanel;
    private bool pickUpAllowed;
    //private bool insideArea;
    //private bool readLog;
    //private bool hasOpenedDoor;
    //private bool canPushLever;
    private GameObject key;
    [SerializeField] private Text pressText;
    //[SerializeField] private Text logText;
    private int hasKey;




    void Start()
    {
        //panel.SetActive(false);
        hasKey = 0;
        //logPanel.SetActive(false);
    }

    private void Update()
    {

        if(pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            key.SetActive(false);
            panel.SetActive(false);
            hasKey++;
            Debug.Log("GOT KEY");
            pickUpAllowed = false;
        }

        
        /*if(pickUpAllowed && !hasKey && Input.GetKeyDown(KeyCode.E))
        {
            key.SetActive(false);
            panel.SetActive(false);
            hasKey = true;
            Debug.Log("HAS KEY");
        }*/

       /* if (insideArea && hasKey == 1 && Input.GetKeyDown(KeyCode.E))
        {
            anim.openDoor();
            hasKey = 0;
            Debug.Log("No longer have key");
            insideArea = false;
            panel.SetActive(false);
            hasOpenedDoor = true;

            if (hasOpenedDoor)
            {
                panel.SetActive(false);
            }




        }
        if (readLog && Input.GetKeyDown(KeyCode.E))
        {
            logPanel.SetActive(true);

            if (logPanel == isActiveAndEnabled)
            {
                panel.SetActive(false);
            }
        }

        if (canPushLever && Input.GetKeyDown(KeyCode.E))
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
        /*if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(true);
            pressText.text = "Press E to read Log";
            readLog = true;
        }

        if (other.gameObject.CompareTag("Lever"))
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
        /*if (other.gameObject.CompareTag("Key"))
        {
            panel.SetActive(false);
            pickUpAllowed = false;
        }*/
        /*if (other.gameObject.CompareTag("Door"))
        {
            panel.SetActive(false);
            insideArea = false;
        }
        if (other.gameObject.CompareTag("Logs"))
        {
            panel.SetActive(false);
            readLog = false;
        }
        if (other.gameObject.CompareTag("Lever"))
        {
            panel.SetActive(false);
            canPushLever = false;
        }*/
    }



    /*public void closeLogPanel()
    {
        logPanel.SetActive(false);
        if (readLog)
        {
            panel.SetActive(true);
        }
    }*/

    public int getKey()
    {
        return hasKey;
    }

    public void resetKeyValue()
    {
        hasKey = 0;
    }

}
