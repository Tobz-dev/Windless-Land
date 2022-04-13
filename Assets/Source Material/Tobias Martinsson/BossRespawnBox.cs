using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Author: Tobias martinsson
public class BossRespawnBox : MonoBehaviour
{
    public GameObject fallingPlatform1, fallingPlatform2, fallingPlatform3;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().Respawn();
            fallingPlatform1.GetComponent<FallingPlatform>().respawn();
            fallingPlatform1.GetComponent<FallingPlatform>().StopShaking();
            fallingPlatform2.GetComponent<FallingPlatform>().respawn();
            fallingPlatform2.GetComponent<FallingPlatform>().StopShaking();
            fallingPlatform3.GetComponent<FallingPlatform>().respawn();
            fallingPlatform3.GetComponent<FallingPlatform>().StopShaking();
        }
    }
}
