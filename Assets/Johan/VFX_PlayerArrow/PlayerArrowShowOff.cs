using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerArrowShowOff : MonoBehaviour
{
    public static int particleDensitySetting = 1; //Inställningen för 1.Normal, 2.Reduced, 3.Minimal - ###!!!MÅSTE LIGGA I GAME CONTROLLER!!!###

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

            
            switch (particleDensitySetting)
            {
                case 1:
                    arrowChannelVFX.SetFloat("OuterPullInCount", 7);
                    arrowChannelVFX.SetFloat("InnerPullInCount", 4);
                    arrowChannelVFX.SendEvent("PlayBowChannel");
                    break;
                case 2:
                    arrowChannelVFX.SetFloat("OuterPullInCount", 0);
                    arrowChannelVFX.SetFloat("InnerPullInCount", 0);
                    arrowChannelVFX.SendEvent("PlayBowChannel");
                    break;
                case 3:
                    break;
                default:
                    arrowChannelVFX.SetFloat("OuterPullInCount", 7);
                    arrowChannelVFX.SetFloat("InnerPullInCount", 4);
                    arrowChannelVFX.SendEvent("PlayBowChannel");
                    break;
            }
            
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
