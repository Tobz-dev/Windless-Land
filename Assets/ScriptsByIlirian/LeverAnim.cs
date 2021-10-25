using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverAnim : MonoBehaviour
{
    private Animator anim;
    private bool canPress;
    private bool leverPulled;
    [SerializeField] private Text pressText;
    [SerializeField] private GameObject panel;
    private Collider trigger;


    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
        trigger = GetComponent<BoxCollider>();
        
    }

    void Update()
    {
        if(canPress && Input.GetKeyDown(KeyCode.E))
        {
            pullLever();
        }

    }

    public void pullLever()
    {
        canPress = false;
        Debug.Log("Pressing!!!");
        anim.SetBool("Open", true);
        leverPulled = true;
        trigger.enabled = false;
        if (leverPulled)
        {
            panel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPress = true;
            panel.SetActive(true);
            pressText.text = "Press E to pull lever";

            if (leverPulled)
            {
                panel.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
            if (other.gameObject.CompareTag("Player"))
            {
                canPress = false;
                panel.SetActive(false);

            }
    }




}
