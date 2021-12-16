using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollisionCheck : MonoBehaviour
{
    private bool playerInvincible = false;
    private float killTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11 || other.gameObject.layer == 17 || other.gameObject.tag == "Player")
        {
            if(!(other.gameObject.tag == "Player"))
            {
                DestroyArrow();
            }
            else
            {
                if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == false)
                {
                    DestroyArrow();
                }
            }
            
        }
    }

    private void DestroyArrow()
    {
        GetComponent<PlayerArrowUpdate>().StopArrow();
        GetComponent<PlayerArrowVFX>().PlayImpactEffect();

        killTime += 1 * Time.deltaTime;
        if (killTime >= 5f)
        {
            Destroy(gameObject);
        }
    }
}
