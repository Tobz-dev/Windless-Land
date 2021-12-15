using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollisionCheck : MonoBehaviour
{
    private bool playerInvincible = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11 || other.gameObject.layer == 17 || other.gameObject.tag == "Player")
        {
            if(!(other.gameObject.tag == "Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                if (other.gameObject.GetComponent<CharacterController>().GetInvincibility() == false)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
