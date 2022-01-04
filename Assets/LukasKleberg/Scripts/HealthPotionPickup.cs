using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionPickup : MonoBehaviour
{
    [SerializeField]
    private int amountOfPotionsGiven;

    private FMOD.Studio.EventInstance PotionPickUp;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterController script = collision.gameObject.GetComponent<CharacterController>();
            Debug.Log("HealthPotionPickup. hit player");
            //collision.gameObject.GetComponent<PlayerHealthScript>().IncreasePotionAmount(amountOfPotionsGiven);
            int potionAmount = (int)script.GetFlaskUses();
            script.SetFlaskUses(potionAmount + amountOfPotionsGiven);
            PotionPickUp = FMODUnity.RuntimeManager.CreateInstance("event:/Game/HealthPickup");
            PotionPickUp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PotionPickUp.start();
            PotionPickUp.release();
            Destroy(gameObject);
            //Destroy(this.GetComponentInParent<GameObject>());
        }
    }
}
