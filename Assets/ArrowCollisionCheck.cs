using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Secondary Author: William Smith
public class ArrowCollisionCheck : MonoBehaviour
{
    private bool playerInvincible = false;
    private float killTime = 0f;

    private FMOD.Studio.EventInstance ArrowCollision;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11 || other.gameObject.layer == 17 || other.gameObject.tag == "Player")
        {
            if(!(other.gameObject.tag == "Player"))
            {
                ArrowCollision = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowCollision");
                ArrowCollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                ArrowCollision.start();
                ArrowCollision.release();
                DestroyArrow();
            }
            else
            {
                if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == false)
                {
                    ArrowCollision = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowCollision");
                    ArrowCollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                    ArrowCollision.start();
                    ArrowCollision.release();
                    DestroyArrow();
                }
            }
            
        }
    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
