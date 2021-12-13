using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSpawnPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.tag == "Player")
        {

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if(go.name != "Boss")
                {
                    Destroy(go);
                }
            }


            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Respawner"))
            {

                go.GetComponent<EnemyRespawnScript>().RespawnEnemy();
                
            }
            collisionObject.GetComponent<PlayerHealthScript>().regainHealth(100);
            collisionObject.GetComponent<PlayerHealthScript>().ResetPotions();
            collisionObject.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }
}
