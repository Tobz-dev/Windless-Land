using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSpawnPoint : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI pressText;
    public TextMeshProUGUI bindingText; //RebindingMenu > KeyboardMenu > RebindLayoutgroup > Interact > InteractBinding > TriggerRebindButton > BindingText
    private bool playerOnCheckpoint = false;
    private bool usedCheckpoint = false;

    private PlayerInputs inputActions;

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }

    private void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.E)*/ inputActions.WindlessLand.Interact.triggered && playerOnCheckpoint && usedCheckpoint == false)
        {
            usedCheckpoint = true;
            panel.SetActive(false);

            RespawnEnemies();
            GetComponent<CheckpointVFX>().StartEffect();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerHealthScript>().regainHealth(100);
            player.GetComponent<CharacterController>().ResetPotions();
            player.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            panel.SetActive(false);
            playerOnCheckpoint = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && usedCheckpoint == false)
        {
            panel.SetActive(true);
            playerOnCheckpoint = true;
            pressText.text = "Press " + bindingText.text + " to interact";          
        }
    }

    public void RespawnEnemies()
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
    }
}
