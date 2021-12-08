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
            CharacterController script = collision.gameObject.GetComponent<CharacterController>();
            Debug.Log("HealthPotionPickup. hit player");
            //collision.gameObject.GetComponent<PlayerHealthScript>().IncreasePotionAmount(amountOfPotionsGiven);
            int potionAmount = (int)script.GetFlaskUses();
            script.SetFlaskUses(potionAmount + amountOfPotionsGiven);
            Destroy(gameObject);
            //Destroy(this.GetComponentInParent<GameObject>());
        }
    }
}
