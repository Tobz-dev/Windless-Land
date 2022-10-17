using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Lukas Kleberg
//Secondary Author: William Smith
public class HealthPotionPickup : MonoBehaviour
{
    [SerializeField]
    private int amountOfPotionsGiven;

    [SerializeField]
    private string potionNamePref;

    private FMOD.Studio.EventInstance PotionPickUp;

    private void Start()
    {
        //destroy if potionNamePref already picked up
        if (PlayerPrefs.GetString(potionNamePref).Equals("pickedUp"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterController script = collision.gameObject.GetComponent<CharacterController>();
            Debug.Log("HealthPotionPickup. hit player");
            //collision.gameObject.GetComponent<PlayerHealthScript>().IncreasePotionAmount(amountOfPotionsGiven);
            int potionAmount = (int)script.GetFlaskUses();
            script.SetFlaskUses(potionAmount + amountOfPotionsGiven);
            script.IncreaseMaxFlaskUses(amountOfPotionsGiven);

            PlayerPrefs.SetString(potionNamePref, "pickedUp");
            //amount of potions PlayerPref +1.
            int previousAmount = PlayerPrefs.GetInt("PotionAmountPref");
            PlayerPrefs.SetInt("PotionAmountPref", previousAmount + 1);

            PotionPickUp = FMODUnity.RuntimeManager.CreateInstance("event:/Game/HealthPickup");
            PotionPickUp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PotionPickUp.start();
            PotionPickUp.release();
            Destroy(gameObject);
            //Destroy(this.GetComponentInParent<GameObject>());
        }
    }
}
