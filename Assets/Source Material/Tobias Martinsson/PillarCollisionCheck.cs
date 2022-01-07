using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
public class PillarCollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("memes2");
        if (collision.gameObject.tag == "BossPillar")
        {
            Debug.Log("memes");
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject);
        }
        
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().takeDamage(3);
        }
        

    }
}