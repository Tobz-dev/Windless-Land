using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

//Main Author: Tobias Martinsson
//Minor changes: Ilirian Zuta
public class SaveSpawnPoint : MonoBehaviour
{
    //public GameObject panel;
    [SerializeField] private GameObject PressE;
    public TextMeshProUGUI pressText;
    public TextMeshProUGUI bindingText; //RebindingMenu > KeyboardMenu > RebindLayoutgroup > Interact > InteractBinding > TriggerRebindButton > BindingText
    private bool playerOnCheckpoint = false;
    private bool usedCheckpoint = false;
    private bool gamepad = false;

    private PlayerInputs inputActions;

    private void Awake()
    {
        inputActions = InputManager.inputActions;
    }

    private void CheckControlType(InputAction.CallbackContext ctx)
    {
        if (ctx.control.name.Contains("Gamepad"))
        {
            gamepad = true;
        }
        else
        {
            gamepad = false;
        }
    }

    private void Update()
    {
        //&& usedCheckpoint == false
        if ((Input.GetKeyDown(KeyCode.E) || inputActions.WindlessLand.Interact.triggered) && playerOnCheckpoint )
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint() 
    {
        //usedCheckpoint = true;
        //panel.SetActive(false);
        PressE.SetActive(false);

        RespawnEnemies();
        GetComponent<CheckpointVFX>().StartEffect();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealthScript>().regainHealth(100);
        player.GetComponent<CharacterController>().ResetPotionsToMax();
        player.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //panel.SetActive(false);
            PressE.SetActive(false);
            playerOnCheckpoint = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && usedCheckpoint == false)
        {
            if (gamepad)
            {
                //bindingText.text = InputManager.GetBindingName("Interact", 1);
            }
            else
            {
                //bindingText.text = InputManager.GetBindingName("Interact", 0);
            }
            //panel.SetActive(true);
            PressE.SetActive(true);
            playerOnCheckpoint = true;
            //pressText.text = "Press " + bindingText.text + " to interact";          
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
