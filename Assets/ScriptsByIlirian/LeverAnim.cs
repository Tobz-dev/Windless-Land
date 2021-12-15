using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LeverAnim : MonoBehaviour
{
    private Animator anim;
    private bool canPress;
    private bool leverPulled;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private GameObject panel;
    private Collider trigger;
    private Animator anim2;
    [Header("Triggers")]
    public UnityEvent TriggerEvent;

    private FMOD.Studio.EventInstance LeverPull;

    [SerializeField] private GameObject PressE;


    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
        trigger = GetComponent<BoxCollider>();
        anim2 = GetComponentInChildren<Animator>();
        PressE.SetActive(false);
        
    }

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().PullLever();

          
        }
        if (canPress && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimEvents>().GetLeverPulled() == true)
        {
            pullLever();

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimEvents>().SetLeverPulledFalse();
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
        PressE.SetActive(false);
        if (leverPulled)
        {
            panel.SetActive(false);
       
        }

        LeverPull = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/LeverPull");
        LeverPull.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        LeverPull.start();
        LeverPull.release();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPress = true;
            panel.SetActive(true);
            pressText.text = "Press E to pull lever";
            PressE.SetActive(true);

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
                PressE.SetActive(false);

            }
    }




}
