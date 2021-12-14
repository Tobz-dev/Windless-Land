using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSpawnPoint : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI pressText;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (go.name != "Boss")
                {
                    Destroy(go);
                }
            }


            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Respawner"))
            {

                go.GetComponent<EnemyRespawnScript>().RespawnEnemy();

            }
            GetComponent<CheckpointVFX>().StartEffect();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerHealthScript>().regainHealth(100);
            player.GetComponent<PlayerHealthScript>().ResetPotions();
            player.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            panel.SetActive(true);       
        }
    }
}
