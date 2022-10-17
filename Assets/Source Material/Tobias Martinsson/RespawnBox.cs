using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tobias Martinsson
public class RespawnBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(1000);
            //collision.gameObject.GetComponent<CharacterController>().Respawn();
        }
    }
}
