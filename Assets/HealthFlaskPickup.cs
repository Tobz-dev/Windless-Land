using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlaskPickup : MonoBehaviour
{
    //private bool destroy = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerHealthScript>().RegainFlask() == true)
            {
                Debug.Log("Destroy health pickup");
                Destroy(gameObject);
            }
        }
    }
}
