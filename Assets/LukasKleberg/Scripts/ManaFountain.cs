using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFountain : MonoBehaviour
{
    [SerializeField]
    private int manaRestoreValue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ManaFountain. hit player");
            if (collision.gameObject.GetComponent<CharacterController>().enabled)
            {
                collision.gameObject.GetComponent<CharacterController>().ManaIncreased(manaRestoreValue);
            }
            else
            {
                collision.gameObject.GetComponent<CharacterControllerRemapTest>().ManaIncreased(manaRestoreValue);
            }
        }
    }
}
