using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSpawnPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().SetRespawnPoint(transform.position);
        }
    }
}
