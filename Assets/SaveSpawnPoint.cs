using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSpawnPoint : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI pressText;
    private bool playerOnCheckpoint = false;
    private bool usedCheckpoint = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnCheckpoint && usedCheckpoint == false)
        {
            usedCheckpoint = true;
            panel.SetActive(false);
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
            player.GetComponent<CharacterController>().ResetPotions();
            player.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        panel.SetActive(false);
        playerOnCheckpoint = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && usedCheckpoint == false)
        {
            panel.SetActive(true);
            playerOnCheckpoint = true;
            
        }
    }
}
