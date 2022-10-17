using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReturnSpawn : MonoBehaviour
{

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private string checkLatestSceneLoaded;

    void Start()
    {
        //checks for the specific latest scene. moves player and sets respawn point.
        if (checkLatestSceneLoaded.Equals(PlayerPrefs.GetString("latestSceneLoaded")))  
        {
            characterController.MovePlayerTo(gameObject.transform.position);
            characterController.respawnPoint = gameObject.transform;
        }
    }

}
