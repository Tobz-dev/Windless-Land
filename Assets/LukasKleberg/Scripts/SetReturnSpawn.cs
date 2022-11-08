using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReturnSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private SaveSpawnPoint saveSpawnPoint;


    [SerializeField]
    private string checkLatestSceneLoaded;

    void Start()
    {
        //checks for the specific latest scene. moves player and sets respawn point.
        if (checkLatestSceneLoaded.Equals(PlayerPrefs.GetString("LatestSceneLoadedPref")))  
        {
            Debug.Log("in SetReturnSpawn. moving player");
            //Debug.Log(checkLatestSceneLoaded);
            //Debug.Log(PlayerPrefs.GetString("LatestSceneLoadedPref"));
            player.transform.position = transform.position;

            saveSpawnPoint.ActivateCheckpoint();
            //characterController.SetRespawnPoint(transform.position);
        }
    }

}
