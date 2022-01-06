using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    //Referens: https://answers.unity.com/questions/383391/setting-a-time-limit-on-actions.html and https://answers.unity.com/questions/894218/check-if-gameobjectsetactivetruefalse.html
    public Camera cam1;
    public Camera cam2;
    //public Animator fade;
    public GameObject visionedCamera;
    //public GameObject player;
    //private float timeLimit = 0f;
    //private bool visionOn = false;


    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    void Vision()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && visionedCamera.activeInHierarchy != false)                                          // && visionOn == false)
        {
            Invoke ("Vision", 0.1f);
            //fade.SetBool("EA", true);
            //player.SetActive(false);

            //visionOn = true;
        }

        /*if (visionOn)
        {
            if(timeLimit <= 5)
            {
                timeLimit += Time.deltaTime;
            }

            if(timeLimit > 5)
            {
                Vision();
                fade.SetBool("EA", false);
                player.SetActive(true);
                visionOn = false;
                timeLimit = 0;
            }
        }*/
    }
}
