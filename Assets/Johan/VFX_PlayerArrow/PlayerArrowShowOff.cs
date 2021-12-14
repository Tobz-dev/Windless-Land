using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerArrowShowOff : MonoBehaviour
{
    [SerializeField]
    private VisualEffect arrowChannelVFX;
    [SerializeField]
    private Transform channelPosition;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject playerArrowPrefab;

    private float currentChannelTime = 0f;
    private bool isFiring;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V) && !isFiring)
        {
            isFiring = true;
            anim.SetTrigger("DrawBow");
            arrowChannelVFX.SendEvent("PlayBowChannel");
        }

        if (isFiring)
        {
            currentChannelTime += Time.deltaTime * 1;

            if(currentChannelTime >= 3f)
            {
                //arrowFireVFX.SendEvent("PlayBowFire");
               
                GameObject arrowPrefab = Instantiate(playerArrowPrefab, channelPosition.position, transform.rotation);
                arrowChannelVFX.SendEvent("StopBowChannel");
                anim.SetTrigger("StopBow");
                anim.SetTrigger("BowRecoil");
                
                
                
                currentChannelTime = 0;
                isFiring = false;
            }
        }
    }
}
