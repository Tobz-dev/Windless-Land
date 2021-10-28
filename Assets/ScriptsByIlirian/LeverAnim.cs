using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LeverAnim : MonoBehaviour
{
    private Animator anim;
    private bool canPress;
    private bool leverPulled;
    [SerializeField] private Text pressText;
    [SerializeField] private GameObject panel;
    private Collider trigger;
    private Animator anim2;
    [Header("Triggers")]
    public UnityEvent TriggerEvent;




    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
        trigger = GetComponent<BoxCollider>();
        anim2 = GetComponentInChildren<Animator>();
        
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
        anim2.SetBool("Open", true);
        TriggerEvent.Invoke();
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
