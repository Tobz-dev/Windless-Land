using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSpawnPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(go);
            }


            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Respawner"))
            {

                go.GetComponent<EnemyRespawnScript>().RespawnEnemy();
                
            }
            collision.gameObject.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }
}
