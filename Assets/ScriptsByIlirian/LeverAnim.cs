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

    public Color myColor;
    public float rcolor, gcolor, bcolor, alpha;

    public Renderer myRenderer;
    private Toggle toggle;
    private GameObject[] lever;





    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
        trigger = GetComponent<BoxCollider>();
        anim2 = GetComponentInChildren<Animator>();
        //anim3 = GetComponentInChildren<Animator>();
        alpha = 1;

        lever = GameObject.FindGameObjectsWithTag("Lever");
        foreach (GameObject tagged in lever)
        {
            tagged.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        }
        toggle = GameObject.Find("Prototype").GetComponent<Toggle>();

        /*foreach(GameObject tagged in lever)
        {
            myRenderer = tagged.GetComponentInChildren<Renderer>();
        }*/
        


        

        
        
        


    }

    void Update()
    {
        

        /*if (canPress)
        {
            changeColor();
        }*/

        
        if (canPress && Input.GetKeyDown(KeyCode.E))
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
            
            //anim3.SetBool("Inside", true);
 

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
            
            //anim3.SetBool("Inside", false);
            //resetColor();




        }
    }

    public void changeColor()
    {

        if (toggle.isOn)
        {
            foreach (GameObject tagged in lever)
            {
                tagged.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            }
        }
        if(toggle.isOn == false)
        {
            foreach (GameObject tagged in lever)
            {
                tagged.GetComponentInChildren<Renderer>().material.color = Color.white;
            }
        }
            
        

        

        /*myColor = new Color(1, 1, 0, 1);
            myRenderer.material.color = myColor;
           */
           
            
        
    }

    /*public void resetColor()
    {
        foreach(GameObject tagged in lever)
            {
            tagged.GetComponentInChildren<Renderer>().material.color = Color.white;
        }
    }*/






    /*public void changeColor()
    {
        if (trigger.enabled)
        {
            bcolor = 0;

            myColor = new Color(rcolor, gcolor, bcolor, alpha);
            myRenderer.material.color = myColor;
            Debug.Log("CHANGE COLOR");
        }
        else
        {
            bcolor = 1;

            myColor = new Color(rcolor, gcolor, bcolor, alpha);
            myRenderer.material.color = myColor;
        }
 

    }*/

    /*public void changeColor()
    {
        if(bcolor > 0)
        {
            bcolor -= 0.01f;
        }
        else
        {
            bcolor = 1;
        }
      

        myColor = new Color(rcolor, gcolor, bcolor, alpha);
        myRenderer.material.color = myColor;
    }

    public void resetColor()
    {
        bcolor = 1;
        myColor = new Color(1, 1, 1, 1);
        myRenderer.material.color = myColor;
    }

 

    /*if (rcolor > 0)
    {
        rcolor -= 0.009f;
    }
    else
    {
        rcolor = 1;
    }

    if (gcolor > 0)
    {
        gcolor -= 0.009f;
    }
    else
    {
        gcolor = 1;
    }

    if (bcolor > 0)
    {
        bcolor -= 0.009f;
    }
    else
    {
        bcolor = 1;

*/





    /* myColor = new Color(rcolor, gcolor, bcolor, alpha);
     myRenderer.material.color = myColor;
 }




 public void resetColor()
 {
     bcolor = 1;
     myColor = new Color(1, 1, 1, 1);
     myRenderer.material.color = myColor;

 }


/* public void changeColorback()
 {


 }*/





}
