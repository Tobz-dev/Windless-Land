using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionPickup : MonoBehaviour
{
    [SerializeField]
    private int amountOfPotionsGiven;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("HealthPotionPickup. hit player");
            collision.gameObject.GetComponent<PlayerHealthScript>().IncreasePotionAmount(amountOfPotionsGiven);
            Destroy(gameObject);
            //Destroy(this.GetComponentInParent<GameObject>());
        }
    }
}
