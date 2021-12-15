using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    //Referens: https://answers.unity.com/questions/383391/setting-a-time-limit-on-actions.html och https://www.youtube.com/watch?v=7_H0b82y_qU&t=277s&ab_channel=AeroBlizz
    public Camera cam1;
    public Camera cam2;
    public Animator fade;
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
        if (Input.GetKeyDown(KeyCode.V)) //&& visionOn == false)
        {
            Invoke ("Vision", 0.1f);
            fade.SetBool("EA", true);
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
